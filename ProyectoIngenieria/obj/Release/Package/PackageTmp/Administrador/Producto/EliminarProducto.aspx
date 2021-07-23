<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="EliminarProducto.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Producto.EliminarProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">ELIMINAR PRODUCTO</h1>
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
                    <label class="col-form-label">ID de Producto</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtID" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Nombre</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtNombre" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Descripción</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtDescripción" runat="server" disabled="disabled" />
                        <%--se tiene que llenar y NO se puede cambiar--%>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Tipo</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtTipo" runat="server" disabled="disabled" />
                        <%--se tiene que llenar y NO se puede cambiar--%>
                    </div>
                </div>

                <div class="form">
                    <div class="col-md-4 ">
                        <br />
                        <br />
                        <asp:Button ID="btnEliminar" runat="server" CssClass="fondoBotonEliminar form-control" OnClientClick="return confirm('¿Seguro que desea eliminar esta bodega?');" Text="ELIMINAR" OnClick="btnEliminar_Clicked" />
                    </div>
                </div>

            </form>
        </div>

    </body>
</asp:Content>
