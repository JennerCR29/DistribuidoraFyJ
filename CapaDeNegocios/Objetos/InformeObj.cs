using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Objetos
{
    /// <summary>
    /// Clase del objeto Informe.
    /// </summary>
    public class InformeObj
    {
        public int informeID { get; set; }
        public DateTime fecha { get; set; }
        public decimal saldo { get; set; }
        public decimal total { get; set; }
        public string fkUsuario { get; set; }

        public InformeObj()
        {

        }
        private InformeObj(int informeID, DateTime fecha, decimal saldo, decimal total, string fkUsuario)
        {
            this.informeID = informeID;
            this.fecha = fecha;
            this.saldo = saldo;
            this.total = total;
            this.fkUsuario = fkUsuario;
        }

        public InformeObj getInformeObj(int informeID, DateTime fecha, decimal saldo, decimal total, string fkUsuario)
        {
            InformeObj informe = new InformeObj(informeID, fecha, saldo, total, fkUsuario);
            return informe;
        }

    }
}
