<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="RegistrarProducto.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Producto.RegistrarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="textoSecundario">REGISTRAR PRODUCTO</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Nombre de producto</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtNombre" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Descripcion</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtDescripcion" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Tipo</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtTipo" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Cantidad a agregar</label>
                    <div class="fore-group col-md-4">
                        <input type="number" onkeydown="javascript: return event.keyCode === 8 ||
                        event.keyCode === 46 ? true : !isNaN(Number(event.key))"
                            class="form-control" id="txtCantidad" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Precio Costo</label>
                    <div class="fore-group col-md-4">
                        <input type="number" onkeydown="javascript: return event.keyCode === 8 ||
                        event.keyCode === 46 ? true : !isNaN(Number(event.key))"
                            class="form-control" id="txtPrecioCosto" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Precio Costo Agente</label>
                    <div class="fore-group col-md-4">
                        <input type="number" onkeydown="javascript: return event.keyCode === 8 ||
                        event.keyCode === 46 ? true : !isNaN(Number(event.key))"
                            class="form-control" id="txtPrecioCostoAgente" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Precio Venta</label>
                    <div class="fore-group col-md-4">
                        <input type="number" onkeydown="javascript: return event.keyCode === 8 ||
                        event.keyCode === 46 ? true : !isNaN(Number(event.key))"
                            class="form-control" id="txtPrecioVenta" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Precio Base Venta</label>
                    <div class="fore-group col-md-4">
                        <input type="number" onkeydown="javascript: return event.keyCode === 8 ||
                        event.keyCode === 46 ? true : !isNaN(Number(event.key))"
                            class="form-control" id="txtPrecioBaseVenta" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <div class="col-md-4 ">
                        <br />
                        <br />
                        <asp:Button ID="btnRegistrar" runat="server" CssClass="fondoBoton form-control" Text="REGISTRAR" OnClick="btnRegistrar_Click" />
                    </div>
                </div>
            </form>
        </div>
    </body>
</asp:Content>
