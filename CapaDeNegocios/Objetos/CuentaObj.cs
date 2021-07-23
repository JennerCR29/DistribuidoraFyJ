using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Objetos
{
    /// <summary>
    /// Clase del objeto cuenta.
    /// </summary>
    public class CuentaObj
    {
        private int id;
        private string tipo;
        private DateTime fecha;
        private int fkClienteID;
        private string FKnombreUsuario;

        public CuentaObj()
        {

        }

        private CuentaObj(int id, string tipo, DateTime fecha, int fkClienteID, string fKnombreUsuario)
        {
            this.id = id;
            this.tipo = tipo;
            this.fecha = fecha;
            this.fkClienteID = fkClienteID;
            FKnombreUsuario = fKnombreUsuario;
        }

        public int getId()
        {
            return this.id;
        }

        public string getTipo()
        {
            return this.tipo;
        }

        public DateTime getFecha()
        {
            return this.fecha;
        }

        public int getFkClienteID()
        {
            return this.fkClienteID;
        }

        public string getFkNombreUsuario()
        {
            return this.FKnombreUsuario;
        }

        public CuentaObj getCuentaObj(int id, string tipo, DateTime fecha, int fkClienteID, string fKnombreUsuario)
        {
            CuentaObj cuenta = new CuentaObj(id, tipo, fecha, fkClienteID, fKnombreUsuario);
            return cuenta;
        }
    }
}
