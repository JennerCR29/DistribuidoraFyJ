using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Objetos
{
    /// <summary>
    /// Clase del objeto factura.
    /// </summary>
    public class FacturaObj
    {
        public int facturaID;
        public DateTime fecha;
        public byte descuento;
        public decimal total;
        public decimal saldo;
        private int fkCuentaID;
        private int fkInformeID;
        private bool activoSN;

        public FacturaObj()
        {

        }

        private FacturaObj(int facturaID, DateTime fecha, byte descuento, decimal total, decimal saldo, int fkCuentaID, int fkInformeID, bool activoSN)
        {
            this.facturaID = facturaID;
            this.fecha = fecha;
            this.descuento = descuento;
            this.total = total;
            this.saldo = saldo;
            this.fkCuentaID = fkCuentaID;
            this.fkInformeID = fkInformeID;
            this.activoSN = activoSN;
        }

        public int getFacturaID()
        {
            return this.facturaID;
        }

        public DateTime getFecha()
        {
            return this.fecha;
        }

        public byte getDescuento()
        {
            return this.descuento;
        }

        public decimal getTotal()
        {
            return this.total;
        }

        public decimal getSaldo()
        {
            return this.saldo;
        }

        public int getFKCuentaID()
        {
            return this.fkCuentaID;
        }

        public int getFKInformeID()
        {
            return this.fkInformeID;
        }

        public bool getActivoSN()
        {
            return this.activoSN;
        }

        public FacturaObj getFacturaObj(int facturaID, DateTime fecha, byte descuento, decimal total, decimal saldo, int fkCuentaID, int fkInformeID, bool activoSN)
        {
            FacturaObj factura = new FacturaObj(facturaID, fecha, descuento, total, saldo, fkCuentaID, fkInformeID, activoSN);
            return factura;
        }

        override
        public string ToString()
        {
            return "ID factura: " + getFacturaID().ToString() + "\nFecha: " + getFecha().ToString() + "\nTotal: ¢" + getTotal().ToString() + "\nSaldo en deuda: ¢" + getSaldo().ToString() + "\n";
        }
    }
}
