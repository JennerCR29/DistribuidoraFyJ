using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDeNegocios.Objetos;

namespace ProyectoIngenieria.Administrador.Ruta
{
    public partial class RegistrarRuta : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorRuta capaNegocios = new CapaDeNegocios.Administradores.AdministradorRuta();
        CapaDeNegocios.Administradores.AdministradorUsuario capaNegociosUsuarios = new CapaDeNegocios.Administradores.AdministradorUsuario();
        List<UsuarioObj> usuarios;
        List<UsuarioObj> agentes = new List<UsuarioObj>();
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios = capaNegociosUsuarios.obtenerListaUsuarios();
            if (!IsPostBack)
            {
                llenarDropDownListUsuarios();
            }
        }

        /// <summary>
        /// Método utilizado para el llenado del DropDown que tiene los usuarios.
        /// </summary>
        private void llenarDropDownListUsuarios()
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + "La base de datos no contiene ningún agente." + "'); window.location='" + Request.ApplicationPath + "Administrador/Ruta/RutaAdministrador.aspx';", true);
            }
        }

        /// <summary>
        ///  Método utilizado para el ingreso de las rutas, donde se realizan las validaciones de los campos: nombre, el picker de agente.
        ///  Dado el caso de que se ingresen datos inválidos se mostrará el respectivo mensaje de error.
        /// </summary>
        private void ingresarRuta()
        {
            try
            {
                if (String.IsNullOrEmpty(txtNombre.Value) || ddlAgente.SelectedIndex == -1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Deben de llenar todos los datos." + "');", true);
                }
                else
                {
                    foreach (var usuarioTemp in usuarios)
                    {
                        if (usuarioTemp.getFkRol() == 2 && usuarioTemp.getActivoSN() == true)
                        {
                            agentes.Add(usuarioTemp);
                        }
                    }
                    bool registrado = capaNegocios.crearRuta(txtNombre.Value, agentes[ddlAgente.SelectedIndex].getNombreUsuario());
                    if (registrado)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + "La ruta se agregó con éxito." + "'); window.location='" + Request.ApplicationPath + "Administrador/Ruta/RutaAdministrador.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "La ruta no se pudo agregar, intente nuevamente." + "');", true);
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
            ingresarRuta();
        }
    }
}
