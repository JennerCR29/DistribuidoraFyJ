using CapaAccesoDatos;
using CapaDeNegocios.Objetos;
using CapaDeNegocios.Patrones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Administradores
{
    public class AdministradorCliente
    {
        CapaAccesoDatos.ControladorBD accesoBD = new CapaAccesoDatos.ControladorBD();
        AdministradorCuenta adminCuenta = new AdministradorCuenta();
        AdministradorFactura admiFactura = new AdministradorFactura();
        ClienteObj cliente = new ClienteObj();
        FabricaCliente fabrica = new FabricaCliente();

        /// <summary>
        /// Este método se encarga de enviar la información necesaria para hacer un añadido
        /// en la base de datos.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="contacto"></param>
        /// <param name="fkRuta"></param>
        /// <returns>true si no existieron problemas de ejecución, de lo contrario false.</returns>
        public Boolean crearCliente(string nombre, string contacto, int fkRuta)
        {
            try
            {
                accesoBD.agregarCliente(nombre, contacto, fkRuta);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Método encargado de recoger los datos de un cliente en modo de lista desde
        /// la base de datos, recorrer uno por uno esa lista y cada una convertirla en uno de los 
        /// objetos de entidad a usar. 
        /// </summary>
        /// <returns>Lista de ClienteObj</returns>
        public List<ClienteObj> obtenerListaClientes()
        {
            List<Cliente> listaBDcliente = accesoBD.buscarClientes();
            List<ClienteObj> listaClienteObj = new List<ClienteObj>();
            PersonaObj personaTemp;
            if (listaBDcliente.Count > 0)
            {
                foreach (Cliente clienteTemp in listaBDcliente)
                {
                    if (clienteTemp.activoSN)
                    {
                        personaTemp = fabrica.crearPersona("Cliente");
                        ClienteObj clienteObj = new ClienteObj(clienteTemp.clienteID, clienteTemp.contacto, clienteTemp.FK_rutaID, clienteTemp.activoSN);
                        Persona persona = accesoBD.obtenerPersona(clienteTemp.FK_personaID);
                        clienteObj.id = persona.personaID;
                        clienteObj.nombre = persona.nombre;
                        listaClienteObj.Add(clienteObj);
                    }
                }
            }
            return listaClienteObj;
        }

        /// <summary>
        /// Este método recibe los datos necesarios para enviar los mismos datos a la base de datos
        /// y que se haga una actualización de datos exitosa. 
        /// </summary>
        /// <param name="personaID"></param>
        /// <param name="clienteID"></param>
        /// <param name="contacto"></param>
        /// <param name="nombre"></param>
        /// <param name="fkRuta"></param>
        /// <returns>Una cadena de String con un mensaje del resultado</returns>
        public String modificarCliente(int personaID, int clienteID, string contacto, string nombre, int fkRuta)
        {
            Boolean modificado;
            modificado = accesoBD.modificarCliente(personaID, clienteID, contacto, nombre, fkRuta);
            if (modificado)
            {
                return "El cliente se modificó correctamente.";
            }
            return "El cliente no puede modificarse con esos valores, intente nuevamente.";
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
                return "No se puede eliminar un cliente con cuentas que contengan saldo pendiente, por favor revise.";
                
            }
            else if (accesoBD.eliminarCliente(id))
            {
                return "El cliente se eliminó correctamente.";
            }
            else
            {
                return "El cliente no se pudo eliminar.";
            }
            
            
        }

        /// <summary>
        /// Método que se encarga de validar que no existan dependencias existentes para proceder
        /// con un posible eliminado de entidad.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true si se encontraron dependencias, de lo contrario false.</returns>
        private Boolean validarDependencia(int id)
        {
            List<CuentaObj> listaCuentas;
            List<FacturaObj> listaFacturas;
            listaCuentas = adminCuenta.obtenerListaCuentas();
            listaFacturas = admiFactura.obtenerListaFacturas();
            foreach (CuentaObj cuentaTemp in listaCuentas)
            {
                if (cuentaTemp.getFkClienteID() == id)
                {
                    if (cuentaTemp.getTipo().Equals("Crédito"))
                    {
                        foreach (FacturaObj facturaTemp in listaFacturas)
                        {
                            if (facturaTemp.getFKCuentaID() == id)
                            {
                                if(facturaTemp.getSaldo() > 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
