using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Administrador.Producto
{
    public partial class RegistrarProducto : System.Web.UI.Page
    {

        CapaDeNegocios.Administradores.AdministradorProducto capaNegocios = new CapaDeNegocios.Administradores.AdministradorProducto();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///  Método utilizado para el ingreso de los productos, donde se realizan las validaciones de los campos: nombre, descripcion, tipo,
        ///  cantidad, precioCosto, precioCostoAgente, precioVenta, precioBaseVenta.
        ///  Dado el caso de que se ingresen datos inválidos se mostrará el respectivo mensaje de error.
        /// </summary>
        private void ingresarProducto()
        {
            try
            {
                if (String.IsNullOrEmpty(txtNombre.Value) || String.IsNullOrEmpty(txtDescripcion.Value) || String.IsNullOrEmpty(txtTipo.Value)
                    || String.IsNullOrEmpty(txtCantidad.Value) || String.IsNullOrEmpty(txtPrecioCosto.Value) || String.IsNullOrEmpty(txtPrecioCostoAgente.Value)
                    || String.IsNullOrEmpty(txtPrecioVenta.Value) || String.IsNullOrEmpty(txtPrecioBaseVenta.Value))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Deben de llenar todos los datos." + "');", true);
                }
                else
                {
                    String registrado = capaNegocios.crearProducto(txtNombre.Value, txtDescripcion.Value, txtTipo.Value, Convert.ToDecimal(txtPrecioCosto.Value),
                        Convert.ToDecimal(txtPrecioCostoAgente.Value), Convert.ToDecimal(txtPrecioVenta.Value), Convert.ToDecimal(txtPrecioBaseVenta.Value),
                        int.Parse(txtCantidad.Value));

                    if (registrado.Equals("AGREGADO"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + "El producto se agregó con éxito." + "'); window.location='" + Request.ApplicationPath + "Administrador/Producto/ProductoAdministrador.aspx';", true);
                    }
                    else
                    {
                        if (registrado.Equals("COSTOMAYOR"))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "No se pudo agregar, el precio costo debe ser el menor, intente nuevamente." + "');", true);

                        }
                        else
                        {
                            if (registrado.Equals("BASEMAYORCOSTOAGENTE"))
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "No se pudo agregar el precio venta agente es menor que el precio base venta, intente nuevamente." + "');", true);

                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "No se pudo agregar el precio venta es mayor que el precio base venta, intente nuevamente." + "');", true);
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
            ingresarProducto();

        }
    }
}