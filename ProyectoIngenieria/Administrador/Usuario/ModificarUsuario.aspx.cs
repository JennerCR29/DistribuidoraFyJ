using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Administrador.Usuario
{
    public partial class ModificarUsuario : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorUsuario capaNegocios = new CapaDeNegocios.Administradores.AdministradorUsuario();
        CapaDeNegocios.Administradores.AdministradorBodega capaNegociosBodega = new CapaDeNegocios.Administradores.AdministradorBodega();
        List<UsuarioObj> usuarios;
        List<BodegaObj> bodegas;
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios = capaNegocios.obtenerListaUsuarios();
            bodegas = capaNegociosBodega.obtenerListaBodegas();

            if (!IsPostBack)
            {
                llenarDropDownList();
                llenarDropDownListBodega();
                mostrarSeleccion();
            }
        }

        /// <summary>
        ///  Método utilizado para el llenado del picker de usuarios. Si no se encuentra ningún usuario se muestra el respectivo mensaje de error.
        /// </summary>
        private void llenarDropDownList()
        {

            if (usuarios.Count > 0)
            {
                foreach (var usuarioTemp in usuarios)
                {
                    ddlUsuario.Items.Add(usuarioTemp.getNombreUsuario() + "/ " + usuarioTemp.nombre.ToString());
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ningún usuario." + "');", true);
            }
        }

        /// <summary>
        ///  Método utilizado para el llenado del picker de bodegas. Si no se encuentra ningún usuario se muestra el respectivo mensaje de error.
        /// </summary>
        private void llenarDropDownListBodega()
        {
            bodegas = capaNegociosBodega.obtenerListaBodegas();
            if (bodegas.Count > 0)
            {
                foreach (var bodegaTemp in bodegas)
                {
                    ddlBodega.Items.Add(bodegaTemp.getNombre() + " / " + bodegaTemp.getUbicacion());
                }
                ddlBodega.Enabled = false;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "No hay bodegan disponibles en la base de datos." + "');", true);
            }
        }

        /// <summary>
        ///  Evento del DropDownList para mostrar los datos que este tiene.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccion();
        }

        /// <summary>
        ///  Método donde se muestran los datos del usuario elegido en el picker de usuarios.
        ///  Si no se encuentra un usuario seleccionado muestra el respectivo mensaje de error.
        /// </summary>
        private void mostrarSeleccion()
        {
            int seleccionado = ddlUsuario.SelectedIndex;
            if (seleccionado != -1)
            {
                UsuarioObj usuarioTemp = usuarios[seleccionado];
                txtIDUsuario.Value = usuarioTemp.getNombreUsuario();
                txtID.Value = usuarioTemp.id.ToString();
                TtxtNombreUsuario.Disabled = false;
                txtContrasena.Disabled = false;
                ddlBodega.Enabled = true;
                TtxtNombreUsuario.Value = usuarioTemp.nombre;
                txtContrasena.Value = usuarioTemp.getContrasena();
                int i = 0;
                int index = 0;
                foreach (BodegaObj bodegaTemp in bodegas)
                {
                    if (bodegaTemp.getBodegaID() == usuarioTemp.getFkBodega())
                    {
                        index = i;
                    }
                    i++;
                }
                ddlBodega.SelectedIndex = index;

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar un usuario a modificar" + "');", true);
            }
        }

        /// <summary>
        ///  Método utilizado para el ingreso y modificación de los datos del usuario elegido para ser modificado.
        ///  Además se validan que los campos sean llenados de forma correcta.
        /// </summary>
        private void modificarUsuario()
        {
            try
            {
                if (String.IsNullOrEmpty(TtxtNombreUsuario.Value) || String.IsNullOrEmpty(txtContrasena.Value) || ddlBodega.SelectedIndex == -1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos vacíos, por favor ingrese lo que se le solicita" + "');", true);

                }
                else
                {
                    bool tamanoContrasena = capaNegocios.validarContrasena(txtContrasena.Value);
                    if (tamanoContrasena)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "La contraseña debe incluir al menos 8 caracteres, por favor intente nuevamente." + "');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + capaNegocios.modificarUsuario(int.Parse(txtID.Value), TtxtNombreUsuario.Value, txtIDUsuario.Value, txtContrasena.Value, bodegas[ddlBodega.SelectedIndex].getBodegaID()) + "'); window.location='" + Request.ApplicationPath + "Administrador/Usuario/UsuarioAdministrador.aspx';", true);
                    }
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
            modificarUsuario();
        }
    }
}