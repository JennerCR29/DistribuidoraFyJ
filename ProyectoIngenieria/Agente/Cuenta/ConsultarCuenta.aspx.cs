using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Agente.Cuenta
{
    public partial class ConsultarCuenta : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorCliente capaNegociosCliente = new CapaDeNegocios.Administradores.AdministradorCliente();
        CapaDeNegocios.Administradores.AdministradorCuenta capaNegociosCuenta = new CapaDeNegocios.Administradores.AdministradorCuenta();
        CapaDeNegocios.Administradores.AdministradorFactura capaNegociosFactura = new CapaDeNegocios.Administradores.AdministradorFactura();
        CapaDeNegocios.Administradores.AdministradorRuta capaNegociosRuta = new CapaDeNegocios.Administradores.AdministradorRuta();
        String nombreUsuario;
        List<ClienteObj> clientes;
        List<CuentaObj> cuentas;
        List<FacturaObj> listaFacturas;
        List<CuentaObj> listaXcliente = new List<CuentaObj>();
        List<FacturaObj> facturasXcliente = new List<FacturaObj>();
        List<CuentaObj> cuentasXcliente = new List<CuentaObj>();
        List<ClienteObj> clientesXagente = new List<ClienteObj>();
        protected void Page_Load(object sender, EventArgs e)
        {
            nombreUsuario = (string)(Session["nombreUsuario"]);
            cuentas = capaNegociosCuenta.obtenerListaCuentas();
            clientes = capaNegociosCliente.obtenerListaClientes();
            txtFechaCreacion.Disabled = true;
            txtSaldo.Disabled = true;
            txtTotalFacturas.Disabled = true;
            if (!IsPostBack)
            {
                llenarDropDownListClientes();
                llenarDropDownListCuentas();
            }
        }
        /// <summary>
        /// Método encargado de llenar los clientes a elegir para una futura consulta
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " El usuario aún no tiene una ruta asignada." + "'); window.location='" + Request.ApplicationPath + "Agente/Cuenta/CuentaAgente.aspx';", true);
            }
            if (clientes.Count > 0)
            {
                foreach (var clienteTemp in clientes)
                {
                    foreach (var cuentaTemp in cuentas)
                    {
                        CuentaObj cuentaT = cuentaTemp;
                        cuentasXcliente.Add(cuentaT);
                        if (cuentaTemp.getFkClienteID() == clienteTemp.getClienteID() && !clientesXagente.Contains(clienteTemp) && clienteTemp.getFkRuta() == rutaActualID)
                        {
                            ddlCliente.Items.Add(clienteTemp.nombre);
                            ClienteObj clienteT = clienteTemp;
                            clientesXagente.Add(clienteT);
                        }
                    }
                }
                Session["listaCuentasXcliente"] = cuentasXcliente;
                Session["listaClientesXagente"] = clientesXagente;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " La base de datos no contiene ningún cliente." + "'); window.location='" + Request.ApplicationPath + "Agente/Cuenta/CuentaAgente.aspx';", true);
            }
        }

        /// <summary>
        /// Método encargado de llenar la lista de cuentas enlazadas al cliente seleccionado
        /// </summary>
        private void llenarDropDownListCuentas()
        {
            clientesXagente = (List<ClienteObj>)Session["listaClientesXagente"];
            ClienteObj clienteTemp = new ClienteObj();

            if (ddlCliente.SelectedIndex != -1)
            {
                clienteTemp = clientesXagente[ddlCliente.SelectedIndex];
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " El usuario no tiene ningún cliente asignado o el cliente no contiene una cuenta." + "'); window.location='" + Request.ApplicationPath + "Agente/Cuenta/CuentaAgente.aspx';", true);

            }
            listaXcliente.Clear();
            ddlCuentas.Items.Clear();
            cuentas = capaNegociosCuenta.obtenerListaCuentas();
            foreach (CuentaObj c in cuentas)
            {
                if (c.getFkClienteID() == clienteTemp.getClienteID())
                {
                    CuentaObj cuentaNueva = c;
                    listaXcliente.Add(cuentaNueva);
                }
            }
            if (listaXcliente.Count > 0)
            {
                foreach (var c in listaXcliente)
                {
                    ddlCuentas.Items.Add(c.getId().ToString() + "-" + c.getTipo());
                }
                mostrarSeleccionCuentaUnica(listaXcliente[0]);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " La base de datos no contiene ninguna cuenta asociada al cliente." + "'); window.location='" + Request.ApplicationPath + "Agente/Cuenta/CuentaAgente.aspx';", true);
            }
            Session["listaListaXcliente"] = listaXcliente;
        }

        /// <summary>
        /// Método encargado de recorrer la lista de facturas enlazadas a la cuenta y de calcular el saldo y total
        /// junto con la fecha paara ser mostrado. 
        /// </summary>
        /// <param name="cuenta"></param>
        private void mostrarSeleccionCuentaUnica(CuentaObj cuenta)
        {
            listaFacturas = capaNegociosFactura.obtenerListaFacturasFecha(calFechaInicio.SelectedDate, calFechaFin.SelectedDate);
            facturasXcliente.Clear();
            decimal sumaTotal = 0;
            decimal sumaSaldo = 0;
            foreach (FacturaObj facturaTemp in listaFacturas)
            {
                if (facturaTemp.getFKCuentaID() == cuenta.getId())
                {
                    sumaTotal += facturaTemp.getTotal();
                    sumaSaldo += facturaTemp.getSaldo();
                    FacturaObj facturaAsociada = facturaTemp;
                    facturasXcliente.Add(facturaAsociada);
                }
            }
            Session["ListaFacturasXCliente"] = facturasXcliente;
            txtSaldo.Value = "¢" + sumaSaldo.ToString();
            txtTotalFacturas.Value = "¢" + sumaTotal.ToString();
            txtFechaCreacion.Value = cuenta.getFecha().ToString();
            llenarDataGrid();
        }

        /// <summary>
        /// Método encargado de que en el GridView se puedan apreciar todas las facturas enlazadas a la cuenta
        /// seleccionada
        /// </summary>
        private void llenarDataGrid()
        {
            facturasXcliente = (List<FacturaObj>)Session["ListaFacturasXCliente"];
            List<facturaTempClass> listaTemp = new List<facturaTempClass>();
            foreach (FacturaObj factura in facturasXcliente)
            {
                facturaTempClass facturaTemp = new facturaTempClass();
                facturaTemp.facturaID = factura.getFacturaID();
                facturaTemp.fecha = factura.getFecha();
                facturaTemp.descuento = factura.getDescuento();
                facturaTemp.total = factura.getTotal();
                listaTemp.Add(facturaTemp);
            }
            ListaOrdenes.DataSource = listaTemp;
            ListaOrdenes.DataBind();
        }

        /// <summary>
        /// Método encargado de rescatar las facturas enlazadas a la cuenta seleccionada. 
        /// </summary>
        private void mostrarSeleccionCuenta()
        {
            listaXcliente = (List<CuentaObj>)Session["listaListaXcliente"];
            int seleccionado = ddlCuentas.SelectedIndex;
            cuentas = capaNegociosCuenta.obtenerListaCuentas();
            listaFacturas = capaNegociosFactura.obtenerListaFacturasFecha(calFechaInicio.SelectedDate, calFechaFin.SelectedDate);
            facturasXcliente.Clear();
            decimal sumaTotal = 0;
            decimal sumaSaldo = 0;
            if (seleccionado != -1)
            {
                CuentaObj cuentaTemp = listaXcliente[seleccionado];
                foreach (FacturaObj facturaTemp in listaFacturas)
                {
                    if (facturaTemp.getFKCuentaID() == cuentaTemp.getId())
                    {
                        sumaTotal += facturaTemp.getTotal();
                        sumaSaldo += facturaTemp.getSaldo();
                        FacturaObj facturaAsociada = facturaTemp;
                        facturasXcliente.Add(facturaAsociada);
                    }
                }
                txtSaldo.Value = "¢" + sumaSaldo.ToString();
                txtTotalFacturas.Value = "¢" + sumaTotal.ToString();
                txtFechaCreacion.Value = cuentaTemp.getFecha().ToString();
                Session["ListaFacturasXCliente"] = facturasXcliente;
            }
        }

        /// <summary>
        /// Evento que actúa al cambiar de cliente. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDropDownListCuentas();
        }

        /// <summary>
        /// Evento que actúa al cambiar de cuenta. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccionCuenta();
            llenarDataGrid();
        }

        /// <summary>
        /// Evento encargado de finalizar la consulta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + " Consulta terminada." + "'); window.location='" + Request.ApplicationPath + "Agente/Cuenta/CuentaAgente.aspx';", true);
        }

        protected void btnAplicarFiltro_Click(object sender, EventArgs e)
        {
            if (calFechaFin.SelectedDate.Year == 1 && calFechaInicio.SelectedDate.Year == 1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "No se seleccionó ninguna fecha, por favor revise." + "');", true);
            }
            else
            {
                listaFacturas = capaNegociosFactura.obtenerListaFacturasFecha(calFechaInicio.SelectedDate, calFechaFin.SelectedDate);
                listaXcliente = (List<CuentaObj>)Session["listaListaXcliente"];
                int seleccionado = ddlCuentas.SelectedIndex;
                CuentaObj cuentaTemp = listaXcliente[seleccionado];
                foreach (FacturaObj facturaTemp in listaFacturas)
                {
                    if (facturaTemp.getFKCuentaID() == cuentaTemp.getId())
                    {
                        FacturaObj facturaAsociada = facturaTemp;
                        facturasXcliente.Add(facturaAsociada);
                    }
                }
                Session["ListaFacturasXCliente"] = facturasXcliente;
                llenarDataGrid();
                calFechaInicio.SelectedDates.Clear();
                calFechaFin.SelectedDates.Clear();
            }
        }
    }

    /// <summary>
    /// Clase parcial para crear facturas temporales. 
    /// </summary>
    public class facturaTempClass
    {
        public int facturaID { get; set; }
        public DateTime fecha { get; set; }
        public byte descuento { get; set; }
        public decimal total { get; set; }

    }

}