<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="ConsultarFactura.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Factura.ConsultarFactura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">CONSULTAR FACTURA</h1>
    <head>
        <link href="../vendor/EstilosMaster.css" type="text/css" rel="stylesheet" />
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
                    <label class="col-form-label">Cuenta del cliente</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlCuentaCliente" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCuentaCliente_SelectedIndexChanged"></asp:DropDownList>
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
                    <label class="col-form-label">Factura</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlFactura" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFactura_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-md-12 col-sm-12 col-lg-12" style="margin-top: 15px;">
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
                            <asp:BoundField HeaderText="Producto" DataField="nombreProducto" />
                            <asp:BoundField HeaderText="Cantidad" DataField="cantidad" />
                            <asp:BoundField HeaderText="Precio Unitario" DataField="precioUnitario" />
                            <asp:BoundField HeaderText="Descuento" DataField="descuento" />
                            <asp:BoundField HeaderText="SubTotal" DataField="subtotal" />
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="row">

                    <div class="form col-3">
                        <label class="col-form-label">Fecha de Factura</label>
                        <div class="fore-group col-md-12">
                            <input type="text" class="form-control" id="txtFechaFactura" runat="server" disabled="disabled" />
                        </div>
                    </div>

                    <div class="form col-3">
                        <label class="col-form-label">Descuento factura (%)</label>
                        <div class="fore-group col-md-12">
                            <input type="number" class="form-control" id="txtDescuentoFactura" runat="server" disabled="disabled" />
                        </div>
                    </div>

                    <div class="form col-3">
                        <label class="col-form-label">Total factura</label>
                        <div class="fore-group col-md-12">
                            <input type="text" class="form-control" id="txtTotalFactura" runat="server" disabled="disabled" />
                        </div>
                    </div>

                </div>

                <div>
                    <div>
                        <asp:Button runat="server" ID="btnFinalizar" CssClass="boton form-control" Text="FINALIZAR" OnClick="btnFinalizar_Click" />
                    </div>
                </div>
                <style>
                    .boton {
                        margin-left: 15px;
                        margin-top: 15px;
                        background-color: #0E2635;
                        color: white;
                        width: 120px;
                        height: 40px;
                    }

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
