using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Administrador.Producto
{
    public partial class EliminarProducto : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorProducto capaNegocios = new CapaDeNegocios.Administradores.AdministradorProducto();
        List<ProductoObj> productos;
        protected void Page_Load(object sender, EventArgs e)
        {
            productos = capaNegocios.obtenerListaProductos();
            if (!IsPostBack)
            {
                llenarDropDowList();
                mostrarSeleccion();
            }
        }

        private void llenarDropDowList()
        {
            productos = capaNegocios.obtenerListaProductos();
            if (productos.Count > 0)
            {
                foreach (var productoTemp in productos)
                {
                    ddlProducto.Items.Add(productoTemp.getNombre());
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ningún producto." + "');", true);
            }
        }


        private void mostrarSeleccion()
        {
            int seleccionado = ddlProducto.SelectedIndex;
            if (seleccionado != -1)
            {
                ProductoObj productoTemp = productos[seleccionado];
                txtID.Value = productoTemp.getID().ToString();
                txtNombre.Value = productoTemp.getNombre().ToString();
                txtDescripción.Value = productoTemp.getDescripcion().ToString();
                txtTipo.Value = productoTemp.getTipo().ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar una bodega a eliminar" + "');", true);
            }
        }

        protected void producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccion();
        }

        protected void btnEliminar_Clicked(object sender, EventArgs e)
        {
            eliminarProducto();
        }

        private void eliminarProducto()
        {
            try
            {
                if (ddlProducto.SelectedIndex == -1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Se debe seleccionar un producto." + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + capaNegocios.confirmarEliminacion(int.Parse(txtID.Value)) + "'); window.location='" + Request.ApplicationPath + "Administrador/Producto/ProductoAdministrador.aspx';", true);
                }
            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "No se pudo eliminar el producto." + "');", true);
            }
        }
    }
}