using CapaDeNegocios.Objetos;
using ProyectoIngenieria.Administrador.Cuenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoIngenieria.Administrador
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        CapaDeNegocios.Administradores.AdministradorUsuario capaNegociosUsuario = new CapaDeNegocios.Administradores.AdministradorUsuario();
        CapaDeNegocios.Administradores.AdministradorFactura capaNegociosFactura = new CapaDeNegocios.Administradores.AdministradorFactura();
        UsuarioObj usuarioTemp;
        List<UsuarioObj> usuarios;
        List<UsuarioObj> agentes = new List<UsuarioObj>();
        List<FacturaObj> facturas;
        List<FacturaObj> facturasXInforme = new List<FacturaObj>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenarDropDownListAgentes();
                mostrarSeleccion();
            }
        }
        private void llenarDropDownListAgentes()
        {
            usuarios = capaNegociosUsuario.obtenerListaUsuarios();
            foreach (UsuarioObj usuarioT in usuarios)
            {
                if (usuarioT.getFkRol() == 2)
                {
                    ddlAgente.Items.Add(usuarioT.getNombreUsuario() + "-" + usuarioT.nombre);
                    agentes.Add(usuarioT);
                }
            }
            Session["agentes"] = agentes;
        }

        private void mostrarSeleccion()
        {
            agentes = (List<UsuarioObj>)Session["agentes"];
            usuarioTemp = agentes[ddlAgente.SelectedIndex];
            facturasXInforme.Clear();
            InformeObj informeNuevo = capaNegociosFactura.obtenerInforme(usuarioTemp.getNombreUsuario());
            if (informeNuevo.informeID == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error: " + "Este agente no cuenta con informe generado de momento. " + "');", true);
                txtFecha.Value = "";
                txtSaldo.Value = "";
                txtTotal.Value = "";
                txtNombre.Value = "";
                ListaFacturas.DataSource = null;
                ListaFacturas.DataBind();
                return;
            }
            else
            {
                txtFecha.Value = informeNuevo.fecha.ToString();
                txtSaldo.Value = informeNuevo.saldo.ToString();
                txtTotal.Value = informeNuevo.total.ToString();
                txtNombre.Value = usuarioTemp.nombre;
                facturas = capaNegociosFactura.obtenerListaFacturas();
                foreach (FacturaObj facturaTemp in facturas)
                {
                    if (facturaTemp.getFKInformeID() == informeNuevo.informeID)
                    {
                        FacturaObj facturaNueva = facturaTemp;
                        facturasXInforme.Add(facturaNueva);
                    }
                }
                Session["facturaXinforme"] = facturasXInforme;
                llenarDataGrid();
            }
        }

        private void llenarDataGrid()
        {
            facturasXInforme = (List<FacturaObj>)Session["facturaXinforme"];
            List<facturaTempClass> listaTemp = new List<facturaTempClass>();
            foreach (FacturaObj factura in facturasXInforme)
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
        protected void ddlAgente_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSeleccion();
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + " Consulta terminada." + "'); window.location='" + Request.ApplicationPath + "Administrador/Cuenta/CuentaAdministrador.aspx';", true);
        }
    }
}