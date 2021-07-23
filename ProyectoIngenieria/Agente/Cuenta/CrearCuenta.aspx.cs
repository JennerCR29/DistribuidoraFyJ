using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Agente.Cuenta
{
    public partial class CrearCuenta : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorCuenta capaCuenta = new CapaDeNegocios.Administradores.AdministradorCuenta();
        CapaDeNegocios.Administradores.AdministradorCliente capaCliente = new CapaDeNegocios.Administradores.AdministradorCliente();
        CapaDeNegocios.Administradores.AdministradorRuta capaNegociosRuta = new CapaDeNegocios.Administradores.AdministradorRuta();
        CapaDeNegocios.Administradores.AdministradorFactura capaNegociosFactura = new CapaDeNegocios.Administradores.AdministradorFactura();


        List<ClienteObj> clientes;
        List<ClienteObj> clientesXAgente = new List<ClienteObj>();
        private String nombreUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            clientes = capaCliente.obtenerListaClientes();
            nombreUsuario = (string)(Session["nombreUsuario"]);
            if (!IsPostBack)
            {
                llenarDdlClientes();
            }
        }

        /// <summary>
        /// Método encargado de llenar el DropDownList con todos los clientes relaciones con el nombre de usuario que se traen desde la base de datos.
        /// </summary>
        private void llenarDdlClientes()
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
                    if (clienteTemp.getFkRuta() == rutaActualID)
                    {
                        ddlCliente.Items.Add(clienteTemp.nombre);
                        clientesXAgente.Add(clienteTemp);
                    }
                }
                if (clientesXAgente.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " El usuario no tiene clientes asignados." + "'); window.location='" + Request.ApplicationPath + "Agente/Cuenta/CuentaAgente.aspx';", true);
                }
                Session["listaClientesXAgente"] = clientesXAgente;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ningún cliente." + "');", true);
            }
        }

        /// <summary>
        /// Método encargado de crear una cuenta con el cliente seleccionado, el tipo de cuenta y además se envía hacia la base de datos el nombre de usuario del usuario que inicio
        /// el sistema.
        /// </summary>
        private void crearCuenta()
        {
            try
            {
                clientesXAgente = (List<ClienteObj>)Session["listaClientesXAgente"];
                bool registrado = capaCuenta.crearCuenta(clientesXAgente[ddlCliente.SelectedIndex].getClienteID(), ddlCuenta.SelectedIndex + 1, nombreUsuario);
                if (registrado)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + "La cuenta fue agregada con éxito" + "'); window.location='" + Request.ApplicationPath + "Agente/Cuenta/CuentaAgente.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error: " + "La cuenta no se pudo agregar, intente nuevamente. Puede ser que el cliente ya tenga una cuenta de ese tipo existente." + "'); window.location='" + Request.ApplicationPath + "Agente/Cuenta/CuentaAgente.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos vacíos, por favor ingrese lo que se le solicita" + "');", true);
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Evento del botón crear.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            crearCuenta();
        }
    }
}