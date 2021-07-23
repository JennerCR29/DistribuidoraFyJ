using CapaDeNegocios.Objetos;
using ProyectoIngenieria.Administrador.Cuenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Agente.CierreCaja
{
    public partial class CierreDeCaja : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorFactura capaNegociosFactura = new CapaDeNegocios.Administradores.AdministradorFactura();
        CapaDeNegocios.Administradores.AdministradorUsuario capaNegociosUsuario = new CapaDeNegocios.Administradores.AdministradorUsuario();
        CapaDeNegocios.Administradores.AdministradorCuenta capaNegociosCuenta = new CapaDeNegocios.Administradores.AdministradorCuenta();
        string nombreUsuario;
        List<CuentaObj> cuentas;
        List<FacturaObj> facturas;
        List<FacturaObj> facturasXagente = new List<FacturaObj>();
        UsuarioObj usuarioTemp;
        decimal subtotal = 0;
        decimal saldo = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            nombreUsuario = Session["nombreUsuario"].ToString();
            usuarioTemp = capaNegociosUsuario.obtenerUsuarioNombre(nombreUsuario);
            txtFecha.Value = DateTime.Now.ToString();
            txtID.Value = usuarioTemp.getNombreUsuario();
            txtNombre.Value = usuarioTemp.nombre;
            listarFacturaXAgente();
            obtenerSubtotal();
            txtTotal.Value = subtotal.ToString();
            txtSaldo.Value = saldo.ToString();
            llenarDataGrid();
        }
        private void listarFacturaXAgente()
        {
            facturas = capaNegociosFactura.obtenerListaFacturas();
            cuentas = capaNegociosCuenta.obtenerListaCuentas();
            foreach (FacturaObj facturaTemp in facturas)
            {
                foreach (CuentaObj cuentaTemp in cuentas)
                {
                    if (facturaTemp.getFKCuentaID() == cuentaTemp.getId())
                    {
                        if (cuentaTemp.getFkNombreUsuario().Equals(usuarioTemp.getNombreUsuario()))
                        {
                            if (facturaTemp.getFKInformeID() == 0)
                            {
                                FacturaObj facturaNueva = facturaTemp;
                                facturasXagente.Add(facturaNueva);
                            }

                        }
                    }
                }
            }
            if (facturasXagente.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " La base de datos no contiene ninguna factura realizada por el usuario." + "'); window.location='" + Request.ApplicationPath + "Agente/Factura/FacturaAgente.aspx';", true);
                return;
            }
            else
            {
                Session["facturaXagenteCierreDeCaja"] = facturasXagente;
            }

        }
        private void obtenerSubtotal()
        {
            facturasXagente = (List<FacturaObj>)Session["facturaXagenteCierreDeCaja"];

            cuentas = capaNegociosCuenta.obtenerListaCuentas();
            if (facturasXagente == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " La base de datos no contiene ninguna factura realizada por el usuario." + "'); window.location='" + Request.ApplicationPath + "Agente/Factura/FacturaAgente.aspx';", true);
                return;
            }
            foreach (FacturaObj facturaTemp in facturasXagente)
            {
                foreach (CuentaObj cuentaTemp in cuentas)
                {
                    if (facturaTemp.getFKCuentaID() == cuentaTemp.getId())
                    {
                        if (cuentaTemp.getTipo().Equals("Contado"))
                        {
                            subtotal += facturaTemp.getTotal();
                        }
                        else if (facturaTemp.getTotal() == facturaTemp.getSaldo())
                        {
                            saldo += facturaTemp.getSaldo();
                        }
                        else if (facturaTemp.getTotal() > facturaTemp.getSaldo())
                        {
                            decimal abono = facturaTemp.getTotal() - facturaTemp.getSaldo();
                            subtotal += abono;
                            saldo += facturaTemp.getSaldo();
                        }
                    }
                }

            }
            Session["totalCierreCaja"] = subtotal;
            Session["saldoCierreCaja"] = saldo;
        }
        /// <summary>
        /// Método encargado de que en el GridView se puedan apreciar todas las facturas enlazadas a la cuenta
        /// seleccionada
        /// </summary>
        private void llenarDataGrid()
        {
            facturasXagente = (List<FacturaObj>)Session["facturaXagenteCierreDeCaja"];
            List<facturaTempClass> listaTemp = new List<facturaTempClass>();
            if (facturasXagente == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " La base de datos no contiene ninguna factura realizada por el usuario." + "'); window.location='" + Request.ApplicationPath + "Agente/Factura/FacturaAgente.aspx';", true);
                return;
            }
            foreach (FacturaObj factura in facturasXagente)
            {
                facturaTempClass facturaTemp = new facturaTempClass();
                facturaTemp.facturaID = factura.getFacturaID();
                facturaTemp.fecha = factura.getFecha();
                facturaTemp.descuento = factura.getDescuento();
                facturaTemp.total = factura.getTotal();
                listaTemp.Add(facturaTemp);
            }
            ListaFacturas.DataSource = listaTemp;
            ListaFacturas.DataBind();
        }
        private void crearInforme()
        {
            nombreUsuario = Session["nombreUsuario"].ToString();
            subtotal = (decimal) Session["totalCierreCaja"];
            saldo = (decimal) Session["saldoCierreCaja"];
            facturasXagente = (List<FacturaObj>)Session["facturaXagenteCierreDeCaja"];
            int nuevoID;
            try
            {
                nuevoID = capaNegociosFactura.agregarInforme(saldo, subtotal, nombreUsuario);
                if (nuevoID != -1)
                {
                    foreach (FacturaObj facturaTemp in facturasXagente)
                    {
                        capaNegociosFactura.asignarFKInforme(facturaTemp.getFacturaID(), nuevoID);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + " El informe semanal fue generado con éxito." + "'); window.location='" + Request.ApplicationPath + "Agente/Cuenta/CuentaAgente.aspx';", true);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error:" + " Algo salió mal, inténtelo de nuevo." + "'); window.location='" + Request.ApplicationPath + "Agente/Cuenta/CuentaAgente.aspx';", true);

            }

        }
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            crearInforme();
        }
    }
}