using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDeNegocios.Objetos;

namespace ProyectoIngenieria.Administrador.Ruta
{
    public partial class ModificarRuta : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorRuta capaNegocios = new CapaDeNegocios.Administradores.AdministradorRuta();
        CapaDeNegocios.Administradores.AdministradorUsuario capaNegociosUsuario = new CapaDeNegocios.Administradores.AdministradorUsuario();
        List<RutaObj> rutas;
        List<UsuarioObj> usuarios;
        List<UsuarioObj> agentes = new List<UsuarioObj>();

        protected void Page_Load(object sender, EventArgs e)
        {
            rutas = capaNegocios.obtenerListaRutas();
            usuarios = capaNegociosUsuario.obtenerListaUsuarios();
            if (!IsPostBack)
            {
                llenarDropDownList();
                llenarDropDownListAgente();
                mostrarSeleccion();
            }
        }

        /// <summary>
        ///  Método utilizado para el llenado del picker de rutas. Si no se encuentra ningún usuario se muestra el respectivo mensaje de error.
        /// </summary>
        private void llenarDropDownList()
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
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ninguna ruta." + "');", true);
            }
        }

        /// <summary>
        ///  Método utilizado para el llenado del picker de agentes. Si no se encuentra ningún usuario se muestra el respectivo mensaje de error.
        /// </summary>
        private void llenarDropDownListAgente()
        {
            if (usuarios.Count > 0)
            {
                foreach (var usuarioTemp in usuarios)
                {
                    if (usuarioTemp.getFkRol() == 2 && usuarioTemp.getActivoSN() == true)
                    {
                        agentes.Add(usuarioTemp);
                        ddlAgente.Items.Add(usuarioTemp.getNombreUsuario() + "/" + usuarioTemp.nombre);
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ningún agente." + "');", true);
            }

            ddlAgente.Enabled = false;
        }

        /// <summary>
        ///  Evento del DropDownList para mostrar los datos que este tiene.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlRuta_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccion();
        }

        /// <summary>
        /// Método donde se muestre los datos de la ruta elegido en el picker de usuarios.
        /// Si no se encuentra un usuarios seleccionado muestra el respectivo mensaje de error.
        /// </summary>
        private void mostrarSeleccion()
        {
            usuarios = capaNegociosUsuario.obtenerListaUsuarios();
            int seleccionado = ddlRuta.SelectedIndex;
            if (seleccionado != -1)
            {
                RutaObj rutaTemp = rutas[seleccionado];
                txtIDRuta.Value = rutaTemp.getRutaID().ToString();
                txtNombre.Value = rutaTemp.getNombre();
                txtNombre.Disabled = false;
                int i = 0;
                int index = 0;
                foreach (var usuarioTemp in usuarios)
                {
                    if (usuarioTemp.getFkRol() == 2 && usuarioTemp.getActivoSN() == true)
                    {
                        agentes.Add(usuarioTemp);
                    }
                }

                foreach (UsuarioObj usuarioTemp in agentes)
                {
                    if (usuarioTemp.getNombreUsuario().Equals(rutaTemp.getFkNombreUsuario()))
                    {
                        index = i;
                    }
                    i++;
                }
                ddlAgente.SelectedIndex = index;
                ddlAgente.Enabled = true;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe de seleccionar una ruta a modificar." + "');", true);
            }
        }

        /// <summary>
        ///  Evento de clickear el botón modificar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modificarRuta();
        }

        /// <summary>
        /// Método utlizado para el ingreso y modificacion de los daros de la ruta elegido para ser modificada.
        /// Además se validan que los campos sean llenados de forma correcta.
        /// </summary>
        private void modificarRuta()
        {
            try
            {
                if (String.IsNullOrEmpty(txtNombre.Value) || ddlAgente.SelectedIndex == -1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos vacíos, por favor ingrese lo que se le solicita" + "');", true);

                }
                else
                {
                    UsuarioObj usuarioTemp = usuarios[ddlAgente.SelectedIndex];

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + capaNegocios.modificarRuta(int.Parse(txtIDRuta.Value), txtNombre.Value, usuarioTemp.getNombreUsuario()) + "'); window.location='" + Request.ApplicationPath + "Administrador/Ruta/RutaAdministrador.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos vacíos, por favor ingrese lo que se le solicita" + "');", true);
            }
        }
    }
}