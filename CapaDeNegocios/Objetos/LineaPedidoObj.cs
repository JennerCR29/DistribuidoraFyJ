using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Objetos
{
    /// <summary>
    /// Clase del objeto LineaPedido.
    /// </summary>
    public class LineaPedidoObj
    {
        Administradores.AdministradorProducto admiProducto = new Administradores.AdministradorProducto();
        private int lineaPedidoID;
        private decimal subtotal;
        private int cantidad;
        private int descuento;
        private int fkFacturaID;
        private int fkProductoID;
        public ProductoObj producto { get; set; }

        private LineaPedidoObj(int lineaPedidoID, decimal subtotal, int cantidad, int descuento, int fkFacturaID, int fkProductoID)
        {
            this.lineaPedidoID = lineaPedidoID;
            this.subtotal = subtotal;
            this.cantidad = cantidad;
            this.descuento = descuento;
            this.fkFacturaID = fkFacturaID;
            this.fkProductoID = fkProductoID;
            this.producto = crearObjetoProducto(fkProductoID);
        }

        public LineaPedidoObj()
        {

        }

        

        public int getLineaPedidoID()
        {
            return this.lineaPedidoID;
        }

        public decimal getSubtotal()
        {
            return this.subtotal;
        }

        public int getCantidad()
        {
            return this.cantidad;
        }

        public int getDescuento()
        {
            return this.descuento;
        }

        public void setFkFacturaID(int fkFacturaID)
        {
            this.fkFacturaID = fkFacturaID;
        }
        public int getFkFacturaID()
        {
            return this.fkFacturaID;
        }

        public int getFkProductoID()
        {
            return this.fkProductoID;
        }

        private ProductoObj crearObjetoProducto(int fk_ProductoID)
        {
            List<ProductoObj> productos = admiProducto.obtenerListaProductos();
            foreach(ProductoObj productoTemp in productos)
            {
                if(productoTemp.getID() == fk_ProductoID)
                {
                    return productoTemp;
                }
            }
            return null;
        }

        public LineaPedidoObj getLineaPedidoObj(int lineaPedidoID, decimal subtotal, int cantidad, int descuento, int fkFacturaID, int fkProductoID)
        {
            LineaPedidoObj lineaPedido = new LineaPedidoObj(lineaPedidoID, subtotal, cantidad, descuento, fkFacturaID, fkProductoID);
            return lineaPedido;
        }
    }
}
