using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDeNegocios.Objetos;

namespace ProyectoIngenieria.Administrador.Cliente
{
    public partial class EliminarCliente : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorCliente capaNegocios = new CapaDeNegocios.Administradores.AdministradorCliente();
        CapaDeNegocios.Administradores.AdministradorRuta capaNegociosRutas = new CapaDeNegocios.Administradores.AdministradorRuta();
        List<ClienteObj> clientes;
        List<RutaObj> rutas;
        protected void Page_Load(object sender, EventArgs e)
        {
            clientes = capaNegocios.obtenerListaClientes();
            rutas = capaNegociosRutas.obtenerListaRutas();
            if (!IsPostBack)
            {
                llenarDropDownList();
                mostrarSeleccion();
            }
        }

        /// <summary>
        /// El método llenarPicker se conecta con la base de datos para poder obtener una lista
        /// de todos los clientes y posteriormente agrega al DropDownList todos los clientes de
        /// la lista.
        /// </summary>
        private void llenarDropDownList()
        {
            if (clientes.Count > 0)
            {
                foreach (var clientesTemp in clientes)
                {
                    ddlCliente.Items.Add(clientesTemp.nombre);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ningún cliente." + "');", true);
            }
        }

        /// <summary>
        /// El método mostrarSeleccion muestra la información de nombre de cliente
        /// del cliente seleccionado.
        /// </summary>
        private void mostrarSeleccion()
        {
            int seleccionado = ddlCliente.SelectedIndex;
            if (seleccionado != -1)
            {
                ClienteObj clienteTemp = clientes[seleccionado];
                txtID.Value = clienteTemp.getClienteID().ToString();
                txtContacto.Value = clienteTemp.getContacto();

                foreach (RutaObj rutaTemp in rutas)
                {
                    if (rutaTemp.getRutaID() == clienteTemp.getFkRuta())
                    {
                        txtRuta.Value = rutaTemp.getNombre();
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar un cliente a eliminar" + "');", true);
            }
        }

        /// <summary>
        /// El método eliminarUsuario se encarga de verfificar que un usuario haya sido seleccionado,
        /// además, confirma si se puede o no llevar a cabo su eliminación y muestra al usuario si pudo
        /// o no eliminarse correctamente.
        /// </summary>
        public void eliminarCliente()
        {
            try
            {
                if (ddlCliente.SelectedIndex == -1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Se debe seleccionar un cliente." + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + capaNegocios.confirmarEliminacion(int.Parse(txtID.Value)) + "'); window.location='" + Request.ApplicationPath + "Administrador/Cliente/ClienteAdministrador.aspx';", true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Evento de para hacer click en el botón ELIMINAR.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccion();
        }

        /// <summary>
        /// Evento para hacer click en el DropDownList para seleccionar el cliente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminar_Clicked(object sender, EventArgs e)
        {
            eliminarCliente();

        }

    }
}