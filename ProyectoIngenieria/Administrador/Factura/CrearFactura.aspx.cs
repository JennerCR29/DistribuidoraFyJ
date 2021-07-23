using CapaDeNegocios.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Administrador.Factura
{
    public partial class CrearFactura : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorCliente capaNegociosCliente = new CapaDeNegocios.Administradores.AdministradorCliente();
        CapaDeNegocios.Administradores.AdministradorCuenta capaNegociosCuenta = new CapaDeNegocios.Administradores.AdministradorCuenta();
        CapaDeNegocios.Administradores.AdministradorFactura capaNegociosFactura = new CapaDeNegocios.Administradores.AdministradorFactura();
        CapaDeNegocios.Administradores.AdministradorRuta capaNegociosRuta = new CapaDeNegocios.Administradores.AdministradorRuta();
        CapaDeNegocios.Administradores.AdministradorUsuario capaNegociosUsuario = new CapaDeNegocios.Administradores.AdministradorUsuario();
        CapaDeNegocios.Administradores.AdministradorProducto capaNegociosProducto = new CapaDeNegocios.Administradores.AdministradorProducto();
        String nombreUsuario;
        decimal subtotalFactura = 0;
        CuentaObj cuentaTemp;
        int agregado = 0;
        List<ClienteObj> clientes;
        List<CuentaObj> cuentas;
        List<FacturaObj> listaFacturas;
        List<CuentaObj> cuentasXcliente = new List<CuentaObj>();
        List<FacturaObj> facturasXcliente = new List<FacturaObj>();
        List<UsuarioObj> listaUsuarios = new List<UsuarioObj>();
        List<ClienteObj> clientesXagente = new List<ClienteObj>();
        List<CuentaObj> listaXcliente = new List<CuentaObj>();
        List<ProductoObj> listaProductosDisponibles = new List<ProductoObj>();
        List<ProductoObj> listaProductosCompleta;
        List<LineaPedidoObj> lineaPedidos = new List<LineaPedidoObj>();
        List<LineaPedidoTemp> lineaDetalle = new List<LineaPedidoTemp>();
        List<int> cantidadesXproductosTemp = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            nombreUsuario = (string)(Session["nombreUsuario"]);
            cuentas = capaNegociosCuenta.obtenerListaCuentas();
            clientes = capaNegociosCliente.obtenerListaClientes();

            if (!IsPostBack)
            {
                Session["subtotalFactura"] = 0;
                Session["listaLineasPedido"] = 0;
                Session["cantidadesXproductoTemp"] = null;
                txtDescuentoFactura.Value = "0";
                llenarDropDownListClientes();
                llenarDropDownListCuentas();
                llenarDropDownListProductos();
                cuentaSeleccionada();
            }
        }

        /// <summary>
        /// Método encargado de llenar el DropDownList con todos los clientes relaciones con el nombre de usuario que se traen desde la base de datos.
        /// </summary>
        private void llenarDropDownListClientes()
        {
            int rutaActualID = 0;
            if (capaNegociosRuta.obtenerRuta(nombreUsuario) != null)
            {
                rutaActualID = capaNegociosRuta.obtenerRuta(nombreUsuario).getRutaID();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " El usuario aún no tiene una ruta asignada." + "'); window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
                return;
            }
            if (clientes.Count > 0)
            {
                foreach (var clienteTemp in clientes)
                {
                    foreach (var cuentaTemp in cuentas)
                    {
                        CuentaObj cuentaT = cuentaTemp;
                        cuentasXcliente.Add(cuentaT);
                        if (cuentaTemp.getFkClienteID() == clienteTemp.getClienteID() && !clientesXagente.Contains(clienteTemp) && clienteTemp.getFkRuta() == rutaActualID)
                        {
                            ddlCliente.Items.Add(clienteTemp.nombre);
                            ClienteObj clienteT = clienteTemp;
                            clientesXagente.Add(clienteT);
                        }
                    }
                }
                Session["listaCuentasXcliente"] = cuentasXcliente;
                Session["listaClientesXagente"] = clientesXagente;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " La base de datos no contiene ningún cliente." + "'); window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
            }
        }

        /// <summary>
        /// Método encargado de llenar el DropDownList de Cuentas que están realacionadas con los clientes del usuario que ingreso al sistema.
        /// </summary>
        private void llenarDropDownListCuentas()
        {
            clientesXagente = (List<ClienteObj>)Session["listaClientesXagente"];
            ClienteObj clienteTemp = new ClienteObj();

            if (ddlCliente.SelectedIndex != -1)
            {
                clienteTemp = clientesXagente[ddlCliente.SelectedIndex];
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " El usuario no tiene ningún cliente asignado, o el cliente no contiene ninguna cuenta asignada." + "'); window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
            }
            listaXcliente.Clear();
            ddlCuenta.Items.Clear();
            cuentas = capaNegociosCuenta.obtenerListaCuentas();
            foreach (CuentaObj c in cuentas)
            {
                if (c.getFkClienteID() == clienteTemp.getClienteID())
                {
                    CuentaObj cuentaNueva = c;
                    listaXcliente.Add(cuentaNueva);
                }
            }
            if (listaXcliente.Count > 0)
            {
                foreach (var c in listaXcliente)
                {
                    ddlCuenta.Items.Add(c.getId().ToString() + "-" + c.getTipo());
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " La base de datos no contiene ninguna cuenta asociada al cliente." + "'); window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
            }
            Session["listaListaXcliente"] = listaXcliente;
            cuentaSeleccionada();
        }

        /// <summary>
        /// Método encargado de llenar los datos que se necesitan conforme se selecciona una cuenta.
        /// </summary>
        private void cuentaSeleccionada()
        {
            listaXcliente = (List<CuentaObj>)Session["listaListaXcliente"];
            decimal saldoTotal = 0;
            if (ddlCuenta.SelectedIndex != -1)
            {
                cuentaTemp = listaXcliente[ddlCuenta.SelectedIndex];
                listaFacturas = capaNegociosFactura.obtenerListaFacturas();
                foreach (FacturaObj facturaTemp in listaFacturas)
                {
                    if (facturaTemp.getFKCuentaID() == cuentaTemp.getId())
                    {
                        saldoTotal += facturaTemp.getSaldo();
                    }
                }


                txtSaldoCuenta.Value = saldoTotal.ToString();
                ddlProducto.Enabled = true;
                txtCantidad.Disabled = false;
                txtDescuentoUnitario.Disabled = false;
                txtDescuentoUnitario.Value = "0";
                txtCantidad.Value = "0";
                btnAgregarLinea.Enabled = true;
            }
        }

        /// <summary>
        /// Método encargado de llenar el DropDownList de facturas que están relacionadas a los clientes del usuario que ingresó al sistema.
        /// </summary>
        private void llenarDropDownListProductos()
        {
            UsuarioObj usuarioTemp = capaNegociosUsuario.obtenerUsuarioNombre(nombreUsuario);
            listaProductosCompleta = capaNegociosProducto.obtenerListaProductos();
            foreach (ProductoObj productoTemp in listaProductosCompleta)
            {
                if (capaNegociosProducto.obtenerCantidadDisp(productoTemp.getID(), usuarioTemp.getFkBodega()) > 0)
                {
                    listaProductosDisponibles.Add(productoTemp);
                    if (Session["cantidadesXproductoTemp"] == null)
                    {
                        cantidadesXproductosTemp.Add(capaNegociosProducto.obtenerCantidadDisp(productoTemp.getID(), usuarioTemp.getFkBodega()));
                    }
                }
            }
            if (Session["cantidadesXproductoTemp"] != null)
            {
                cantidadesXproductosTemp = (List<int>)Session["cantidadesXproductoTemp"];
            }
            if (listaProductosDisponibles.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " No hay productos disponibles en su bodega." + "'); window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
            }
            else
            {
                foreach (ProductoObj productoTemp in listaProductosDisponibles)
                {
                    ddlProducto.Items.Add(productoTemp.getNombre());
                }
                Session["cantidadesXproductoTemp"] = cantidadesXproductosTemp;
                Session["productosDisponibles"] = listaProductosDisponibles;
                llenarPrecioXProducto();
            }

        }

        /// <summary>
        /// Método encargado de revisar todos los productos que se encuentran en la bodega relacionada al usuario.
        /// Además agrega al DataGridView los productos que se desean comprar, y valida que la cantidad de productos solicitada no sea mayor
        /// a la cantidad que se encuentra en la bodega relacionada al usuario. Así mismo valida otras opciones por ejemplo que el descuento
        /// no sobrepase el solicitado por el cliente.
        /// </summary>
        /// <returns>Retorna un booleano true si se agregó con éxito la linea de pedido, de lo contrario un false.</returns>
        private bool nuevaLineaPedido()
        {
            listaProductosDisponibles = (List<ProductoObj>)Session["productosDisponibles"];
            cantidadesXproductosTemp = (List<int>)Session["cantidadesXproductoTemp"];
            int cantidadDisponible;
            decimal subtotal = 0;
            decimal descuento = 0;
            decimal subtotalMin = 0;
            ProductoObj productoTemp = listaProductosDisponibles[ddlProducto.SelectedIndex];
            int cantidadXproducto = cantidadesXproductosTemp[ddlProducto.SelectedIndex];
            int nuevaCantidad;
            UsuarioObj usuarioTemp = capaNegociosUsuario.obtenerUsuarioNombre(nombreUsuario);
            if (Decimal.Parse(txtSaldoCuenta.Value) >= 600000)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + "El saldo de la cuenta de crédito se encuentra al límite, inténtelo con otra cuenta." + "'); window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
            }
            if (ddlProducto.SelectedIndex != -1)
            {
                if (String.IsNullOrEmpty(txtCantidad.Value))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos vacíos, favor revise" + "');", true);
                }
                else
                {
                    if (txtCantidad.Value.ToString().Equals("0"))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "La cantidad debe de ser mayor a 0." + "');", true);
                        return false;
                    }
                    if (!txtCantidad.Value.ToCharArray().All(Char.IsDigit) || !txtDescuentoUnitario.Value.ToCharArray().All(Char.IsDigit))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Existen datos que solo reciben datos numéricos, favor revise" + "');", true);
                        return false;
                    }
                    else
                    {
                        cantidadDisponible = capaNegociosProducto.obtenerCantidadDisp(productoTemp.getID(), usuarioTemp.getFkBodega());
                        if (cantidadXproducto < Convert.ToInt32(txtCantidad.Value))
                        {
                            txtCantidad.Value = "0";
                            String mensaje = "La cantidad que intenta agregar es mayor que la existente en bodega. En existencia: " + cantidadXproducto;
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + mensaje + "');", true);
                            return false;
                        }
                        else
                        {
                            subtotal = (productoTemp.getPrecioVenta() * Convert.ToInt32(txtCantidad.Value));
                            subtotalMin = (productoTemp.getPrecioBaseVenta() * Convert.ToInt32(txtCantidad.Value));
                            if (Convert.ToInt32(txtDescuentoUnitario.Value) != 0)
                            {
                                descuento = Convert.ToDecimal(Convert.ToDecimal(txtDescuentoUnitario.Value) / 100);
                                decimal rebaja = (subtotal * descuento);
                                subtotal = (subtotal - rebaja);
                                if (subtotal <= subtotalMin)
                                {
                                    txtDescuentoUnitario.Value = "0";
                                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "El descuento que desea aplicar sobrepasa el precio base." + "');", true);
                                    return false;
                                }

                            }
                            if (Session["subtotalFactura"].ToString().Equals("0"))
                            {
                                subtotalFactura += subtotal;
                                Session["subtotalFactura"] = subtotalFactura;
                            }
                            else
                            {
                                subtotalFactura = (decimal)Session["subtotalFactura"];
                                subtotalFactura += subtotal;
                                Session["subtotalFactura"] = subtotalFactura;
                            }
                            nuevaCantidad = cantidadXproducto - Convert.ToInt32(txtCantidad.Value);
                            cantidadesXproductosTemp[ddlProducto.SelectedIndex] = nuevaCantidad;
                            LineaPedidoObj nuevaLinea = new LineaPedidoObj();
                            LineaPedidoTemp nuevaLineaDetalle = new LineaPedidoTemp();
                            nuevaLineaDetalle.descuento = Byte.Parse(txtDescuentoUnitario.Value);
                            nuevaLineaDetalle.nombreProducto = productoTemp.getNombre();
                            nuevaLineaDetalle.precioUnitario = productoTemp.getPrecioVenta();
                            nuevaLineaDetalle.subtotal = subtotal;
                            nuevaLineaDetalle.cantidad = Convert.ToInt32(txtCantidad.Value);
                            nuevaLinea = nuevaLinea.getLineaPedidoObj(1, subtotal, Convert.ToInt32(txtCantidad.Value), Convert.ToInt32(txtDescuentoUnitario.Value), 1, productoTemp.getID());
                            if (Session["listaLineasPedido"].ToString().Equals("0"))
                            {
                                lineaPedidos.Add(nuevaLinea);
                                lineaDetalle.Add(nuevaLineaDetalle);
                                Session["ListaPedidos"] = lineaPedidos;
                                Session["listaLineasPedido"] = lineaDetalle;
                                Session["cantidadesXproductoTemp"] = cantidadesXproductosTemp;
                                ListaOrdenes.DataSource = lineaDetalle;
                                ListaOrdenes.DataBind();
                            }
                            else
                            {
                                lineaDetalle = (List<LineaPedidoTemp>)Session["listaLineasPedido"];
                                lineaPedidos = (List<LineaPedidoObj>)Session["ListaPedidos"];
                                lineaPedidos.Add(nuevaLinea);
                                lineaDetalle.Add(nuevaLineaDetalle);
                                Session["listaLineasPedido"] = lineaDetalle;
                                Session["ListaPedidos"] = lineaPedidos;
                                Session["cantidadesXproductoTemp"] = cantidadesXproductosTemp;
                                ListaOrdenes.DataSource = lineaDetalle;
                                ListaOrdenes.DataBind();
                            }
                        }
                    }
                }
                return true;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Debe seleccionar un producto." + "');", true);
                return false;
            }
        }

        /// <summary>
        /// Método encargado de aplicar el debido descuento al subtotal, para que así pueda retornar el total con el descuento aplicado.
        /// </summary>
        /// <returns>Retorna una variable tipo decimal con el total modificado.</returns>
        private decimal aplicarDescuento()
        {
            decimal descuento = 0;
            decimal subtotal = 0;
            subtotal = Convert.ToDecimal(txtSubtotalFactura.Value);
            decimal total = subtotal;
            if (Convert.ToInt32(txtDescuentoFactura.Value) > 20)
            {
                txtDescuentoFactura.Value = "0";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "El descuento no puede ser mayor a 20%" + "');", true);
            }
            else if (Convert.ToInt32(txtDescuentoFactura.Value) != 0)
            {
                descuento = Convert.ToDecimal(Convert.ToDecimal(txtDescuentoFactura.Value) / 100);
                decimal rebaja = (subtotal * descuento);
                total = (subtotal - rebaja);
            }
            return total;
        }

        /// <summary>
        /// Método encargado de ingresar al espacio de texto del precio unitario, el precio unitario de cada producto que se escoja.
        /// </summary>
        private void llenarPrecioXProducto()
        {
            listaProductosDisponibles = (List<ProductoObj>)Session["productosDisponibles"];
            if (ddlProducto.SelectedIndex != -1)
            {
                txtPrecioUnitario.Value = listaProductosDisponibles[ddlProducto.SelectedIndex].getPrecioVenta().ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " El usuario no tiene ningún cliente asignado" + "'); window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
            }
        }
        protected void ListaOrdenes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        /// <summary>
        /// Evento relacionado a la acción de borrar una fila del DataGridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ListaOrdenes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int indice = e.RowIndex;
            eliminarFila(indice);
        }

        /// <summary>
        /// Método encargado de recibir el índice de la linea que desea eliminar del DataGridView, y de sobreescribir todos los espacios que se deben de modificar.
        /// </summary>
        /// <param name="indice"></param>
        private void eliminarFila(int indice)
        {
            lineaDetalle = (List<LineaPedidoTemp>)Session["listaLineasPedido"];
            lineaPedidos = (List<LineaPedidoObj>)Session["listaPedidos"];
            cantidadesXproductosTemp = (List<int>)Session["cantidadesXproductoTemp"];
            listaProductosDisponibles = (List<ProductoObj>)Session["productosDisponibles"];
            decimal subtotalEliminar = lineaDetalle[indice].subtotal;
            int devolverCantidad = lineaDetalle[indice].cantidad;
            string nombreProducto = lineaDetalle[indice].nombreProducto;
            int indiceBuscado = 0;
            foreach (ProductoObj productoTemp in listaProductosDisponibles)
            {
                if (productoTemp.getNombre().Equals(nombreProducto))
                {
                    cantidadesXproductosTemp[indiceBuscado] = cantidadesXproductosTemp[indiceBuscado] + devolverCantidad;
                }
                else
                {
                    indiceBuscado++;
                }
            }
            lineaDetalle.RemoveAt(indice);
            lineaPedidos.RemoveAt(indice);
            decimal subtotalEntrante = (decimal)Session["subtotalFactura"];
            subtotalFactura = subtotalEntrante - subtotalEliminar;
            txtSubtotalFactura.Value = subtotalFactura.ToString();
            txtTotalFactura.Value = aplicarDescuento().ToString();
            Session["subtotalFactura"] = subtotalFactura;
            Session["listaLineasPedido"] = lineaDetalle;
            Session["listaPedidos"] = lineaPedidos;
            ListaOrdenes.DataSource = lineaDetalle;
            ListaOrdenes.DataBind();
        }

        /// <summary>
        /// Evento relacionado al DropDownList de los clientes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDropDownListCuentas();
        }

        /// <summary>
        /// Evento relacionado al DropDownList de cuentas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            cuentaSeleccionada();
        }

        /// <summary>
        /// Evento relacionado al DropDownList de productos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarPrecioXProducto();
        }

        /// <summary>
        /// Evento relacionado al evento del boton de agregar linea de pedido. Si se realizó la agregación de linea de pedido con éxito 
        /// se restablecen los valores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            try
            {
                if (nuevaLineaPedido())
                {
                    txtCantidad.Value = "0";
                    txtDescuentoUnitario.Value = "0";
                    btnConfirmar.Enabled = true;
                    txtSubtotalFactura.Value = subtotalFactura.ToString();
                    txtTotalFactura.Value = aplicarDescuento().ToString();
                }
                else
                {
                    txtCantidad.Value = "0";
                    txtDescuentoUnitario.Value = "0";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Ocurrió un error, intente agregar de nuevo" + "');", true);
            }
        }

        /// <summary>
        /// Evento relacionado con el boton de generar factura.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            lineaPedidos = (List<LineaPedidoObj>)Session["listaPedidos"];
            if (lineaPedidos.Count <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "No hay productos suficientes para generar una factura." + "');", true);
            }
            else if (Convert.ToInt32(txtDescuentoFactura.Value) <= 20)
            {
                generarFactura();
                agregarLineas();
            }
            else
            {
                txtDescuentoFactura.Value = "0";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "El descuento no puede ser mayor a 20%" + "');", true);
            }

        }

        /// <summary>
        /// Método encargado de enviar a la base de datos la factura con todos los datos llenos conforme edito el usuario.
        /// </summary>
        private void generarFactura()
        {
            listaXcliente = (List<CuentaObj>)Session["listaListaXcliente"];
            CuentaObj cuentaTemp = listaXcliente[ddlCuenta.SelectedIndex];
            if (cuentaTemp.getTipo().Equals("Crédito"))
            {
                agregado = capaNegociosFactura.crearFactura(Int32.Parse(txtDescuentoFactura.Value), decimal.Parse(txtTotalFactura.Value), decimal.Parse(txtSaldoCuenta.Value) + decimal.Parse(txtTotalFactura.Value), cuentaTemp.getId());
            }
            else
            {
                agregado = capaNegociosFactura.crearFactura(Int32.Parse(txtDescuentoFactura.Value), decimal.Parse(txtTotalFactura.Value), 0, cuentaTemp.getId());
            }

            if (agregado != -1)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Alerta:" + " La factura fue agregada con éxito. Consecutivo de la factura: " + agregado + "'); window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "Mensaje" + "La factura NO se pudo agregar " + "; window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
            }
        }

        /// <summary>
        /// Método encargado de enviar a la base de datos las lineas de pedido que se agregaron a la factura.
        /// </summary>
        private void agregarLineas()
        {
            lineaPedidos = (List<LineaPedidoObj>)Session["listaPedidos"];
            UsuarioObj usuarioTemp = capaNegociosUsuario.obtenerUsuarioNombre(nombreUsuario);
            try
            {
                foreach (LineaPedidoObj linea in lineaPedidos)
                {
                    linea.setFkFacturaID(agregado);
                }
                capaNegociosFactura.crearLineaPedido(lineaPedidos, usuarioTemp.getFkBodega());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "Mensaje" + "La factura NO se pudo agregar " + "; window.location='" + Request.ApplicationPath + "Administrador/Factura/FacturaAdministrador.aspx';", true);
            }

        }

    }

}
public partial class LineaPedidoTemp
{
    public string nombreProducto { get; set; }
    public decimal precioUnitario { get; set; }
    public int cantidad { get; set; }
    public decimal subtotal { get; set; }
    public int descuento { get; set; }

    override
   public string ToString()
    {
        return "Producto: " + nombreProducto + "\nPrecio unitario: ¢" + precioUnitario + "\nCantidad: " + cantidad + "\nSubtotal: ¢" + subtotal;
    }
}