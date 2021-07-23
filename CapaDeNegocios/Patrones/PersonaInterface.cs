using System;
using System.Collections.Generic;
using System.Text;
using CapaDeNegocios.Objetos;

namespace CapaDeNegocios
{
    /// <summary>
    /// Interfaz para la creación del patrón de diseño Frábrica. Para la creación de personas.
    /// </summary>
    public interface PersonaInterface
    {
        PersonaObj crearPersona(string tipo);
        
    }
}
