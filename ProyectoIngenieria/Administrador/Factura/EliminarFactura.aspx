<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="EliminarFactura.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Factura.EliminarFactura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">ELIMINAR FACTURA</h1>
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

                <div class="form">
                    <label class="col-form-label">Factura</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlFactura" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFactura_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Contraseña de Administrador</label>
                    <div class="fore-group col-md-4">
                        <input type="password" class="form-control" id="txtContra" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <div class="col-md-4">
                        <br />
                        <asp:Button ID="btnEliminar" runat="server" CssClass="fondoBotonEliminar form-control" Text="ELIMINAR" OnClientClick="return confirm('¿Seguro que desea eliminar esta factura?');" OnClick="btnEliminar_Click" />
                    </div>
                </div>

            </form>
        </div>
    </body>
</asp:Content>
