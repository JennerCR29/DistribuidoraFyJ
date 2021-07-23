using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Administrador.Producto
{
    public partial class DespacharProducto : System.Web.UI.Page
    {

        CapaDeNegocios.Administradores.AdministradorProducto capaNegocios = new CapaDeNegocios.Administradores.AdministradorProducto();
        CapaDeNegocios.Administradores.AdministradorBodega capaNegocioB = new CapaDeNegocios.Administradores.AdministradorBodega();
        List<ProductoObj> productos;
        BodegaObj bodegaOrigenTemp;
        BodegaObj bodegaDestinoTemp;
        List<BodegaObj> bodegas;
        ProductoObj productoTemp;
        protected void Page_Load(object sender, EventArgs e)
        {
            productos = capaNegocios.obtenerListaProductos();
            bodegas = capaNegocioB.obtenerListaBodegas();
            if (!IsPostBack)
            {
                llenarDropDownList();
                llenerDropDownListBodegaOrigen();
                llenerDropDownListBodegaDestino();
                mostrarSeleccion();
            }
        }

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

        private void llenerDropDownListBodegaOrigen()
        {
            bodegas = capaNegocioB.obtenerListaBodegas();
            if (bodegas.Count > 0)
            {
                foreach (var bodegaTemp in bodegas)
                {
                    ddlBodegaOrigen.Items.Add(bodegaTemp.getNombre() + " / " + bodegaTemp.getUbicacion());
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ninguna bodega." + "');", true);
            }
        }

        private void llenerDropDownListBodegaDestino()
        {
            bodegas = capaNegocioB.obtenerListaBodegas();
            if (bodegas.Count > 0)
            {
                foreach (var bodegaTemp in bodegas)
                {
                    ddlBodegaDestino.Items.Add(bodegaTemp.getNombre() + " / " + bodegaTemp.getUbicacion());
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La base de datos no contiene ninguna bodega." + "');", true);
            }
        }

        private int mostrarSeleccion()
        {
            int seleccionado = ddlProducto.SelectedIndex;
            if (seleccionado != -1)
            {
                productoTemp = productos[seleccionado];
                return productoTemp.getID();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar un producto a despachar." + "');", true);
                return productoTemp.getID();
            }
        }

        private BodegaObj seleccionBodegaOrigen()
        {
            int seleccionado = ddlBodegaOrigen.SelectedIndex;
            if (seleccionado != -1)
            {
                bodegaOrigenTemp = bodegas[seleccionado];
                return bodegaOrigenTemp;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar la bodega de origen." + "');", true);
                return bodegaOrigenTemp;
            }
        }

        private BodegaObj seleccionBodegaDestino()
        {
            int seleccionado = ddlBodegaDestino.SelectedIndex;
            if (seleccionado != -1)
            {
                bodegaDestinoTemp = bodegas[seleccionado];
                return bodegaDestinoTemp;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar la bodega de destino." + "');", true);
                return bodegaDestinoTemp;
            }
        }

        protected void producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccion();
        }

        protected void bodegaOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionBodegaOrigen();
        }

        protected void bodegaDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionBodegaDestino();
        }

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            aplicarDespecho(seleccionBodegaOrigen(), seleccionBodegaDestino());
        }

        private void aplicarDespecho(BodegaObj origen, BodegaObj destino)
        {
            if (ddlProducto.SelectedIndex == -1 || ddlBodegaOrigen.SelectedIndex == -1 || ddlBodegaDestino.SelectedIndex == -1 || String.IsNullOrEmpty(txtCantidad.Value))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos vacíos, favor ingresar todos los datos." + "');", true);
            }
            else
            {
                if (origen.getBodegaID() == destino.getBodegaID())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "No se puede despachar a la misma bodega, por favor revisar." + "');", true);
                }
                else
                {
                    String mensaje = capaNegocios.despacharProducto(mostrarSeleccion(), origen.getBodegaID(), destino.getBodegaID(), Convert.ToInt32(txtCantidad.Value));
                    if (mensaje.Contains("éxito"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + mensaje + "'); window.location='" + Request.ApplicationPath + "Administrador/Producto/ProductoAdministrador.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + mensaje + "');", true);
                    }
                }
            }
        }


    }
}