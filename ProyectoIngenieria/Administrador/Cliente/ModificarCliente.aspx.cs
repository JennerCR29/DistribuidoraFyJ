using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDeNegocios.Objetos;

namespace ProyectoIngenieria.Administrador.Cliente
{
    public partial class ModificarCliente : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorCliente capaNegocios = new CapaDeNegocios.Administradores.AdministradorCliente();
        CapaDeNegocios.Administradores.AdministradorRuta capaNegociosRuta = new CapaDeNegocios.Administradores.AdministradorRuta();
        List<ClienteObj> clientes;
        List<RutaObj> rutas;
        protected void Page_Load(object sender, EventArgs e)
        {
            clientes = capaNegocios.obtenerListaClientes();
            rutas = capaNegociosRuta.obtenerListaRutas();
            if (!IsPostBack)
            {
                llenarDropDownList();
                llenarDropDownListRutas();
                mostrarSeleccion();
            }
        }

        /// <summary>
        ///  Método utilizado para el llenado del picker de clientes. Si no se encuentra ningún cliente se muestra el respectivo mensaje de error.
        /// </summary>
        private void llenarDropDownList()
        {
            if (clientes.Count > 0)
            {
                foreach (var clienteTemp in clientes)
                {
                    ddlCliente.Items.Add(clienteTemp.nombre);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ningún cliente." + "');", true);
            }
        }

        /// <summary>
        ///  Método utilizado para el llenado del picker de rutas. Si no se encuentra ninguna ruta se muestra el respectivo mensaje de error.
        /// </summary>
        private void llenarDropDownListRutas()
        {
            if (rutas.Count > 0)
            {
                foreach (var rutaTemp in rutas)
                {
                    ddlRuta.Items.Add(rutaTemp.getNombre());
                }
                ddlRuta.Enabled = false;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "No hay rutas disponibles en la base de datos." + "');", true);
            }
        }

        /// <summary>
        ///  Método donde se muestran los datos del cliente elegido en el picker de clientes.
        ///  Si no se encuentra un cliente seleccionado muestra el respectivo mensaje de error.
        /// </summary>
        private void mostrarSeleccion()
        {
            int seleccionado = ddlCliente.SelectedIndex;
            if (seleccionado != -1)
            {
                ClienteObj clienteTemp = clientes[seleccionado];
                txtIDCliente.Value = clienteTemp.getClienteID().ToString();
                txtNombre.Value = clienteTemp.nombre.ToString();
                txtContacto.Value = clienteTemp.getContacto();
                txtIDCliente.Disabled = true;
                txtNombre.Disabled = false;
                txtContacto.Disabled = false;
                ddlRuta.Enabled = true;
                int i = 0;
                int index = 0;
                foreach (RutaObj rutaTemp in rutas)
                {
                    if (rutaTemp.getRutaID() == clienteTemp.getFkRuta())
                    {
                        index = i;
                    }
                    i++;
                }
                ddlRuta.SelectedIndex = index;

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar un cliente a modificar" + "');", true);
            }
        }

        /// <summary>
        ///  Método utilizado para el ingreso y modificación de los datos del cliente elegido para ser modificado.
        ///  Además se validan que los campos sean llenados de forma correcta.
        /// </summary>
        private void modificarCliente()
        {
            try
            {
                if (String.IsNullOrEmpty(txtNombre.Value) || String.IsNullOrEmpty(txtContacto.Value) || ddlRuta.SelectedIndex == -1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos vacíos, por favor ingrese lo que se le solicita" + "');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + capaNegocios.modificarCliente(clientes[ddlCliente.SelectedIndex].id, int.Parse(txtIDCliente.Value), txtContacto.Value, txtNombre.Value, clientes[ddlCliente.SelectedIndex].getFkRuta()) + "'); window.location='" + Request.ApplicationPath + "Administrador/Cliente/ClienteAdministrador.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos vacíos, por favor ingrese lo que se le solicita" + "');", true);
            }
        }

        /// <summary>
        ///  Evento de clickear el botón modificar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modificarCliente();
        }

        /// <summary>
        ///  Evento del DropDownList para mostrar los datos que este tiene.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccion();
        }
    }
}