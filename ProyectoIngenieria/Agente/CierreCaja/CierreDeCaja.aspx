<%@ Page Title="" Language="C#" MasterPageFile="~/Agente/AgenteMaster.Master" AutoEventWireup="true" CodeBehind="CierreDeCaja.aspx.cs" Inherits="ProyectoIngenieria.Agente.CierreCaja.CierreDeCaja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">CIERRE DE CAJA</h1>
    <head>
        <link href="../vendor/EstilosAgenteMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="row">
                    <div class="form col-3">
                        <label class="col-form-label">Cédula</label>
                        <div class="fore-group col-md-12">
                            <input type="text" class="form-control" id="txtID" runat="server" disabled="disabled" />
                        </div>
                    </div>
                    <div class="form col-3">
                        <label class="col-form-label">Nombre</label>
                        <div class="fore-group col-md-12">
                            <input type="text" class="form-control" id="txtNombre" runat="server" disabled="disabled" />
                        </div>
                    </div>
                    <div class="form col-3">
                        <label class="col-form-label">Fecha</label>
                        <div class="fore-group col-md-12">
                            <input type="text" class="form-control" id="txtFecha" runat="server" disabled="disabled" />
                        </div>
                    </div>
                </div>
                <div>
                    <hr size="58px" color="#0E2635" />
                    <h5 style="align-items: flex-start">DETALLE DE FACTURAS</h5>
                </div>
                <div class="col-md-12 col-sm-12 col-lg-12">
                    <asp:GridView CssClass="table table-striped" AutoGenerateColumns="False" BorderWidth="0px" ID="ListaFacturas" runat="server">
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
                    <br />
                    <br />
                    <hr size="58px" color="#0E2635" />
                    <h5 style="align-items: flex-start">DETALLE DE PAGO PARA CIERRE</h5>
                </div>
                <div class="row">
                    <div class="form col-3">
                        <label class="col-form-label">Total a pagar</label>
                        <div class="fore-group col-md-12">
                            <input type="text" class="form-control" id="txtTotal" runat="server" disabled="disabled"/>
                        </div>
                    </div>
                    <div class="form col-3">
                        <label class="col-form-label">Saldo crediticio</label>
                        <div class="fore-group col-md-12">
                            <input type="text" class="form-control" id="txtSaldo" runat="server" disabled="disabled"/>
                        </div>
                    </div>
                </div>
                <div class="form">
                    <asp:Button runat="server" ID="btnConfirmar" Text="GENERAR" CssClass="fondoBotonDerecha form-control" OnClientClick="return confirm('¿Seguro que desea confirmar su cierre de caja semanal?');" OnClick="btnConfirmar_Click" />
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

            </form>
        </div>

    </body>
</asp:Content>
