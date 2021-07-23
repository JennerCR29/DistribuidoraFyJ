<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="EliminarRuta.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Ruta.EliminarRuta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">ELIMINAR RUTA</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Ruta</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList ID="ddlRuta" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlRuta_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">ID de ruta</label>
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
                    <label class="col-form-label">Agente encargado</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtAgente" runat="server" disabled="disabled" />
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
