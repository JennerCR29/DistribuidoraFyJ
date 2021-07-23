using System;
using System.Collections.Generic;
using System.Text;
using CapaDeNegocios.Objetos;

namespace CapaDeNegocios
{
    /// <summary>
    /// Clase que contiene el patrón de diseño fábrica e implementa la interfaz de persona objeto.
    /// </summary>
    public class FabricaUsuario : PersonaInterface
    {
        /// <summary>
        /// Método que se encarga de crear una persona dependiendo del string que le ingrese, en este caso crea objetos usuarios.
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns>Retorna el nuevo usuario, de lo contrario retorna un valor nulo.</returns>
        public PersonaObj crearPersona(string tipo)
        {
            if (tipo.Equals("Usuario"))
            {
                return new UsuarioObj();
            }
            return null;
        }
    }
}
