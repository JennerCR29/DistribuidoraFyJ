using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDeNegocios.Objetos;

namespace ProyectoIngenieria.Administrador.Bodega
{
    public partial class ModificarBodega : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorBodega capaNegocios = new CapaDeNegocios.Administradores.AdministradorBodega();
        List<BodegaObj> bodegas;
        protected void Page_Load(object sender, EventArgs e)
        {
            bodegas = capaNegocios.obtenerListaBodegas();
            if (!IsPostBack)
            {
                llenarDropDowList();
                mostrarSeleccion();
            }
            
        }

        /// <summary>
        ///  Método utilizado para el llenado del picker de Bodegas. Si no se encuentra ninguna bodega se muestra el respectivo mensaje de error.
        /// </summary>
        private void llenarDropDowList()
        {
            bodegas = capaNegocios.obtenerListaBodegas();
            if (bodegas.Count > 0)
            {
                foreach (var bodegaTemp in bodegas)
                {
                    ddlBodega.Items.Add(bodegaTemp.getNombre() + "/" + bodegaTemp.getUbicacion());
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ninguna bodega." + "');", true);
            }
        }

        /// <summary>
        ///  Método donde se muestran los datos de la bodega elegida en el picker de bodegas.
        ///  Si no se encuentra una bodega seleccionada muestra el respectivo mensaje de error.
        /// </summary>
        private void mostrarSeleccion()
        {
            int seleccionada = ddlBodega.SelectedIndex;
            if (seleccionada != -1)
            {
                BodegaObj bodegaTemp = bodegas[seleccionada];
                txtIDBodega.Value = bodegaTemp.getBodegaID().ToString();
                txtNombre.Value = bodegaTemp.getNombre();
                txtUbicacion.Value = bodegaTemp.getUbicacion();
                txtIDBodega.Disabled = true;
                txtNombre.Disabled = false;
                txtUbicacion.Disabled = false;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar una bodega a modificar" + "');", true);
            }
        }

        // <summary>
        ///  Método utilizado para el ingreso y modificación de los datos de la bogeda elegida para ser modificada.
        ///  Además se validan que los campos sean llenados de forma correcta.
        /// </summary>
        private void modificarBodega()
        {
            try
            {
                if (String.IsNullOrEmpty(txtNombre.Value) || String.IsNullOrEmpty(txtUbicacion.Value))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos vacíos, por favor ingrese lo que se le solicita" + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + capaNegocios.modificarBodega(int.Parse(txtIDBodega.Value), txtNombre.Value, txtUbicacion.Value) + "'); window.location='" + Request.ApplicationPath + "Administrador/Bodega/BodegaAdministrador.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos vacíos, por favor ingrese lo que se le solicita" + "');", true);
            }
        }

        /// <summary>
        ///  Evento del DropDownList para mostrar los datos que este tiene.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccion();
        }

        /// <summary>
        ///  Evento de clickear el botón modificar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modificarBodega();
        }
    }
}