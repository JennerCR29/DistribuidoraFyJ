<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="ModificarUsuario.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Usuario.ModificarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">MODIFICAR USUARIO</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Usuario</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlUsuario" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUsuario_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Número de Cédula</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtIDUsuario" runat="server" disabled="disabled" />
                        <%--se tiene que llenar y NO se puede cambiar--%>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">ID de usuario</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtID" runat="server" disabled="disabled" />
                        <%--se tiene que llenar y NO se puede cambiar--%>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Nombre de Usuario</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="TtxtNombreUsuario" runat="server" disabled="disabled" />
                        <%--se tiene que llenar y se puede cambiar--%>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Contraseña</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtContrasena" runat="server" disabled="disabled" />
                        <%--se tiene que llenar y se puede cambiar--%>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Bodega</label>
                    <div class="form-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlBodega" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
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
