using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Objetos
{
    /// <summary>
    /// Clase del objeto ruta.
    /// </summary>
    public class RutaObj
    {
        private int rutaID;
        private String nombre;
        private string fkNombreUsuario;
        private bool activoSN;

        public RutaObj()
        {
        }

        private RutaObj(int rutaID, string nombre, string fkNombreUsuario, bool activoSN)
        {
            this.rutaID = rutaID;
            this.nombre = nombre;
            this.fkNombreUsuario = fkNombreUsuario;
            this.activoSN = activoSN;
        }

        public RutaObj getRuta(int rutaID, string nombre, string fkNombreUsuario, bool activoSN)
        {
            RutaObj ruta = new RutaObj(rutaID, nombre, fkNombreUsuario, activoSN);
            return ruta;
        }

        public int getRutaID()
        {
            return this.rutaID;
        }

        public String getNombre()
        {
            return this.nombre;
        }

        public string getFkNombreUsuario()
        {
            return this.fkNombreUsuario;
        }

        public bool getActivoSN()
        {
            return this.activoSN;
        }
    }
}
