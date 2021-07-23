using CapaAccesoDatos;
using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Administradores
{
    public class AdministradorRuta
    {
        CapaAccesoDatos.ControladorBD accesoBD = new CapaAccesoDatos.ControladorBD();
        AdministradorCliente adminCliente = new AdministradorCliente();
        RutaObj ruta = new RutaObj();


        /// <summary>
        /// Este método se encarga de enviar la información necesaria para hacer un añadido
        /// en la base de datos.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="fkUsuario"></param>
        /// <returns>true si no existieron problemas de ejecución, de lo contrario false.</returns>
        public Boolean crearRuta(String nombre, String fkUsuario)
        {
            try
            {
                accesoBD.agregarRuta(nombre, fkUsuario);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }


        /// <summary>
        /// Método encargado de recoger los datos de una ruta en modo de lista desde
        /// la base de datos, recorrer uno por uno esa lista y cada una convertirla en uno de los 
        /// objetos de entidad a usar. 
        /// </summary>
        /// <returns>lista de RutaObj</returns>
        public List<RutaObj> obtenerListaRutas()
        {
            List<Ruta> listaBDruta = accesoBD.buscarRutas();
            List<RutaObj> listaRutasObj = new List<RutaObj>();
            if (listaBDruta.Count > 0)
            {
                foreach (Ruta rutaTemp in listaBDruta)
                {
                    if (rutaTemp.activoSN)
                    {
                        RutaObj rutaObj = ruta.getRuta(rutaTemp.rutaID, rutaTemp.nombre, rutaTemp.FK_nombreUsuario, rutaTemp.activoSN);
                        listaRutasObj.Add(rutaObj);
                    }

                }
            }
            return listaRutasObj;
        }


        /// <summary>
        /// Este método recibe los datos necesarios para enviar los mismos datos a la base de datos
        /// y que se haga una actualización de datos exitosa. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="fkUsuario"></param>
        /// <returns>Una cadena de String con un mensaje del resultado</returns>
        public String modificarRuta(int id, String nombre, String fkUsuario)
        {
            Boolean modificado;
            modificado = accesoBD.modificarRuta(id, nombre, fkUsuario);
            if (modificado)
            {
                return "La ruta se modificó correctamente.";
            }
            return "La ruta no puede modificarse con esos valores, intente nuevamente.";
        }

        /// <summary>
        /// Una vez seleccionada la opción de eliminar y solicitada la confirmación al usuario
        /// se envía su ID para ser localizado en la base y proceder con el borrado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Una cadena de String con un mensaje del resultado</returns>
        public string confirmarEliminacion(int id)
        {
            if (validarDependencia(id))
            {
                return "No se puede eliminar una ruta con clientes asociados, por favor revise.";
            }
            else if (accesoBD.eliminarRuta(id))
            {
                return "La ruta se eliminó correctamente.";
            }
            else
            {
                return "La ruta no se pudo eliminar.";
            }
        }

        /// <summary>
        /// Método encargado de revisar que no hayan clientes entrelazados al identificador de la ruta.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un booleano true si se encontró una dependencia, de lo contrario un false.</returns>
        private Boolean validarDependencia(int id)
        {
            List<ClienteObj> listaClientes;
            listaClientes = adminCliente.obtenerListaClientes();
            foreach (ClienteObj clienteTemp in listaClientes)
            {
                if (clienteTemp.getFkRuta() == id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Método encargado de enviar al controlador el nombre de usuario del usuario que inicio sesión, y recoger la ruta que se encuentra enlazada a este mismo,
        /// además crea un objeto ruta con los datos de la base de datos, el cual es necesitado para poderlo utilizar en la capa de presentación.
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns>Retorna un objeto ruta si se encontró en la base de datos, de lo contrario retorna un null.</returns>
        public RutaObj obtenerRuta(String nombreUsuario)
        {
            List<Ruta> listaBDruta = accesoBD.buscarRutas();
            if (listaBDruta.Count > 0)
            {
                foreach (Ruta rutaTemp in listaBDruta)
                {
                    if (rutaTemp.activoSN && rutaTemp.FK_nombreUsuario.Equals(nombreUsuario))
                    {
                        RutaObj rutaObj = ruta.getRuta(rutaTemp.rutaID, rutaTemp.nombre, rutaTemp.FK_nombreUsuario, rutaTemp.activoSN);
                        return rutaObj;
                    }

                }
            }
            return null;
        }
    }
}
