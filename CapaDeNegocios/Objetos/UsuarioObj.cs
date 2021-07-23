using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Objetos
{
    /// <summary>
    /// Clase del objeto usuario que hereda la interfaz del objeto persona para la implementación del patrón de diseño fábrica.
    /// </summary>
    public class UsuarioObj :PersonaObj
    {
        private string nombreUsuario;
        private string contrasena;
        private int fkRol;
        private int fkBodega;
        private bool activoSN;

        public UsuarioObj()
        {

        }


        public UsuarioObj(string nombreUsuario, string contrasena, int fkRol, int fkBodega, bool activoSN)
        {
            this.nombreUsuario = nombreUsuario;
            this.contrasena = contrasena;
            this.fkRol = fkRol;
            this.fkBodega = fkBodega;
            this.activoSN = activoSN;
        }

        public string getNombreUsuario()
        {
            return this.nombreUsuario;
        }

        public string getContrasena()
        {
            return this.contrasena;
        }

        public int getFkRol()
        {
            return this.fkRol;
        }

        public int getFkBodega()
        {
            return this.fkBodega;
        }

        public bool getActivoSN()
        {
            return this.activoSN;
        }
    }
}
