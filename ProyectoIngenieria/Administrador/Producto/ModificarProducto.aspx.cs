using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Administrador.Producto
{
    public partial class ModificarProducto : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorProducto capaNegocios = new CapaDeNegocios.Administradores.AdministradorProducto();
        List<ProductoObj> productos;
        protected void Page_Load(object sender, EventArgs e)
        {
            productos = capaNegocios.obtenerListaProductos();
            if (!IsPostBack)
            {
                llenarDropDownList();
                mostrarSeleccion();
            }
        }

        /// <summary>
        ///  Método utilizado para el llenado del picker de productos. Si no se encuentra ningún producto se muestra el respectivo mensaje de error.
        /// </summary>
        private void llenarDropDownList()
        {
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

        /// <summary>
        ///  Método donde se muestran los datos del producto elegido en el picker de productos.
        ///  Si no se encuentra un producto seleccionado muestra el respectivo mensaje de error.
        /// </summary>
        private void mostrarSeleccion()
        {
            int seleccionado = ddlProducto.SelectedIndex;
            if (seleccionado != -1)
            {
                ProductoObj productoTemp = productos[seleccionado];
                txtIDProducto.Value = productoTemp.getID().ToString();
                txtNombre.Value = productoTemp.getNombre().ToString();
                txtDescripcion.Value = productoTemp.getDescripcion();
                txtTipo.Value = productoTemp.getTipo();
                txtCantidad.Value = capaNegocios.obtenerCantidadDisp(productoTemp.getID(), 3).ToString();
                txtPrecioCosto.Value = productoTemp.getPrecioCosto().ToString();
                txtPrecioCostoAgente.Value = productoTemp.getPrecioCostoAgente().ToString();
                txtPrecioVenta.Value = productoTemp.getPrecioVenta().ToString();
                txtPrecioBaseVenta.Value = productoTemp.getPrecioVenta().ToString();
                txtIDProducto.Disabled = true;
                txtNombre.Disabled = false;
                txtDescripcion.Disabled = false;
                txtTipo.Disabled = false;
                txtCantidad.Disabled = false;
                txtPrecioCosto.Disabled = false;
                txtPrecioCostoAgente.Disabled = false;
                txtPrecioVenta.Disabled = false;
                txtPrecioBaseVenta.Disabled = false;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar un usuario a modificar" + "');", true);
            }
        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccion();
        }

        /// <summary>
        ///  Método utilizado para el ingreso y modificación de los datos del producto elegido para ser modificado.
        ///  Además se validan que los campos sean llenados de forma correcta.
        /// </summary>
        private void modificarProducto()
        {
            try
            {
                if (String.IsNullOrEmpty(txtNombre.Value) || String.IsNullOrEmpty(txtDescripcion.Value) || String.IsNullOrEmpty(txtTipo.Value)
                     || String.IsNullOrEmpty(txtCantidad.Value) || String.IsNullOrEmpty(txtPrecioCosto.Value) || String.IsNullOrEmpty(txtPrecioCostoAgente.Value)
                     || String.IsNullOrEmpty(txtPrecioVenta.Value) || String.IsNullOrEmpty(txtPrecioBaseVenta.Value))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos vacíos, por favor ingrese lo que se le solicita" + "');", true);
                }
                else
                {
                    String mensaje = capaNegocios.modificarProducto(int.Parse(txtIDProducto.Value), txtNombre.Value, txtTipo.Value, txtDescripcion.Value, Convert.ToDecimal(txtPrecioCosto.Value),
                        Convert.ToDecimal(txtPrecioCostoAgente.Value), Convert.ToDecimal(txtPrecioVenta.Value), Convert.ToDecimal(txtPrecioBaseVenta.Value), int.Parse(txtCantidad.Value));
                    if (mensaje.Contains("correctamente"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + mensaje + "'); window.location='" + Request.ApplicationPath + "Administrador/Producto/ProductoAdministrador.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + mensaje + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Por favor revise los datos ingresados. Hay datos inválidos." + "');", true);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modificarProducto();
        }
    }
}