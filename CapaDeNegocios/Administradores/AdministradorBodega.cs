using CapaAccesoDatos;
using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Administradores
{
    public class AdministradorBodega
    {
        CapaAccesoDatos.ControladorBD accesoBD = new CapaAccesoDatos.ControladorBD();
        BodegaObj bodega = new BodegaObj();

        /// <summary>
        /// Este método se encarga de enviar la información necesaria para hacer un añadido
        /// en la base de datos.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="ubicacion"></param>
        /// <returns>true si no existieron problemas de ejecución, de lo contrario false.</returns>
        public Boolean crearBodega(String nombre, String ubicacion)
        {
            try
            {
                accesoBD.agregarBodega(nombre, ubicacion);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Método encargado de recoger los datos de una bodega en modo de lista desde
        /// la base de datos, recorrer uno por uno esa lista y cada una convertirla en uno de los 
        /// objetos de entidada a usar. 
        /// </summary>
        /// <returns></returns>
        public List<BodegaObj> obtenerListaBodegas()
        {
            List<Bodega> listaBDbodega = accesoBD.buscarBodegas();
            List<BodegaObj> listaBodegasObj = new List<BodegaObj>();
            if (listaBDbodega.Count > 0)
            {
                foreach (Bodega bodegaTemp in listaBDbodega)
                {
                    if (bodegaTemp.activoSN == true)
                    {
                        BodegaObj bodegaObj = bodega.getBodegaObj(bodegaTemp.bodegaID, bodegaTemp.nombre, bodegaTemp.ubicacion, bodegaTemp.activoSN);
                        listaBodegasObj.Add(bodegaObj);
                    }

                }
            }
            return listaBodegasObj;
        }

        /// <summary>
        /// Este método recibe los datos necesarios para enviar los mismos datos a la base de datos
        /// y que se haga una actualización de datos exitosa. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="ubicacion"></param>
        /// <returns>Una cadena de String con un mensaje del resultado</returns>
        public String modificarBodega(int id, String nombre, String ubicacion)
        {
            Boolean modificado;
            modificado = accesoBD.modificarBodega(id, nombre, ubicacion);
            if (modificado)
            {
                return "La bodega se modificó correctamente.";
            }
            return "La bodega no puede modificarse con esos valores, intente nuevamente.";
        }

        /// <summary>
        /// Una vez seleccionada la opción de eliminar y solicitada la confirmación al usuario
        /// se envía su ID para ser localizado en la base y proceder con el borrado. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>>Una cadena de String con un mensaje del resultado</returns>
        public String confirmarEliminacion(int id)
        {
            if (validarDependencia(id))
            {
                return "No se puede eliminar una bodega con productos asociados, por favor revise.";
            }
            else if (accesoBD.eliminarBodega(id))
            {
                return "La bodega se eliminó correctamente.";
            }
            return "La bodega no se pudo eliminar.";
        }

        /// <summary>
        /// Método que se encarga de validar que no existan dependencias existentes para proceder
        /// con un posible eliminado de entidad.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true si se encontraron dependencias, de lo contrario false.</returns>
        private Boolean validarDependencia(int id)
        {
            List<BodegaProducto> listaBodegaProducto;
            listaBodegaProducto = accesoBD.buscarBodegaXProducto();
            foreach (BodegaProducto productoTemp in listaBodegaProducto)
            {
                if (productoTemp.FK_bodegaID == id)
                {
                    if (productoTemp.cantidad > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
