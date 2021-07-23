﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="RegistrarCliente.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Cliente.RegistrarCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">REGISTRAR CLIENTE</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Nombre de Cliente</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtNombre" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Contacto</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtContacto" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Ruta</label>
                    <div class="form-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlRuta" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <div class="col-md-4 ">
                        <br />
                        <asp:Button ID="btnRegistrar" runat="server" CssClass="fondoBoton form-control" Text="REGISTRAR" OnClick="btnRegistrar_Click" />
                    </div>
                </div>
            </form>
        </div>

    </body>
</asp:Content>
