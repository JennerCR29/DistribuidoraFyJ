<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="ConsultarCuenta.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Cuenta.ConsultarCuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">CONSULTAR CUENTA</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Cliente</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Cuenta</label>
                    <div class="form-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlCuentas" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCuentas_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Fecha de Creacion</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtFechaCreacion" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="row">
                    <div class="form">
                        <label class="col-form-label">Fecha de inicio</label>
                        <div class="fore-group col-md-4">
                            <asp:Calendar ID="calFechaInicio" runat="server"></asp:Calendar>
                        </div>
                    </div>
                    <div class="form">
                        <label class="col-form-label">Fecha de fin</label>
                        <div class="fore-group col-md-4">
                            <asp:Calendar ID="calFechaFin" runat="server"></asp:Calendar>
                        </div>
                    </div>
                    <div>
                        <asp:Button runat="server" ID="btnAplicarFiltro" CssClass="btnAplicarFiltro form-control" Text="APLICAR FILTRO" OnClick="btnAplicarFiltro_Click" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Total de Facturas</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtTotalFacturas" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Saldo</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtSaldo" runat="server" disabled="disabled" />
                    </div>
                </div>
                <div>
                    <hr size="58px" color="#0E2635" />
                    <h5 style="align-items: flex-start">Facturas asociadas a la cuenta</h5>
                </div>

                <div class="col-md-12 col-sm-12 col-lg-12">
                    <asp:GridView CssClass="table table-striped" AutoGenerateColumns="False" BorderWidth="0px" ID="ListaOrdenes" runat="server">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#0E2635" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                        <Columns>
                            <asp:BoundField HeaderText="Consecutivo" DataField="facturaID" />
                            <asp:BoundField HeaderText="Fecha" DataField="fecha" />
                            <asp:BoundField HeaderText="Descuento Aplicado" DataField="descuento" />
                            <asp:BoundField HeaderText="Total" DataField="total" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div>
                    <hr size="58px" color="#0E2635" />
                </div>
                <div class="form">
                    <div class="col-md-4 ">
                        <br />
                        <asp:Button ID="btnFinalizar" runat="server" CssClass="fondoBoton form-control" Text="FINALIZAR" OnClick="btnFinalizar_Click" />
                    </div>
                </div>
                <style>
                    .btnAplicarFiltro{
                        margin-top: 36px;
                        width: 140px;
                        background-color: #0E2635;
                        height:40px;
                        color: white;
                    }
                </style>
            </form>
        </div>
    </body>
</asp:Content>
