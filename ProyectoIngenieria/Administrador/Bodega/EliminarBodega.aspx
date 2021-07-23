<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="EliminarBodega.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Bodega.EliminarBodega" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="textoSecundario">ELIMINAR BODEGA</h1>
    <head>
        <link href="vendor/EstiloMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Bodega</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList ID="bodega" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="bodega_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">ID de Bodega</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtID" runat="server" disabled="disabled" />
                        <%--se tiene que llenar y NO se puede cambiar--%>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Nombre</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtNombre" runat="server" disabled="disabled" />
                        <%--se tiene que llenar y NO se puede cambiar--%>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Ubicación</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtUbicacion" runat="server" disabled="disabled" />
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
