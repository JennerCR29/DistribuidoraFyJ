using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Administrador.Cuenta
{
    public partial class AbonarCuenta : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorCliente capaNegocios = new CapaDeNegocios.Administradores.AdministradorCliente();
        CapaDeNegocios.Administradores.AdministradorCuenta capaNegociosCuenta = new CapaDeNegocios.Administradores.AdministradorCuenta();
        CapaDeNegocios.Administradores.AdministradorFactura capaNegociosFactura = new CapaDeNegocios.Administradores.AdministradorFactura();
        CapaDeNegocios.Administradores.AdministradorRuta capaNegociosRuta = new CapaDeNegocios.Administradores.AdministradorRuta();
        List<ClienteObj> clientes;
        List<ClienteObj> clientesXagente = new List<ClienteObj>();
        List<ClienteObj> facturasXcliente = new List<ClienteObj>();
        List<FacturaObj> facturas = new List<FacturaObj>();
        List<FacturaObj> facturasTemp = new List<FacturaObj>();
#pragma warning disable CS0169 // El campo 'AbonarCuenta.cuentas' nunca se usa
        List<CuentaObj> cuentas;
#pragma warning restore CS0169 // El campo 'AbonarCuenta.cuentas' nunca se usa
        List<CuentaObj> cuentasXcliente = new List<CuentaObj>();
        int idFacturaActual = 0;
        string nombreUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            clientes = capaNegocios.obtenerListaClientes();
            cuentasXcliente = capaNegociosCuenta.obtenerListaCuentas();
            facturas = capaNegociosFactura.obtenerListaFacturas();
            nombreUsuario = (string)(Session["nombreUsuario"]);
            if (!IsPostBack)
            {
                llenarDropDownListClientes();
            }
        }

        /// <summary>
        /// Método encargado de llenar los campos de selección para un cliente.
        /// </summary>
        private void llenarDropDownListClientes()
        {
            int rutaActualID = 0;
            if (capaNegociosRuta.obtenerRuta(nombreUsuario) != null)
            {
                rutaActualID = capaNegociosRuta.obtenerRuta(nombreUsuario).getRutaID();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + "El usuario aún no tiene una ruta asignada." + "'); window.location='" + Request.ApplicationPath + "Administrador/Cuenta/CuentaAdministrador.aspx';", true);
            }
            ClienteObj clienteTemporal = null;
            if (clientes.Count > 0)
            {
                foreach (var clienteTemp in clientes)
                {
                    foreach (var cuentaTemp in cuentasXcliente)
                    {
                        foreach (var facturaTemp in facturas)
                        {
                            if (clienteTemp.getClienteID() == cuentaTemp.getFkClienteID() && clienteTemp.getFkRuta() == rutaActualID && cuentaTemp.getTipo().Equals("Crédito") && cuentaTemp.getId() == facturaTemp.getFKCuentaID() && facturaTemp.getSaldo() > 0)
                            {
                                ddlCliente.Items.Add(clienteTemp.nombre);
                                clientesXagente.Add(clienteTemp);
                                if (clienteTemporal == null)
                                {
                                    clienteTemporal = clienteTemp;
                                }
                                break;
                            }
                        }
                    }
                }
                if (clienteTemporal == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " La base de datos no contiene ningún cliente asociado al usuario" + "'); window.location='" + Request.ApplicationPath + "Administrador/Cuenta/CuentaAdministrador.aspx';", true);
                }
                else
                {
                    mostrarSeleccionUnicaCliente(clienteTemporal);
                }
                Session["clientesXagente"] = clientesXagente;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ningún cliente." + "');", true);
            }
        }

        /// <summary>
        /// Método encargado de llenar el campo para seleccionar una factura a la cual se desea abonar
        /// </summary>
        /// <param name="cuentaID"></param>
        private void llenarDropDownListFacturas(int cuentaID)
        {
            facturas.Clear();
            facturas = capaNegociosFactura.obtenerListaFacturas();
            FacturaObj factura = null;
            if (facturas.Count > 0)
            {
                foreach (var facturaTemp in facturas)
                {
                    if (facturaTemp.getFKCuentaID() == cuentaID && facturaTemp.getSaldo() > 0)
                    {
                        ddlFacturasPendientes.Items.Add(facturaTemp.getFecha().ToString());
                        FacturaObj facturaNuevaLista = facturaTemp;
                        facturasTemp.Add(facturaNuevaLista);
                        if (factura == null)
                        {
                            factura = facturaTemp;
                        }
                    }
                }
                Session["facturasTemporales"] = facturasTemp;
                mostrarSeleccionFacturaUnica(factura);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ninguna factura." + "');", true);
            }
        }

        /// <summary>
        /// Evento encargado de que cuando se cambie de cliente se muestren los valores solo del seleccionado, 
        /// además de limpiar los espacios con valores innecesarios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            facturasTemp.Clear();
            ddlFacturasPendientes.Items.Clear();
            txtTotal.Value = "";
            txtSaldoRestante.Value = "";
            txtCuenta.Value = "";
            mostrarSeleccion();
        }

        /// <summary>
        /// Método encargado de mostrar los valores de la cuenta, llenando las facturas relacionadas a esta cuenta.
        /// </summary>
        private void mostrarSeleccion()
        {
            clientesXagente = (List<ClienteObj>)Session["clientesXagente"];
            int seleccionado = ddlCliente.SelectedIndex;
            if (seleccionado != -1)
            {
                ClienteObj clienteTemp = clientesXagente[seleccionado];
                foreach (CuentaObj cuentaTemp in cuentasXcliente)
                {
                    if (cuentaTemp.getFkClienteID() == clienteTemp.getClienteID() && cuentaTemp.getTipo().Equals("Crédito"))
                    {
                        txtCuenta.Value = cuentaTemp.getId() + "-" + cuentaTemp.getTipo();
                        llenarDropDownListFacturas(cuentaTemp.getId());
                    }
                }
            }
        }

        /// <summary>
        /// Método encargado de recorrer las cuentas relacionadas con el cliente seleccionado. 
        /// </summary>
        /// <param name="cliente"></param>
        private void mostrarSeleccionUnicaCliente(ClienteObj cliente)
        {
            foreach (CuentaObj cuentaTemp in cuentasXcliente)
            {
                if (cuentaTemp.getFkClienteID() == cliente.getClienteID() && cuentaTemp.getTipo().Equals("Crédito"))
                {
                    txtCuenta.Value = cuentaTemp.getId() + "-" + cuentaTemp.getTipo();
                    llenarDropDownListFacturas(cuentaTemp.getId());
                }
            }
        }
        /// <summary>
        /// Evento encargado de llamar al método para mostrar los datos cuando se seleccione una factura. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ddlFacturasPendientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccionFactura();
        }
        /// <summary>
        /// Método encargado de mostrar la información relacionada con la factura seleccionada
        /// </summary>
        private void mostrarSeleccionFactura()
        {
            int seleccionado = ddlFacturasPendientes.SelectedIndex;
            facturas = (List<FacturaObj>)Session["FacturasTemporales"];
            if (seleccionado != -1)
            {
                FacturaObj facturaTemp = facturas[seleccionado];
                foreach (FacturaObj facturaT in facturas)
                {
                    if (facturaT.getFacturaID() == facturaTemp.getFacturaID())
                    {
                        idFacturaActual = facturaT.getFacturaID();
                        txtTotal.Value = facturaT.getTotal().ToString();
                        txtSaldoRestante.Value = facturaT.getSaldo().ToString();
                    }
                }
                Session["facturaActualID"] = idFacturaActual;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar un cliente." + "');", true);
            }
        }
        /// <summary>
        /// Método que retorna un id de una factura única y rellena los espacios con la factura seleccionada de total y saldo. 
        /// </summary>
        /// <param name="factura"></param>
        private void mostrarSeleccionFacturaUnica(FacturaObj factura)
        {
            facturas = (List<FacturaObj>)Session["FacturasTemporales"];

            foreach (FacturaObj facturaT in facturas)
            {
                if (facturaT.getFacturaID() == factura.getFacturaID())
                {
                    idFacturaActual = facturaT.getFacturaID();
                    txtTotal.Value = facturaT.getTotal().ToString();
                    txtSaldoRestante.Value = facturaT.getSaldo().ToString();
                    break;
                }
            }
            Session["facturaActualID"] = idFacturaActual;
        }


        /// <summary>
        /// Método encargado de enlazarse con la capa de negocios para efectuar un abono de cuenta, 
        /// devuelve un mensaje de confirmación en caso de que haya sido exitodo el abono o de lo contrario se le indica 
        /// el caso de error encontrado
        /// </summary>
        public void abonarCuenta()
        {
            if (txtMonto.Value != "")
            {
                if (txtMonto.Value.ToCharArray().All(Char.IsDigit))
                {
                    if (decimal.Parse(txtSaldoRestante.Value) >= decimal.Parse(txtMonto.Value))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Notificación:" + capaNegociosCuenta.abonarCuenta((int)Session["FacturaActualID"], decimal.Parse(txtSaldoRestante.Value), decimal.Parse(txtMonto.Value)) + "'); window.location='" +
                        Request.ApplicationPath + "Administrador/Cuenta/CuentaAdministrador.aspx';", true);
                    }
                    else
                    {
                        txtMonto.Value = "";
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "El monto a abonar no puede ser mayor al saldo restante." + "');", true);
                    }
                }
                else
                {
                    txtMonto.Value = "";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Solo se permiten números." + "');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe de ingresar un monto." + "');", true);
            }

        }

        /// <summary>
        /// Evento del método apra confirmar abono, encargado de llamar al método para abonar. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            abonarCuenta();

        }
    }
}