<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="EliminarCliente.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Cliente.EliminarCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">ELIMINAR CLIENTE</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Cliente</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList id="ddlCliente" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">ID de Cliente</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtID" runat="server"  disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Contacto</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtContacto" runat="server"  disabled="disabled"/>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Ruta</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtRuta" runat="server"  disabled="disabled" />
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
