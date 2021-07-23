<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="RegistrarBodega.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Bodega.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="textoSecundario"> REGISTRAR BODEGA</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Nombre de la Bodega</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtNombre" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Ubicación de la bodega</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtUbicacion" runat="server" />
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
