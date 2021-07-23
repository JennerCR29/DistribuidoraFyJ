<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="EliminarUsuario.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Usuario.EliminarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">ELIMINAR USUARIO</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Usuario</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList id="usuario" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="usuario_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Número de Cédula</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtID" runat="server"  disabled="disabled" />
                        <%--se tiene que llenar y NO se puede cambiar--%>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Nombre de Usuario</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtNombreUsuario" runat="server"  disabled="disabled"/>
                        <%--se tiene que llenar y se puede cambiar--%>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Rol</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtRol" runat="server"  disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Bodega</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtBodega" runat="server"  disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <div class="col-md-4 ">
                        <br />
                        <asp:Button ID="btnEliminar" runat="server" CssClass="fondoBotonEliminar form-control" OnClientClick="return confirm('¿Seguro que desea eliminar este usuario?');" Text="ELIMINAR" OnClick="btnEliminar_Clicked" />
                    </div>
                </div>
            </form>
        </div>

    </body>
</asp:Content>
