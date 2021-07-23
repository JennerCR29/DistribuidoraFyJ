using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDeNegocios.Objetos;

namespace ProyectoIngenieria.Administrador.Usuario
{
    public partial class EliminarUsuario : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorUsuario capaNegocios = new CapaDeNegocios.Administradores.AdministradorUsuario();
        CapaDeNegocios.Administradores.AdministradorBodega capaNegociosBodega = new CapaDeNegocios.Administradores.AdministradorBodega();
        List<BodegaObj> bodegas;
        List<UsuarioObj> usuarios;
        protected void Page_Load(object sender, EventArgs e)
        {
            bodegas = capaNegociosBodega.obtenerListaBodegas();
            usuarios = capaNegocios.obtenerListaUsuarios();
            if (!IsPostBack) {
                llenarDropDownList();
                mostrarSeleccion();
            }
        }

        /// <summary>
        /// El método llenarPicker se conecta con la base de datos para poder obtener una lista
        /// de todos los usuarios y posteriormente agrega al DropDownList todos los usuarios de
        /// la lista.
        /// </summary>
        private void llenarDropDownList()
        {
            usuarios = capaNegocios.obtenerListaUsuarios();
            if (usuarios.Count > 0)
            {
                foreach (var usuarioTemp in usuarios)
                {
                    usuario.Items.Add(usuarioTemp.getNombreUsuario() + "/ " + usuarioTemp.nombre.ToString());
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ningún usuario." + "');", true);
            }
        }

        /// <summary>
        /// El método mostrarSeleccion muestra la información de nombre de usuario y el identificador 
        /// del usuario seleccionado.
        /// </summary>
        private void mostrarSeleccion()
        {
            int seleccionado = usuario.SelectedIndex;
            if (seleccionado != -1)
            {
                UsuarioObj usuarioTemp = usuarios[seleccionado];
                txtID.Value = usuarioTemp.getNombreUsuario();
                txtNombreUsuario.Value = usuarioTemp.nombre;

                if (usuarioTemp.getFkRol() == 1)
                {
                    txtRol.Value = "Administrador";
                }
                else
                {
                    txtRol.Value = "Agente de ventas";
                }

                bodegas = capaNegociosBodega.obtenerListaBodegas();
                foreach (BodegaObj bodegaTemp in bodegas)
                {
                    if (bodegaTemp.getBodegaID() == usuarioTemp.getFkBodega())
                    {
                        txtBodega.Value = bodegaTemp.getNombre() + "/" + bodegaTemp.getUbicacion();
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar un usuario a eliminar" + "');", true);
            }
        }

        /// <summary>
        /// El método eliminarUsuario se encarga de verfificar que un usuario haya sido seleccionado,
        /// además, confirma si se puede o no llevar a cabo su eliminación y muestra al usuario si pudo
        /// o no eliminarse correctamente.
        /// </summary>
        public void eliminarUsuario()
        {
            try
            {
                if (usuario.SelectedIndex == -1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Se debe seleccionar un usuario." + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + capaNegocios.confirmarEliminacion(txtID.Value) + "'); window.location='" + Request.ApplicationPath + "Administrador/Usuario/UsuarioAdministrador.aspx';", true);
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
        protected void btnEliminar_Clicked(object sender, EventArgs e)
        {
            eliminarUsuario();
        }

        /// <summary>
        /// Evento para hacer click en el DropDownList para seleccionar el usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void usuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccion();
        }
    }
}