using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Administrador.Factura
{
    public partial class EliminarFactura : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorCliente capaNegociosCliente = new CapaDeNegocios.Administradores.AdministradorCliente();
        CapaDeNegocios.Administradores.AdministradorCuenta capaNegociosCuenta = new CapaDeNegocios.Administradores.AdministradorCuenta();
        CapaDeNegocios.Administradores.AdministradorFactura capaNegociosFactura = new CapaDeNegocios.Administradores.AdministradorFactura();
        CapaDeNegocios.Administradores.AdministradorUsuario capaNegociosUsuario = new CapaDeNegocios.Administradores.AdministradorUsuario();

        String nombreUsuario;
        List<ClienteObj> clientes;
        List<CuentaObj> cuentas;
        List<FacturaObj> facturas;
        List<FacturaObj> listaFacturas;
        List<CuentaObj> cuentasXcliente = new List<CuentaObj>();
        List<FacturaObj> facturasXcliente = new List<FacturaObj>();
        List<ClienteObj> clientesXagente = new List<ClienteObj>();
        List<CuentaObj> listaXcliente = new List<CuentaObj>();
        List<FacturaObj> listaFacturasDDL = new List<FacturaObj>();

        protected void Page_Load(object sender, EventArgs e)
        {
            nombreUsuario = (string)(Session["nombreUsuario"]);
            cuentas = capaNegociosCuenta.obtenerListaCuentas();
            clientes = capaNegociosCliente.obtenerListaClientes();
            facturas = capaNegociosFactura.obtenerListaFacturas();

            if (!IsPostBack)
            {
                llenarDropDownListClientes();
                llenarDropDownListCuentas();
            }
        }

        /// <summary>
        /// Evento relacionado con el DropDownList de clientes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDropDownListCuentas();
        }

        /// <summary>
        /// Método encargado de llenar el DropDownList con todos los clientes que vienen desde la base de datos.
        /// </summary>
        private void llenarDropDownListClientes()
        {
            if (clientes.Count > 0)
            {
                foreach (var clienteTemp in clientes)
                {
                    foreach (var cuentaTemp in cuentas)
                    {
                        CuentaObj cuentaT = cuentaTemp;
                        cuentasXcliente.Add(cuentaT);
                        if (cuentaTemp.getFkClienteID() == clienteTemp.getClienteID() && !clientesXagente.Contains(clienteTemp))
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " La base de datos no contiene ningún cliente." + "'); window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
            }
        }

        /// <summary>
        /// Evento relacionado con el DropDownList de cuentas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCuentaCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDropDownListFacturas();
        }

        /// <summary>
        /// Método encargado de llenar el DropDownList de Cuentas que están realacionadas con los clientes del usuario que ingreso al sistema.
        /// </summary>
        private void llenarDropDownListCuentas()
        {
            clientesXagente = (List<ClienteObj>)Session["listaClientesXagente"];
            ClienteObj clienteTemp = clientesXagente[ddlCliente.SelectedIndex];
            listaXcliente.Clear();
            ddlCuentaCliente.Items.Clear();
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
                    ddlCuentaCliente.Items.Add(c.getId().ToString() + "-" + c.getTipo());
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " La base de datos no contiene ninguna cuenta asociada al cliente." + "'); window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
            }
            Session["listaListaXcliente"] = listaXcliente;
            llenarDropDownListFacturas();
        }


        protected void ddlFactura_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            listaFacturas = capaNegociosFactura.obtenerListaFacturas();
            facturasXcliente.Clear();
            if (seleccionado != -1)
            {
                CuentaObj cuentaTemp = listaXcliente[seleccionado];
                foreach (FacturaObj facturaTemp in listaFacturas)
                {
                    if (facturaTemp.getFKCuentaID() == cuentaTemp.getId())
                    {
                        ddlFactura.Items.Add(facturaTemp.getFacturaID().ToString() + " / " + facturaTemp.getFecha().ToString() + " / " + facturaTemp.getTotal().ToString());
                        listaFacturasDDL.Add(facturaTemp);
                        Session["listaFacturasDDL"] = listaFacturasDDL;
                    }
                }

            }
        }

        /// <summary>
        /// Evento relacionado al botón de eliminar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            validarContrasena();
        }

        /// <summary>
        /// Método encargado de validar que sea el usuario administrador el que quiere eliminar la factura escogido, esto validando que la contraseña sea la misma que la 
        /// que se encuentra en la base de datos. además si es correcta en envía los datos a la base de datos para la debida eliminación.
        /// </summary>
        private void validarContrasena()
        {
            List<UsuarioObj> listaUsuarioTemp;
            List<FacturaObj> listaTemporalFacturas;
            listaTemporalFacturas = (List<FacturaObj>)Session["listaFacturasDDL"];
            listaUsuarioTemp = capaNegociosUsuario.obtenerListaUsuarios();
            foreach (UsuarioObj usuarioTemp in listaUsuarioTemp)
            {
                if (usuarioTemp.getFkRol() == 1 && usuarioTemp.getContrasena().Equals(txtContra.Value) && nombreUsuario.Equals(usuarioTemp.getNombreUsuario()))
                {
                    if (capaNegociosFactura.confirmarEliminacion(listaTemporalFacturas[ddlFactura.SelectedIndex].getFacturaID()).Equals("La factura no se pudo eliminar."))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + "La factura no se pudo eliminar." + "'); window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + "La eliminación se realizó correctamente" + "'); window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
                        return;
                    }
                }
                
            }
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Contraseña incorrecta." + "');", true);
            txtContra.Value = "";
        }
    }
}