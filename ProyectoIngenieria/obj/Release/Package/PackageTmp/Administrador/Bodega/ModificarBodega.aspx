<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="ModificarBodega.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Bodega.ModificarBodega" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">MODIFICAR BODEGA</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Bodega</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlBodega" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBodega_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">ID de Bodega</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtIDBodega" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Nombre </label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtNombre" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Ubicación</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtUbicacion" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <div class="col-md-4">
                        <br />
                        <br />
                        <asp:Button ID="btnModificar" runat="server" CssClass="fondoBoton form-control" Text="MODIFICAR" OnClick="btnModificar_Click" />
                    </div>
                </div>

            </form>
        </div>
    </body>

</asp:Content>
