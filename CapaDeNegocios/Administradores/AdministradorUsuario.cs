using CapaAccesoDatos;
using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Administradores
{
    public class AdministradorUsuario
    {
        CapaAccesoDatos.ControladorBD accesoBD = new CapaAccesoDatos.ControladorBD();
        FabricaUsuario fabrica = new FabricaUsuario();

        /// <summary>
        /// Método que se encarga de obtener un objeto usuario de la base de datos, mediante el nombre de usuario que se ingreso en la capa de presentación
        /// con el fin de validar si existe o no el usuario para el inicio de sesión. Al mismo tiempo se crea un objeto Usuario de la aplicación con los datos 
        /// recogidos de la base de datos.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>Retorna el un usuario temporal con todos los datos de la base de datos, de no encontrar el usuario se retorna un valor nulo.</returns>
        public UsuarioObj obtenerUsuarioNombre(string nombre)
        {
            try
            {
                Usuario usuario = accesoBD.obtenerUsuarioNombre(nombre);
                Persona persona = accesoBD.obtenerPersona(usuario.FK_personaID);
                if (usuario != null && usuario.activoSN)
                {
                    UsuarioObj usuarioTemp = new UsuarioObj(usuario.nombreUsuario, usuario.contrasena, usuario.FK_rolID, usuario.FK_bodegaID, usuario.activoSN);
                    usuarioTemp.nombre = persona.nombre;
                    usuarioTemp.id = persona.personaID;
                    return usuarioTemp;
                }
                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Método encargado de recoger los datos ingresados en la capa de presentación, y enviarselos a la capa de base de datos para añadirlos a la base de datos. 
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="identificador"></param>
        /// <param name="contrasena"></param>
        /// <param name="fkBodega"></param>
        /// <param name="fkRol"></param>
        /// <returns>Retorna un boolean el cual confirma si se pudo agregar el usuario o no.</returns>
        public Boolean crearUsuario(string nombreUsuario, string identificador, string contrasena, int fkBodega, int fkRol)
        {
            try
            {
                accesoBD.agregarUsuario(nombreUsuario, identificador, contrasena, fkBodega, fkRol);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Método encargado de obtener la lista de usuarios de la base de datos y así mismo pasa todos los datos al objeto usuario de la aplicación para poder utilizarlos y añade a una lista de
        /// usuario de la aplicación.
        /// </summary>
        /// <returns>Retorna la lista creada de tipo Usuario con todos los usuarios de la base de datos.</returns>
        public List<UsuarioObj> obtenerListaUsuarios()
        {
            List<Usuario> listaBDusuario = accesoBD.buscarUsuarios();
            List<UsuarioObj> listaUsuariosObj = new List<UsuarioObj>();
            PersonaObj personaTemp;
            if (listaBDusuario.Count > 0)
            {
                foreach (Usuario usuarioTemp in listaBDusuario)
                {
                    if (usuarioTemp.activoSN)
                    {
                        personaTemp = fabrica.crearPersona("Usuario");
                        UsuarioObj usuarioObj = new UsuarioObj(usuarioTemp.nombreUsuario, usuarioTemp.contrasena, usuarioTemp.FK_rolID, usuarioTemp.FK_bodegaID, usuarioTemp.activoSN);
                        Persona persona = accesoBD.obtenerPersona(usuarioTemp.FK_personaID);
                        usuarioObj.id = persona.personaID;
                        usuarioObj.nombre = persona.nombre;
                        listaUsuariosObj.Add(usuarioObj);
                    }

                }
            }
            return listaUsuariosObj;
        }

        /// <summary>
        /// Método encargado de recoger los datos ingresados en la capa de presentación para enviarlos a la base de datos y poder modificar al usuario con los nuevos datos.
        /// </summary>
        /// <param name="personaID"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="identificador"></param>
        /// <param name="contrasena"></param>
        /// <param name="fkBodega"></param>
        /// <returns>Retorna un booleano confirmando si se logró modificar el usuario o no.</returns>
        public String modificarUsuario(int personaID, String nombreUsuario, String identificador, String contrasena, int fkBodega)
        {
            Boolean modificado;
            modificado = accesoBD.modificarUsuario(personaID, nombreUsuario, identificador, contrasena, fkBodega);
            if (modificado)
            {
                return "El usuario se modificó correctamente.";
            }
            return "El usuario no puede modificarse con esos valores, intente nuevamente.";
        }

        /// <summary>
        /// Método encargado de recoger el nombre de usuario que se quiere eliminar en la capa de presentación y enviarlo hacia la base de datos.
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns>Retorna un booleano confirmando si se logró eliminar el usuario o no.</returns>
        public string confirmarEliminacion(string nombreUsuario)
        {
            if (accesoBD.eliminarUsuario(nombreUsuario))
            {
                return "El usuario se eliminó correctamente.";
            }
            return "El usuario no se pudo eliminar.";
        }

        /// <summary>
        /// Método encargado de validar el identificador de el usuario que se quiere agregar, se envía el id ingresado en la capa de presentación y lo envía hacia la capa de baso de datos
        /// para ver si está repetido o no.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns>Retorna un booleano confirmando si se repite o no el identificador en la base de datos.</returns>
        public bool validarID(string idUsuario)
        {
            List<Usuario> listaBDusuario = accesoBD.buscarUsuarios();
            if (listaBDusuario.Count > 0)
            {
                foreach (Usuario usuarioTemp in listaBDusuario)
                {
                    if (usuarioTemp.nombreUsuario == idUsuario)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        /// <summary>
        /// Método que se encarga de validar la longitud de la contraseña ingresada en la capa de presentación.
        /// </summary>
        /// <param name="contrasena"></param>
        /// <returns>Retorna un booleano confirmando si la contrañsena es válida o no.</returns>
        public bool validarContrasena(string contrasena)
        {
            if (contrasena.Length <= 7)
            {
                return true;
            }
            return false;
        }

    }
}