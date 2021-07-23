using CapaAccesoDatos;
using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.Administradores
{
    public class AdministradorFactura
    {
        CapaAccesoDatos.ControladorBD accesoBD = new CapaAccesoDatos.ControladorBD();
        FacturaObj factura = new FacturaObj();
        LineaPedidoObj lineaPedido = new LineaPedidoObj();


        /// <summary>
        /// Método encargado de enviar los datos al controlador de base de datos para que pueda agregar una factura a la base de datos.
        /// </summary>
        /// <param name="descuento"></param>
        /// <param name="total"></param>
        /// <param name="saldo"></param>
        /// <param name="FKcuentaID"></param>
        /// <returns>Retorna el id que devolvio el controlador de base de datos.</returns>
        public int crearFactura(int descuento, decimal total, decimal saldo, int FKcuentaID)
        {
            int id;
            try
            {
                id = accesoBD.crearFactura(descuento, total, saldo, FKcuentaID);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Método encargado de recoger los datos de las facturas en modo de lista desde
        /// la base de datos, recorrer uno por uno esa lista y cada una convertirla en uno de los 
        /// objetos de la entidad a usar, además de listarlo en otra lista con la restricción de 
        /// que la factura se encuentra entre las fechas especificadas por parámetro.
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<FacturaObj> obtenerListaFacturasFecha(DateTime fechaInicio, DateTime fechaFin)
        {

            int mes = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int anho = DateTime.Now.Year;
            if (mes == 1)
            {
                mes = 12;
                anho = anho - 1;
            }
            else
            {
                mes = mes - 1;
            }
            if (day == 31 || day == 30 || day == 29)
            {
                day = 28;
            }
            DateTime fechalimite = new DateTime(anho, mes, day);
            DateTime fechaTemporal;
            List<Factura> listaBDfactura = accesoBD.obtenerFacturas();
            List<FacturaObj> listaFacturaObj = new List<FacturaObj>();
            //sin fechas, ultimos 30 dias
            if (fechaInicio.Year == 1 && fechaFin.Year == 1)
            {
                if (listaBDfactura.Count > 0)
                {
                    foreach (Factura facturaTemp in listaBDfactura)
                    {
                        fechaTemporal = new DateTime(facturaTemp.fecha.Year, facturaTemp.fecha.Month, facturaTemp.fecha.Day);
                        if (facturaTemp.activoSN && fechaTemporal.CompareTo(fechalimite) >= 0)
                        {
                            FacturaObj facturaObj = factura.getFacturaObj(facturaTemp.facturaID, facturaTemp.fecha, facturaTemp.descuento, facturaTemp.total, facturaTemp.saldo, facturaTemp.FK_cuentaID, facturaTemp.FK_informeID, facturaTemp.activoSN);
                            listaFacturaObj.Add(facturaObj);
                        }

                    }
                }
                return listaFacturaObj;
            }
            // solo fecha fin
            else if(fechaInicio.Year == 1 && fechaFin.Year != 1)
            {
                if (listaBDfactura.Count > 0)
                {
                    foreach (Factura facturaTemp in listaBDfactura)
                    {
                        fechaTemporal = new DateTime(facturaTemp.fecha.Year, facturaTemp.fecha.Month, facturaTemp.fecha.Day);
                        if (facturaTemp.activoSN && fechaTemporal.CompareTo(fechaFin) <= 0)
                        {
                            FacturaObj facturaObj = factura.getFacturaObj(facturaTemp.facturaID, facturaTemp.fecha, facturaTemp.descuento, facturaTemp.total, facturaTemp.saldo, facturaTemp.FK_cuentaID, facturaTemp.FK_informeID, facturaTemp.activoSN);
                            listaFacturaObj.Add(facturaObj);
                        }

                    }
                }
                return listaFacturaObj;
            }
            // solo fecha inicio
            else if (fechaInicio.Year != 1 && fechaFin.Year == 1)
            {
                if (listaBDfactura.Count > 0)
                {
                    foreach (Factura facturaTemp in listaBDfactura)
                    {
                        fechaTemporal = new DateTime(facturaTemp.fecha.Year, facturaTemp.fecha.Month, facturaTemp.fecha.Day);
                        if (facturaTemp.activoSN && fechaTemporal.CompareTo(fechaInicio) >= 0)
                        {
                            FacturaObj facturaObj = factura.getFacturaObj(facturaTemp.facturaID, facturaTemp.fecha, facturaTemp.descuento, facturaTemp.total, facturaTemp.saldo, facturaTemp.FK_cuentaID, facturaTemp.FK_informeID, facturaTemp.activoSN);
                            listaFacturaObj.Add(facturaObj);
                        }

                    }
                }
                return listaFacturaObj;
            }
            //ambas fechas
            else
            {
                if (listaBDfactura.Count > 0)
                {
                    foreach (Factura facturaTemp in listaBDfactura)
                    {
                        fechaTemporal = new DateTime(facturaTemp.fecha.Year, facturaTemp.fecha.Month, facturaTemp.fecha.Day);
                        if (facturaTemp.activoSN && fechaTemporal.CompareTo(fechaInicio) >= 0 && fechaTemporal.CompareTo(fechaFin) < 0)
                        {
                            FacturaObj facturaObj = factura.getFacturaObj(facturaTemp.facturaID, facturaTemp.fecha, facturaTemp.descuento, facturaTemp.total, facturaTemp.saldo, facturaTemp.FK_cuentaID, facturaTemp.FK_informeID, facturaTemp.activoSN);
                            listaFacturaObj.Add(facturaObj);
                        }

                    }
                }
                return listaFacturaObj;
            }
        }

        /// <summary>
        /// Método encargado de recoger los datos de las facturas en modo de lista desde
        /// la base de datos, recorrer uno por uno esa lista y cada una convertirla en uno de los 
        /// objetos de la entidad a usar. 
        /// </summary>
        /// <returns>Retorna lista con todas las facturas transformadas para poder utilizarlas en la capa de presentación.</returns>
        public List<FacturaObj> obtenerListaFacturas()
        {
            List<Factura> listaBDfactura = accesoBD.obtenerFacturas();
            List<FacturaObj> listaFacturaObj = new List<FacturaObj>();
            if (listaBDfactura.Count > 0)
            {
                foreach (Factura facturaTemp in listaBDfactura)
                {
                    if (facturaTemp.activoSN)
                    {
                        FacturaObj facturaObj = factura.getFacturaObj(facturaTemp.facturaID, facturaTemp.fecha, facturaTemp.descuento, facturaTemp.total, facturaTemp.saldo, facturaTemp.FK_cuentaID, facturaTemp.FK_informeID, facturaTemp.activoSN);
                        listaFacturaObj.Add(facturaObj);
                    }

                }
            }
            return listaFacturaObj;
        }

        /// <summary>
        /// Una vez seleccionada la opción de eliminar y solicitada la confirmación al usuario
        /// se envía su ID para ser localizado en la base y proceder con el borrado. 
        /// </summary>
        /// <param name="facturaID"></param>
        /// <returns>Una cadena de String con un mensaje del resultado</returns>
        public string confirmarEliminacion(int facturaID)
        {
            if (accesoBD.eliminarFactura(facturaID))
            {
                return "La factura se eliminó correctamente.";
            }
            return "La factura no se pudo eliminar.";
        }

        /// <summary>
        /// Método encargado de enviar la o las lineas de pedido que se ingresaron en la capa de presentación hacia el controlador de la base de datos
        /// para que pueda agregarse a la base de datos, además se envía el identificador de la bodega para poder actualizar la cantidad de productos
        /// que hay en la misma.
        /// </summary>
        /// <param name="listaPedido"></param>
        /// <param name="fkBodega"></param>
        /// <returns>Retorna un valor booleano true si se realizó con éxito, de lo contario se retorna un false.</returns>
        public Boolean crearLineaPedido(List<LineaPedidoObj> listaPedido, int fkBodega)
        {
            try
            {
                foreach (var lineaTemp in listaPedido)
                {
                    accesoBD.crearLineaPedido(lineaTemp.getSubtotal(), lineaTemp.getCantidad(), lineaTemp.getDescuento(), lineaTemp.getFkFacturaID(), lineaTemp.getFkProductoID(), fkBodega);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Método encargado de recoger los datos de las lineas de pedido en modo de lista desde
        /// la base de datos, recorrer uno por uno esa lista y cada una convertirla en uno de los 
        /// objetos de la entidad a usar. 
        /// </summary>
        /// <param name="facturaID"></param>
        /// <returns>Retorna la lista creada con todas las lineas de pedido que venían desde la base de datos.</returns>
        public List<LineaPedidoObj> obtenerListaLineaPedidos(int facturaID)
        {
            List<LineaPedido> listaBDLinea = accesoBD.obtenerLineasPedidos();
            List<LineaPedidoObj> listaLineaPedidoObj = new List<LineaPedidoObj>();
            if (listaBDLinea.Count > 0)
            {
                foreach (LineaPedido lineaPedidoTemp in listaBDLinea)
                {
                    LineaPedidoObj lineaObj = lineaPedido.getLineaPedidoObj(lineaPedidoTemp.lineaPedidoID, lineaPedidoTemp.subtotal, lineaPedidoTemp.cantidad, lineaPedidoTemp.descuento, lineaPedidoTemp.FK_facturaID, lineaPedidoTemp.FK_productoID);
                    if (lineaObj.getFkFacturaID() == facturaID)
                    {
                        listaLineaPedidoObj.Add(lineaObj);
                    }
                }
            }
            return listaLineaPedidoObj;
        }

        /// <summary>
        /// Método encargado de enviar todos los datos hacia el controlador para que pueda agregar 
        /// a la base de datos el informe, además de que se envía el identificador del usuario para que se pueda
        /// relacionar y quede en evidencia quien hizo el informe.
        /// </summary>
        /// <param name="saldo"></param>
        /// <param name="total"></param>
        /// <param name="fkUsuario"></param>
        /// <returns>Retorna el identificador del informe agregado.</returns>
        public int agregarInforme(decimal saldo, decimal total, string fkUsuario)
        {
            try
            {
                return accesoBD.agregarInforme(saldo, total, fkUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Método encargado de enviar al controlador los datos necesarios para que pueda asignarle al 
        /// informe las facturas correspondietes a ese informe.
        /// </summary>
        /// <param name="facturaId"></param>
        /// <param name="fkInforme"></param>
        /// <returns>Retorna un valor booleano true si se realizó con éxito, de lo contrario retorna false.</returns>
        public Boolean asignarFKInforme(int facturaId, int fkInforme)
        {
            try
            {
                accesoBD.asignarFkinforme(facturaId, fkInforme);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Método encargado de obtener el informe de la base de datos relacionado 
        /// con el nombre de usuario ingresado en la capa de presentación, además que 
        /// lo convierte al objeto de entidad a usar.
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns>Retorna un </returns>
        public InformeObj obtenerInforme(string nombreUsuario)
        {
            Informe informeTemp = accesoBD.obtenerInformeXAgente(nombreUsuario);
            InformeObj informeNuevo = new InformeObj();
            informeNuevo = informeNuevo.getInformeObj(informeTemp.informeID, informeTemp.fecha, informeTemp.saldo, informeTemp.total, informeTemp.FK_nombreUsuario);
            return informeNuevo;
        }
    }
}
