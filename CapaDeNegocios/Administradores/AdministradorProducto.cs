using CapaAccesoDatos;
using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Administradores
{
    public class AdministradorProducto
    {
        CapaAccesoDatos.ControladorBD accesoBD = new CapaAccesoDatos.ControladorBD();
        ProductoObj producto = new ProductoObj();


        /// <summary>
        /// Este método se encarga de enviar la información necesaria para hacer un añadido
        /// en la base de datos de un producto nuevo.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="tipo"></param>
        /// <param name="precioCosto"></param>
        /// <param name="precioCostoAgente"></param>
        /// <param name="precioVenta"></param>
        /// <param name="precioBaseVenta"></param>
        /// <param name="cantidad"></param>
        /// <returns>true si no existieron problemas de ejecución, de lo contrario false.</returns>
        public String crearProducto(string nombre, string descripcion, string tipo, decimal precioCosto, decimal precioCostoAgente, decimal precioVenta, decimal precioBaseVenta, int cantidad)
        {
            try
            {
                if (precioBaseVenta >= precioCosto && precioCostoAgente >= precioCosto && precioVenta >= precioCosto)
                {
                    if (precioBaseVenta >= precioCostoAgente)
                    {
                        if (precioBaseVenta <= precioVenta)
                        {
                            accesoBD.agregarProducto(nombre, descripcion, tipo, precioCosto, precioCostoAgente, precioVenta, precioBaseVenta, cantidad);
                            return "AGREGADO";
                        }
                        else
                        {
                            return "BASEMAYORVENTA";
                        }
                    }
                    else
                    {
                        return "BASEMAYORCOSTOAGENTE";
                    }
                }
                else
                {
                    return "COSTOMAYOR";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "NOSEPUDOAGREGAR";
            }

        }


        /// <summary>
        /// Método encargado de recoger los datos de una ruta en modo de lista desde
        /// la base de datos, recorrer uno por uno esa lista y cada una convertirla en uno de los 
        /// objetos de entidad a usar.
        /// </summary>
        /// <returns>lista de ProductoObj</returns>
        public List<ProductoObj> obtenerListaProductos()
        {
            List<Producto> listaBDproducto = accesoBD.buscarProductos();
            List<ProductoObj> listaProductosObj = new List<ProductoObj>();
            if (listaBDproducto.Count > 0)
            {
                foreach (Producto productoTemp in listaBDproducto)
                {
                    if (productoTemp.activoSN)
                    {
                        ProductoObj productoObj = producto.getProductoObj(productoTemp.productoID, productoTemp.nombre, productoTemp.descripcion, productoTemp.tipo, productoTemp.precioCosto, productoTemp.precioCostoAgente, productoTemp.precioVenta, productoTemp.precioBaseVenta, productoTemp.activoSN);
                        listaProductosObj.Add(productoObj);
                    }

                }
            }
            return listaProductosObj;
        }


        /// <summary>
        /// Este método recibe los datos necesarios para enviar los mismos datos a la base de datos
        /// y que se haga una actualización necesaria y exitosa de datos. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="tipo"></param>
        /// <param name="precioCosto"></param>
        /// <param name="precioCostoAgente"></param>
        /// <param name="precioVenta"></param>
        /// <param name="precioBaseVenta"></param>
        /// <param name="cantidad"></param>
        /// <returns>Una cadena de String con un mensaje del resultado</returns>
        public String modificarProducto(int id, string nombre, string descripcion, string tipo, decimal precioCosto, decimal precioCostoAgente, decimal precioVenta, decimal precioBaseVenta, int cantidad)
        {
            Boolean modificado;


            if (precioBaseVenta >= precioCosto && precioCostoAgente >= precioCosto && precioVenta >= precioCosto)
            {
                if (precioBaseVenta >= precioCostoAgente)
                {
                    if (precioBaseVenta <= precioVenta)
                    {
                        modificado = accesoBD.modificarProducto(id, nombre, tipo, descripcion, precioCosto, precioCostoAgente, precioVenta, precioBaseVenta, cantidad);
                        if (modificado)
                        {
                            return "El producto se modificó correctamente.";
                        }
                        else
                        {
                            return "El producto no puede modificarse con esos valores, intente nuevamente.";
                        }
                    }
                    else
                    {
                        return "El producto no puede modificarse, el precio base venta es mayor que el precio venta.";
                    }
                }
                else
                {
                    return "El producto no puede modificarse, el precio base venta es menor que el precio costo de agente.";
                }
            }
            else
            {
                return "El producto no puede modificarse, el precio costo debe de ser el menor.";
            }


        }


        /// <summary>
        /// Método encargado de obtener la cantidad disponible de
        /// productos dentro de una bodega en específico
        /// </summary>
        /// <param name="productoID"></param>
        /// <param name="bodegaOrigen"></param>
        /// <returns>Número de productos disponibles.</returns>
        public int obtenerCantidadDisp(int productoID, int bodegaOrigen)
        {
            return accesoBD.obtenerCantidadProductoEsp(bodegaOrigen, productoID);
        }


        /// <summary>
        /// Método que se encarga de trasladar cantidad de producto de una bodega a otra.
        /// </summary>
        /// <param name="productoID"></param>
        /// <param name="bodegaOrigen"></param>
        /// <param name="bodegaDestino"></param>
        /// <param name="cantidad"></param>
        /// <returns>Una cadena de valores que específican el éxito o no del método.</returns>
        public String despacharProducto(int productoID, int bodegaOrigen, int bodegaDestino, int cantidad)
        {
            int cantidadActualizada = 0;
            cantidadActualizada = obtenerCantidadDisp(productoID, bodegaOrigen);
            if (cantidad > cantidadActualizada)
            {
                return "La cantidad que desea despachar no está disponible. Cantidad disponible: " + cantidadActualizada;
            }
            else
            {
                cantidadActualizada = cantidadActualizada - cantidad;
            }
            if (accesoBD.despacharProducto(bodegaOrigen, bodegaDestino, productoID, cantidad, cantidadActualizada))
            {
                return "Los productos se despacharon con éxito";
            }
            else
            {
                return "Los productos no se despacharon, intente nuevamente";
            }

        }


        /// <summary>
        /// Una vez seleccionada la opción de eliminar y solicitada la confirmación al usuario
        /// se envía su ID para ser localizado en la base y proceder con el borrado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Una cadena de String con un mensaje del resultado</returns>
        public String confirmarEliminacion(int id)
        {
            if (accesoBD.eliminarProducto(id))
            {
                return "El producto se eliminó correctamente.";
            }
            return "El producto no se pudo eliminar.";
        }
    }
}
