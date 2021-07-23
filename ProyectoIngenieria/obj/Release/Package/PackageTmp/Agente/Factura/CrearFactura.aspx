<%@ Page Title="" Language="C#" MasterPageFile="~/Agente/AgenteMaster.Master" AutoEventWireup="true" CodeBehind="CrearFactura.aspx.cs" Inherits="ProyectoIngenieria.Agente.Factura.CrearFactura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">CREAR FACTURA</h1>
    <head>
        <link href="../vendor/EstilosAgenteMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="row">
                    <div class="form col-3">
                        <label class="col-form-label">Cliente</label>
                        <div class="fore-group col-md-12">
                            <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form col-3">
                        <label class="col-form-label">Tipo de cuenta</label>
                        <div class="fore-group col-md-12">
                            <asp:DropDownList runat="server" ID="ddlCuenta" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCuenta_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form col-3">
                        <label class="col-form-label">Saldo de la cuenta</label>
                        <div class="fore-group col-md-12">
                            <input type="text" class="form-control" id="txtSaldoCuenta" runat="server" disabled="disabled" />
                        </div>
                    </div>
                </div>
                <div>
                    <hr size="58px" color="#0E2635" />
                    <h5 style="align-items: flex-start">NUEVA LÍNEA PEDIDO</h5>
                </div>
                <div class="row">
                    <div class="form col-3">
                        <label class="col-form-label">Producto</label>
                        <div class="fore-group col-md-12">
                            <asp:DropDownList runat="server" ID="ddlProducto" CssClass="form-control" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form col-3">
                        <label class="col-form-label">Cantidad</label>
                        <div class="fore-group col-md-12">
                            <input type="number" onkeydown="javascript: return event.keyCode === 8 ||
                        event.keyCode === 46 ? true : !isNaN(Number(event.key))"
                                class="form-control" id="txtCantidad" runat="server" disabled="disabled" />
                        </div>
                    </div>
                    <div class="form col-3">
                        <label class="col-form-label">Precio unitario</label>
                        <div class="fore-group col-md-12">
                            <input type="text" class="form-control" id="txtPrecioUnitario" runat="server" disabled="disabled" />
                        </div>
                    </div>
                    <div class="form col-3">
                        <label class="col-form-label">Descuento del producto (%)</label>
                        <div class="fore-group col-md-12">
                            <input type="number" onkeydown="javascript: return event.keyCode === 8 ||
                        event.keyCode === 46 ? true : !isNaN(Number(event.key))"
                                class="form-control" id="txtDescuentoUnitario" runat="server" disabled="disabled" />
                        </div>
                    </div>
                </div>
                <div class="form">
                    <asp:Button runat="server" Text="AGREGAR" ID="btnAgregarLinea" CssClass="fondoBotonDerecha form-control" Enabled="false" OnClick="btnAgregarLinea_Click" />
                    <style>
                        .fondoBotonDerecha {
                            background-color: #0E2635 !important;
                            color: white;
                            text-align: center;
                            width: 120px;
                            height: 40px;
                            width: 100px;
                            margin-top: 10px;
                            top: 50%;
                            left: 50%;
                            float: right;
                            margin-right: 0px;
                            overflow: auto;
                        }
                    </style>
                </div>
                <div>
                    <br />
                    <br />
                    <hr size="58px" color="#0E2635" />
                    <h5 style="align-items: flex-start">DETALLE DE FACTURA</h5>
                </div>
                <div class="col-md-12 col-sm-12 col-lg-12">
                    <asp:GridView CssClass="table table-striped" AutoGenerateColumns="False" BorderWidth="0px" ID="ListaOrdenes" runat="server" OnRowDeleting="ListaOrdenes_RowDeleting" OnSelectedIndexChanging="ListaOrdenes_SelectedIndexChanging">
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
                            <asp:CommandField ShowDeleteButton="true" SelectText="Eliminar" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="fore-control">
                    <br />
                    <br />
                    <hr size="58px" color="#0E2635" />
                    <h5 style="align-items: flex-start">DETALLE DE PAGO</h5>
                </div>
                <div class="row">
                    <div class="form col-3">
                        <label class="col-form-label">Subtotal</label>
                        <div class="fore-group col-md-12">
                            <input type="text" class="form-control" id="txtSubtotalFactura" runat="server" />
                        </div>
                    </div>
                    <div class="form col-3">
                        <label class="col-form-label">Descuento factura (%)</label>
                        <div class="fore-group col-md-12">
                            <input type="number" onkeydown="javascript: return event.keyCode === 8 ||
                        event.keyCode === 46 ? true : !isNaN(Number(event.key))"
                                class="form-control" id="txtDescuentoFactura" runat="server" />
                        </div>
                    </div>
                    <div class="form col-3">
                        <label class="col-form-label">Total factura</label>
                        <div class="fore-group col-md-12">
                            <input type="text" class="form-control" id="txtTotalFactura" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form">
                    <asp:Button runat="server" ID="btnConfirmar" Text="GENERAR" CssClass="fondoBotonDerecha form-control" OnClientClick="return confirm('¿Seguro que desea continuar?');" Enabled="false" OnClick="btnConfirmar_Click" />
                </div>
            </form>
        </div>

    </body>
</asp:Content>
