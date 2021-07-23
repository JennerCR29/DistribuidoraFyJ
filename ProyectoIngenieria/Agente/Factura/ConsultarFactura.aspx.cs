using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Agente.Factura
{
    public partial class ConsultarFactura : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorCliente capaNegociosCliente = new CapaDeNegocios.Administradores.AdministradorCliente();
        CapaDeNegocios.Administradores.AdministradorCuenta capaNegociosCuenta = new CapaDeNegocios.Administradores.AdministradorCuenta();
        CapaDeNegocios.Administradores.AdministradorFactura capaNegociosFactura = new CapaDeNegocios.Administradores.AdministradorFactura();
        CapaDeNegocios.Administradores.AdministradorRuta capaNegociosRuta = new CapaDeNegocios.Administradores.AdministradorRuta();
        String nombreUsuario;
        List<ClienteObj> clientes;
        List<CuentaObj> cuentas;
        List<FacturaObj> listaFacturas;
        List<CuentaObj> cuentasXcliente = new List<CuentaObj>();
        List<FacturaObj> facturasXcliente = new List<FacturaObj>();

        List<ClienteObj> clientesXagente = new List<ClienteObj>();
        List<CuentaObj> listaXcliente = new List<CuentaObj>();


        protected void Page_Load(object sender, EventArgs e)
        {
            nombreUsuario = (string)(Session["nombreUsuario"]);
            cuentas = capaNegociosCuenta.obtenerListaCuentas();
            clientes = capaNegociosCliente.obtenerListaClientes();

            if (!IsPostBack)
            {
                llenarDropDownListClientes();
                llenarDropDownListCuentas();
            }
        }

        /// <summary>
        /// Método encargado de llenar el DropDownList con todos los clientes relaciones con el nombre de usuario que se traen desde la base de datos.
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " El usuario aún no tiene una ruta asignada." + "'); window.location='" + Request.ApplicationPath + "Agente/Factura/FacturaAgente.aspx';", true);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " La base de datos no contiene ningún cliente." + "'); window.location='" + Request.ApplicationPath + "Agente/Factura/FacturaAgente.aspx';", true);
            }
        }

        /// <summary>
        /// Evento del DropDownList que contiene los clientes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDropDownListCuentas();
        }


        /// <summary>
        /// Método encargado de llenar el DropDownList de Cuentas que están realacionadas con los clientes del usuario que ingreso al sistema.
        /// </summary>
        private void llenarDropDownListCuentas()
        {
            clientesXagente = (List<ClienteObj>)Session["listaClientesXagente"];
            ClienteObj clienteTemp = new ClienteObj();
            listaXcliente.Clear();
            ddlCuentaCliente.Items.Clear();
            cuentas = capaNegociosCuenta.obtenerListaCuentas();
            if (ddlCliente.SelectedIndex != -1)
            {
                clienteTemp = clientesXagente[ddlCliente.SelectedIndex];
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " El usuario no tiene ningún cliente asignado, o el cliente no contiene una cuenta asignada." + "'); window.location='" + Request.ApplicationPath + "Agente/Factura/FacturaAgente.aspx';", true);

            }
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
                    ddlCuentaCliente.Items.Add(c.getId().ToString() + "-" + c.getTipo());
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " La base de datos no contiene ninguna cuenta asociada al cliente." + "'); window.location='" + Request.ApplicationPath + "Agente/Factura/FacturaAgente.aspx';", true);
            }
            Session["listaListaXcliente"] = listaXcliente;
            llenarDropDownListFacturas();
        }


        /// <summary>
        /// Evento relacionado al DropDownList de cuentas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCuentaCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDropDownListFacturas();

        }

        /// <summary>
        /// Método encargado de llenar el DropDownList de facturas que están relacionadas a los clientes del usuario que ingresó al sistema.
        /// </summary>
        private void llenarDropDownListFacturas()
        {
            ddlFactura.Items.Clear();
            listaXcliente = (List<CuentaObj>)Session["listaListaXcliente"];
            int seleccionado = ddlCuentaCliente.SelectedIndex;
            cuentas = capaNegociosCuenta.obtenerListaCuentas();
            listaFacturas = capaNegociosFactura.obtenerListaFacturasFecha(calFechaInicio.SelectedDate, calFechaFin.SelectedDate);
            facturasXcliente.Clear();
            if (seleccionado != -1)
            {
                CuentaObj cuentaTemp = listaXcliente[seleccionado];
                foreach (FacturaObj facturaTemp in listaFacturas)
                {
                    if (facturaTemp.getFKCuentaID() == cuentaTemp.getId())
                    {
                        ddlFactura.Items.Add(facturaTemp.getFacturaID().ToString() + " / " + facturaTemp.getFecha().ToString() + " / " + facturaTemp.getTotal().ToString());
                        facturasXcliente.Add(facturaTemp);
                        Session["listaFacturas"] = listaFacturas;
                    }

                }
                Session["facturasXCliente"] = facturasXcliente;
            }
            llenarDataGrid();
        }

        /// <summary>
        /// Evento del DropDownList de facturas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDataGrid();
        }

        /// <summary>
        /// Método encargado de llenar el DataGridView con las facturas que se encuentran relacionadas al tipo de cuenta del cliente seleccionado.
        /// </summary>
        private void llenarDataGrid()
        {
            facturasXcliente = (List<FacturaObj>)Session["facturasXCliente"];
            FacturaObj facturaTemp = new FacturaObj();
            List<LineaPedidoObj> listaTemp = new List<LineaPedidoObj>();
            LineaPedidoTemp pedidoTemp = new LineaPedidoTemp();
            List<LineaPedidoTemp> lineaPedidoTemps = new List<LineaPedidoTemp>();
            if (ddlFactura.SelectedIndex != -1)
            {
                listaTemp = capaNegociosFactura.obtenerListaLineaPedidos(facturasXcliente[ddlFactura.SelectedIndex].getFacturaID());
                if (ddlFactura.SelectedIndex != -1)
                {
                    facturaTemp = facturasXcliente[ddlFactura.SelectedIndex];
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " El usuario no tiene ningún cliente asignado" + "'); window.location='" + Request.ApplicationPath + "Agente/Factura/FacturaAgente.aspx';", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " No hay facturas para consultar." + "'); window.location='" + Request.ApplicationPath + "Agente/Factura/FacturaAgente.aspx';", true);

            }
            foreach (LineaPedidoObj lineaTemp in listaTemp)
            {
                if (lineaTemp.getFkFacturaID() == facturaTemp.getFacturaID())
                {
                    pedidoTemp = new LineaPedidoTemp();
                    pedidoTemp.nombreProducto = lineaTemp.producto.getNombre();
                    pedidoTemp.cantidad = lineaTemp.getCantidad();
                    pedidoTemp.descuento = lineaTemp.getDescuento();
                    pedidoTemp.precioUnitario = lineaTemp.producto.getPrecioVenta();
                    pedidoTemp.subtotal = lineaTemp.getSubtotal();
                    lineaPedidoTemps.Add(pedidoTemp);
                }
            }
            txtDescuentoFactura.Value = facturaTemp.getDescuento().ToString();
            txtTotalFactura.Value = facturaTemp.getTotal().ToString();
            txtFechaFactura.Value = facturaTemp.getFecha().ToString();
            ListaOrdenes.DataSource = lineaPedidoTemps;
            ListaOrdenes.DataBind();
        }

        /// <summary>
        /// Evento relacionado al botón de finalizar que se encarga de mostrar un mensaje y además realizar un redireccionamiento de página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + " Consulta terminada." + "'); window.location='" + Request.ApplicationPath + "Agente/Factura/FacturaAgente.aspx';", true);

        }

        protected void btnAplicarFiltro_Click(object sender, EventArgs e)
        {
            if (calFechaFin.SelectedDate.Year == 1 && calFechaInicio.SelectedDate.Year == 1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "No se seleccionó ninguna fecha, por favor revise." + "');", true);
            }
            else
            {
                llenarDropDownListFacturas();
                calFechaInicio.SelectedDates.Clear();
                calFechaFin.SelectedDates.Clear();
            }
        }
    }
}