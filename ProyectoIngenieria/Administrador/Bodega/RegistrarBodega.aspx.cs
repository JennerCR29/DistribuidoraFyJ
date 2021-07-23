using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Administrador.Bodega
{
    public partial class WebForm1 : System.Web.UI.Page
    {


        CapaDeNegocios.Administradores.AdministradorBodega capaNegocios = new CapaDeNegocios.Administradores.AdministradorBodega();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///  Método utilizado para el ingreso de las bodegas, donde se realizan las validaciones de los campos: Nombre y Ubicacion.
        ///  Dado el caso de que se ingresen datos inválidos se mostrará el respectivo mensaje de error.
        /// </summary>
        private void ingresarBodega()
        {
            try
            {
                if (String.IsNullOrEmpty(txtNombre.Value) || String.IsNullOrEmpty(txtUbicacion.Value))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Deben de llenar todos los datos." + "');", true);
                }
                else
                {
                    bool registrado = capaNegocios.crearBodega(txtNombre.Value, txtUbicacion.Value);
                    if (registrado)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + "La bodega se agregó con éxito." + "'); window.location='" + Request.ApplicationPath + "Administrador/Bodega/BodegaAdministrador.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "La bodega no se pudo agregar, intente nuevamente." + "');", true);
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
            ingresarBodega();

        }

    }
}