using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria
{
    public partial class InicioSesion : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorUsuario capaNegocios = new CapaDeNegocios.Administradores.AdministradorUsuario();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnIngresar_Clicked(object sender, EventArgs e)
        {
            validarDatos();
        }

        private void validarDatos()
        {
            //Response.Redirect("Administrador/Bodega/BodegaAdministrador.aspx");
            UsuarioObj usuarioTemp = capaNegocios.obtenerUsuarioNombre(inptID.Value);
            if (inptID.Value != null & inptPSW.Value != null)
            {
                if (inptID.Value.ToCharArray().All(Char.IsDigit))
                {
                    if (usuarioTemp != null)
                    {
                        if (usuarioTemp.getContrasena().Equals(inptPSW.Value))
                        {
                            if (usuarioTemp.getFkRol() == 1)
                            {
                                Session["nombreUsuario"] = usuarioTemp.getNombreUsuario();
                                Session["contrasena"] = usuarioTemp.getContrasena();
                                Response.Redirect("Administrador/Bodega/BodegaAdministrador.aspx");
                            }
                            else
                            {
                                Session["nombreUsuario"] = usuarioTemp.getNombreUsuario();
                                Response.Redirect("Agente/Cuenta/CuentaAgente.aspx");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La contraseña no coincide con el nombre de usuario" + "');", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Por favor ingrese un nombre de usuario asociado a un usuario" + "');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "El nombre de usuario solo admite números, favor revisar" + "');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos vacíos, por favor ingrese lo que se le solicita" + "');", true);
            }

        }
    }
}