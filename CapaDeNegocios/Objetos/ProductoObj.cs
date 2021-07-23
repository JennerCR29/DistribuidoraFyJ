using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Objetos
{
    /// <summary>
    /// Clase del objeto producto.
    /// </summary>
    public class ProductoObj
    {
        private int productoID;
        public string nombre;
        private string tipo;
        private string descripcion;
        private decimal precioCosto;
        private decimal precioCostoAgente;
        private decimal precioVenta;
        private decimal precioBaseVenta;
        private bool activoSN;

        public ProductoObj()
        {

        }

        private ProductoObj(int iD, string nombre, string descripcion, string tipo, decimal precioCosto, decimal precioCostoAgente, decimal precioVenta, decimal precioBaseVenta, bool activoSN)
        {
            this.productoID = iD;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.tipo = tipo;
            this.precioCosto = precioCosto;
            this.precioCostoAgente = precioCostoAgente;
            this.precioVenta = precioVenta;
            this.precioBaseVenta = precioBaseVenta;
            this.activoSN = activoSN;
        }

        public int getID()
        {
            return this.productoID;
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public String getTipo()
        {
            return this.tipo;
        }

        public String getDescripcion()
        {
            return this.descripcion;
        }

        public Decimal getPrecioCosto()
        {
            return this.precioCosto;
        }

        public Decimal getPrecioCostoAgente()
        {
            return this.precioCostoAgente;
        }

        public Decimal getPrecioVenta()
        {
            return this.precioVenta;
        }

        public Decimal getPrecioBaseVenta()
        {
            return this.precioBaseVenta;
        }
        public bool getActivoSN()
        {
            return this.activoSN;
        }

        public ProductoObj getProductoObj(int iD, string nombre, string descripcion, string tipo, decimal precioCosto, decimal precioCostoAgente, decimal precioVenta, decimal precioBaseVenta, bool activoSN)
        {
            ProductoObj producto = new ProductoObj(iD, nombre, descripcion, tipo, precioCosto, precioCostoAgente, precioVenta, precioBaseVenta, activoSN);
            return producto;
        }
    }
}
