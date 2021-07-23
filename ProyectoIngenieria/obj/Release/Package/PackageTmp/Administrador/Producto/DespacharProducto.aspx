<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="DespacharProducto.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Producto.DespacharProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">DESPACHAR PRODUCTO</h1>
    <head>
        <link href="vendor/EstiloMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Producto</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList ID="ddlProducto" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="producto_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Cantidad a despachar</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtCantidad" runat="server" onkeydown="javascript: return event.keyCode === 8 ||
event.keyCode === 46 ? true : !isNaN(Number(event.key))"/>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Bodega Origen</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList ID="ddlBodegaOrigen" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="bodegaOrigen_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Bodega Destino</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList ID="ddlBodegaDestino" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="bodegaDestino_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <div class="col-md-4 ">
                        <br />
                        <br />
                        <asp:Button ID="btnAplicar" runat="server" CssClass="fondoBoton form-control" Text="APLICAR" OnClick="btnAplicar_Click" />
                    </div>
                </div>

            </form>
        </div>

    </body>
</asp:Content>
