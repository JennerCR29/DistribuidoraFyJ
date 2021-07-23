using CapaAccesoDatos;
using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Administradores
{
    public class AdministradorCuenta
    {
        CapaAccesoDatos.ControladorBD accesoBD = new CapaAccesoDatos.ControladorBD();
        string tipoCuentaStr = "";
        int numeroCuentas = 0;
        List<Cuenta> listaCuentaTemp;
        CuentaObj cuenta = new CuentaObj();

        /// <summary>
        /// Este método se encarga de enviar la información necesaria para hacer un añadido
        /// en la base de datos de la cuenta.
        /// </summary>
        /// <param name="clienteID"></param>
        /// <param name="tipoCuenta"></param>
        /// <param name="nombreUsuario"></param>
        /// <returns>Retorna un valor booleano true si se realizo con éxito, de lo contrario se retorna un false.</returns>
        public bool crearCuenta(int clienteID, int tipoCuenta, string nombreUsuario)
        {
            try
            {
                listaCuentaTemp = accesoBD.obtenerCuentas();

                foreach (Cuenta cuenta in listaCuentaTemp)
                {
                    if (clienteID == cuenta.FK_clienteID && cuenta.tipo.Equals("Contado"))
                    {
                        numeroCuentas = 1;
                    }
                    else if (clienteID == cuenta.FK_clienteID && cuenta.tipo.Equals("Crédito"))
                    {
                        numeroCuentas = numeroCuentas + 2;
                    }

                }

                if (tipoCuenta == 1)
                {
                    tipoCuentaStr = "Contado";
                }
                else
                {
                    tipoCuentaStr = "Crédito";
                }

                if (tipoCuenta == 1 && numeroCuentas != 1 && numeroCuentas != 3)
                {
                    accesoBD.guardarCuenta(clienteID, tipoCuentaStr, nombreUsuario);
                    return true;
                }
                else if (tipoCuenta == 2 && numeroCuentas != 2 && numeroCuentas != 3)
                {
                    accesoBD.guardarCuenta(clienteID, tipoCuentaStr, nombreUsuario);
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Método encargado de recoger los datos de las cuentas en modo de lista desde la base de datos, 
        /// recorrer uno por uno esa lista y cada una convertirla en uno de los objetos de entidad a usar.
        /// </summary>
        /// <returns>Retorna una lista de CuentaObj</returns>
        public List<CuentaObj> obtenerListaCuentas()
        {
            List<Cuenta> listaBDcuenta = accesoBD.obtenerCuentas();
            List<CuentaObj> listaCuentaObj = new List<CuentaObj>();
            if (listaBDcuenta.Count > 0)
            {
                foreach (Cuenta cuentaTemp in listaBDcuenta)
                {
                    CuentaObj cuentaObj = cuenta.getCuentaObj(cuentaTemp.cuentaID, cuentaTemp.tipo, cuentaTemp.fechaCreacion, cuentaTemp.FK_clienteID, cuentaTemp.FK_nombreUsuario);
                    listaCuentaObj.Add(cuentaObj);
                }
            }
            return listaCuentaObj;
        }

        /// <summary>
        /// Método encargado de enviar al controlador de la base de datos, los diferentes datos para poder modificar la cuenta deseada.
        /// </summary>
        /// <param name="facturaID"></param>
        /// <param name="saldo"></param>
        /// <param name="montoAbonar"></param>
        /// <returns>Retorna un true si se logró el cambio con éxito, de lo contrario se retorna un false.</returns>
        public string abonarCuenta(int facturaID, decimal saldo, decimal montoAbonar)
        {
            decimal nuevoSaldo = saldo - montoAbonar;

            if (accesoBD.abonarCuenta(facturaID, nuevoSaldo))
            {
                return "El abono se realizó exitosamente.";
            }
            return "El abono no se pudo realizar.";
        }

    }
}
