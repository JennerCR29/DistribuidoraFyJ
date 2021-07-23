using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDeNegocios.Objetos;

namespace ProyectoIngenieria.Administrador.Bodega
{
    public partial class EliminarBodega : System.Web.UI.Page
    {

        CapaDeNegocios.Administradores.AdministradorBodega capaNegocios = new CapaDeNegocios.Administradores.AdministradorBodega();
        List<BodegaObj> bodegas;

        protected void Page_Load(object sender, EventArgs e)
        {
            bodegas = capaNegocios.obtenerListaBodegas();
            if (!IsPostBack)
            {
                llenarDropDownList();
                mostrarSeleccion();
            }
        }

        /// <summary>
        /// El método llenarPicker se conecta con la base de datos para poder obtener una lista
        /// de todas las bodegas y posteriormente agrega al DropDownList todas las bodegas de
        /// la lista.
        /// </summary>
        private void llenarDropDownList()
        {
            bodegas = capaNegocios.obtenerListaBodegas();
            if (bodegas.Count > 0)
            {
                foreach (var bodegaTemp in bodegas)
                {
                    bodega.Items.Add(bodegaTemp.getNombre() + "/ " + bodegaTemp.getUbicacion());
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ninguna bodega." + "');", true);
            }
        }

        /// <summary>
        /// El método mostrarSeleccion muestra la información de ID, nombre y ubicación
        /// de la bodega seleccionada.
        /// </summary>
        private void mostrarSeleccion()
        {
            int seleccionado = bodega.SelectedIndex;
            if (seleccionado != -1)
            {
                BodegaObj bodegaTemp = bodegas[seleccionado];
                txtID.Value = bodegaTemp.getBodegaID().ToString();
                txtNombre.Value = bodegaTemp.getNombre();
                txtUbicacion.Value = bodegaTemp.getUbicacion();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar una bodega a eliminar" + "');", true);
            }
        }

        /// <summary>
        /// El método eliminarBodega se encarga de verfificar que una bodega haya sido seleccionada,
        /// además, confirma si se puede o no llevar a cabo su eliminación y muestra al usuario si pudo
        /// o no eliminarse correctamente.
        /// </summary>
        public void eliminarBodega()
        {
            try
            {
                if (bodega.SelectedIndex == -1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Se debe seleccionar una bodega." + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + capaNegocios.confirmarEliminacion(int.Parse(txtID.Value)) + "'); window.location='" + Request.ApplicationPath + "Administrador/Bodega/BodegaAdministrador.aspx';", true);
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
            eliminarBodega();
        }

        /// <summary>
        /// Evento para hacer click en el DropDownList para seleccionar la bodega.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccion();
        }


    }
}