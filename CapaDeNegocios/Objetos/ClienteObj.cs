using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Objetos
{
    /// <summary>
    /// Clase del objeto cliente que hereda la interfaz del objeto persona para la implementación del patrón de diseño fábrica.
    /// </summary>
    public class ClienteObj : PersonaObj
    {
        private int clienteID;
        private string contacto;
        private int fkRuta;
        private bool activoSN;
        public ClienteObj()
        {
        }

        public ClienteObj(int clienteID, string contacto, int fkRuta, bool activoSN)
        {
            this.clienteID = clienteID;
            this.contacto = contacto;
            this.fkRuta = fkRuta;
            this.activoSN = activoSN;
        }

        public int getClienteID()
        {
            return this.clienteID;
        }

        public string getContacto()
        {
            return this.contacto;
        }

        public int getFkRuta()
        {
            return this.fkRuta;
        }

        public bool getActivoSN()
        {
            return this.activoSN;
        }
    }
}
