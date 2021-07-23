<%@ Page Title="" Language="C#" MasterPageFile="~/Agente/AgenteMaster.Master" AutoEventWireup="true" CodeBehind="AbonarCuenta.aspx.cs" Inherits="ProyectoIngenieria.Agente.Cuenta.AbonarCuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">ABONAR CUENTA</h1>
    <head>
        <link href="../vendor/EstilosAgenteMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>

                <div class="form">
                    <label class="col-form-label">Cliente</label>
                    <div class="form-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Cuenta</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtCuenta" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Facturas pendientes</label>
                    <div class="form-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlFacturasPendientes" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFacturasPendientes_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Total</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtTotal" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Saldo restante</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtSaldoRestante" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Monto a abonar</label>
                    <div class="fore-group col-md-4">
                        <input type="number" onkeydown="javascript: return event.keyCode === 8 ||
                        event.keyCode === 46 ? true : !isNaN(Number(event.key))"
                            class="form-control" id="txtMonto" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <div class="col-md-4 ">
                        <br />
                        <asp:Button ID="btnConfirmar" runat="server" CssClass="fondoBoton form-control" Text="CONFIRMAR" OnClick="btnConfirmar_Click" />
                    </div>
                </div>

            </form>
        </div>
    </body>
</asp:Content>
