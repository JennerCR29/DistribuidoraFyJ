using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Administrador.Usuario
{
    public partial class RegistrarUsuario : System.Web.UI.Page
    {

        CapaDeNegocios.Administradores.AdministradorUsuario capaNegocios = new CapaDeNegocios.Administradores.AdministradorUsuario();
        CapaDeNegocios.Administradores.AdministradorBodega capaNegociosBodega = new CapaDeNegocios.Administradores.AdministradorBodega();
        List<BodegaObj> bodegas;
        protected void Page_Load(object sender, EventArgs e)
        {
            bodegas = capaNegociosBodega.obtenerListaBodegas();
            if (!IsPostBack)
            {
                llenarDropDownListBodega();
            }
        }

        /// <summary>
        /// Método utilizado para el llenado del DropDown que tiene las bodegas.
        /// </summary>
        private void llenarDropDownListBodega()
        {
            bodegas = capaNegociosBodega.obtenerListaBodegas();
            if (bodegas.Count > 0)
            {
                foreach (var bodegaTemp in bodegas)
                {
                    ddlBodega.Items.Add(bodegaTemp.getNombre() + "/ " + bodegaTemp.getUbicacion());
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error: " + "La base de datos no contiene ninguna bodega." + "'); window.location='" + Request.ApplicationPath + "Administrador/Usuario/UsuarioAdministrador.aspx';", true);
            }
        }

        /// <summary>
        ///  Método utilizado para el ingreso de los usuarios, donde se realizan las validaciones de los campos: ID, NombreUsuario, contraseña,
        ///  el picker de rol y el de bodega. Dado el caso de que se ingresen datos inválidos se mostrará el respectivo mensaje de error.
        /// </summary>
        private void ingresarUsuario()
        {
            try
            {
                if (String.IsNullOrEmpty(txtID.Value) || String.IsNullOrEmpty(txtNombreUsuario.Value) || String.IsNullOrEmpty(txtContra.Value) || cbRol.SelectedIndex == -1 || ddlBodega.SelectedIndex == -1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Deben de llenar todos los datos." + "');", true);
                }
                else
                {
                    bool idRepetido = capaNegocios.validarID(txtID.Value);
                    if (idRepetido)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "El identificardor que desea agregar ya se encuentra registrado en el sistema." + "');", true);

                    }
                    else
                    {
                        bool tamanoContrasena = capaNegocios.validarContrasena(txtContra.Value);
                        if (tamanoContrasena)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "La contraseña debe incluir al menos 8 caracteres, por favor intente nuevamente." + "');", true);

                        }
                        else
                        {
                            bool registrado = capaNegocios.crearUsuario(txtNombreUsuario.Value, txtID.Value, txtContra.Value, bodegas[ddlBodega.SelectedIndex].getBodegaID(), cbRol.SelectedIndex + 1);
                            if (registrado)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + "El usuario se agregó con éxito." + "'); window.location='" + Request.ApplicationPath + "Administrador/Usuario/UsuarioAdministrador.aspx';", true);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "El usuario no se pudo agregar, intente nuevamente." + "');", true);
                            }

                        }
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
            ingresarUsuario();
            
        }

    }
}