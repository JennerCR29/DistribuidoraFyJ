using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Patrones
{
    /// <summary>
    /// Clase que contiene el patrón de diseño fábrica e implementa la interfaz de persona objeto.
    /// </summary>
    public class FabricaCliente : PersonaInterface
    {
        /// <summary>
        /// Método que se encarga de crear una persona dependiendo del string que le ingrese, en este caso crea objetos clientes.
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns>Retorna el nuevo cliente, de lo contrario retorna un valor nulo.</returns>
        public PersonaObj crearPersona(string tipo)
        {
            if (tipo.Equals("Cliente"))
            {
                return new ClienteObj();
            }
            return null;
        }
    }
}
