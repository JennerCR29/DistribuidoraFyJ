using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDeNegocios.Objetos;

namespace ProyectoIngenieria.Administrador.Cliente
{
    public partial class RegistrarCliente : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorCliente capaNegocios = new CapaDeNegocios.Administradores.AdministradorCliente();
        CapaDeNegocios.Administradores.AdministradorRuta capaNegociosRuta = new CapaDeNegocios.Administradores.AdministradorRuta();
        List<RutaObj> rutas;

        protected void Page_Load(object sender, EventArgs e)
        {
            rutas = capaNegociosRuta.obtenerListaRutas();
            if (!IsPostBack)
            {
                llenarDropDownListRutas();
            }
        }

        /// <summary>
        ///  Método utilizado para el llenado del picker de clientes. Si no se encuentra ningún cliente se muestra el respectivo mensaje de error.
        /// </summary>
        private void llenarDropDownListRutas()
        {
            if (rutas.Count > 0)
            {
                foreach (var rutaTemp in rutas)
                {
                    ddlRuta.Items.Add(rutaTemp.getNombre());
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error: " + "La base de datos no contiene ningún cliente." + "'); window.location='" + Request.ApplicationPath + "Administrador/Cliente/ClienteAdministrador.aspx';", true);
            }
        }

        /// <summary>
        ///  Método utilizado para el ingreso de los clientes, donde se realizan las validaciones de los campos: NombreCliente, contacto y
        ///  el picker de ruta. Dado el caso de que se ingresen datos inválidos se mostrará el respectivo mensaje de error.
        /// </summary>
        private void ingresarCliente()
        {
            try
            {
                if (String.IsNullOrEmpty(txtNombre.Value) || String.IsNullOrEmpty(txtContacto.Value) || ddlRuta.SelectedIndex == -1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Deben de llenar todos los datos." + "');", true);
                }
                else
                {
                    bool registrado = capaNegocios.crearCliente(txtNombre.Value, txtContacto.Value, rutas[ddlRuta.SelectedIndex].getRutaID());
                    if (registrado)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + "El cliente se agregó con éxito." + "'); window.location='" + Request.ApplicationPath + "Administrador/Cliente/ClienteAdministrador.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "El cliente no se pudo agregar, intente nuevamente." + "');", true);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        ///  Evento de clickear el botón Registrar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            ingresarCliente();
        }
    }
}