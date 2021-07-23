using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDeNegocios.Objetos;

namespace ProyectoIngenieria.Administrador.Ruta
{
    public partial class EliminarRuta : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorRuta capaNegocios = new CapaDeNegocios.Administradores.AdministradorRuta();
        List<RutaObj> rutas;
        protected void Page_Load(object sender, EventArgs e)
        {
            rutas = capaNegocios.obtenerListaRutas();
            if (!IsPostBack)
            {
                llenarddl();
                mostrarSeleccion();
            }
        }

        /// <summary>
        /// El método llenarddl se conecta con la base de datos para poder obtener una lista
        /// de todas las rutas y posteriormente agrega al DropDownList todas las rutas de
        /// la lista.
        /// </summary>
        private void llenarddl()
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
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "No hay rutas existentes en la base de datos" + "');", true);
            }
        }

        /// <summary>
        /// Evento para hacer click en el DropDownList para seleccionar el usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlRuta_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccion();
        }

        /// <summary>
        /// El método mostrarSeleccion muestra la información de ID, nombre, agente.
        /// dela ruta seleccionada.
        /// </summary>
        private void mostrarSeleccion()
        {
            int seleccionado = ddlRuta.SelectedIndex;
            if (seleccionado != -1)
            {
                RutaObj rutaTemp = rutas[seleccionado];
                txtID.Value = rutaTemp.getRutaID().ToString();
                txtID.Disabled = true;
                txtNombre.Disabled = true;
                txtAgente.Disabled = true;
                txtNombre.Value = rutaTemp.getNombre();
                txtAgente.Value = rutaTemp.getFkNombreUsuario();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar una ruta a eliminar" + "');", true);
            }
        }

        /// <summary>
        /// El método eliminarRuta se encarga de verfificar que una ruta haya sido seleccionada,
        /// además, confirma si se puede o no llevar a cabo su eliminación y muestra al usuario si pudo
        /// o no eliminarse correctamente.
        /// </summary>
        private void eliminarRuta()
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + capaNegocios.confirmarEliminacion(int.Parse(txtID.Value)) + "'); window.location='" + Request.ApplicationPath + "Administrador/Ruta/RutaAdministrador.aspx';", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "No se pudo eliminar la ruta." + "');", true);
            }
        }

        /// <summary>
        /// Evento de para hacer click en el botón ELIMINAR.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminar_Clicked(object sender, EventArgs e)
        {
            eliminarRuta();
        }
    }
}