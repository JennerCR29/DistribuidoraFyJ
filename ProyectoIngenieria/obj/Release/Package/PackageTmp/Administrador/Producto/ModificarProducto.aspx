<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="ModificarProducto.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Producto.ModificarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">MODIFICAR PRODUCTO</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Producto</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlProducto" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">ID</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtIDProducto" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Nombre</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtNombre" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Descripcion</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtDescripcion" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Tipo</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtTipo" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Cantidad</label>
                    <div class="fore-group col-md-4">
                        <input type="number" onkeydown="javascript: return event.keyCode === 8 ||
                        event.keyCode === 46 ? true : !isNaN(Number(event.key))"
                            class="form-control" id="txtCantidad" runat="server" disabled="disabled" />
                    </div>

                </div>

                <div class="form">
                    <label class="col-form-label">Precio Costo</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtPrecioCosto" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Precio Costo Agente</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtPrecioCostoAgente" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Precio Venta</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtPrecioVenta" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Precio Base Venta</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtPrecioBaseVenta" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <div class="col-md-4 ">
                        <br />
                        <asp:Button ID="btnModificar" runat="server" CssClass="fondoBoton form-control" Text="MODIFICAR" OnClick="btnModificar_Click" />
                    </div>
                </div>
            </form>
        </div>

    </body>
</asp:Content>
