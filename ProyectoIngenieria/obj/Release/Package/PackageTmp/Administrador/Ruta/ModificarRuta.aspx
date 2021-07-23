<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="ModificarRuta.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Ruta.ModificarRuta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">MODIFICAR RUTA</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Ruta</label>
                    <div class="fore-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlRuta" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRuta_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">ID de ruta</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtIDRuta" runat="server" disabled="disabled" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Nombre</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtNombre" runat="server" disabled="disabled"/>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Agente encargado</label>
                    <div class="form-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlAgente" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
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
