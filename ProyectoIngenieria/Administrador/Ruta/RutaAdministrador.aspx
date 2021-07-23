<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="RutaAdministrador.aspx.cs" Inherits="ProyectoIngenieria.Administrador.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">RUTA</h1>
    <a href="RegistrarRuta.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-plus-circle"></i>REGISTRAR RUTA</a>
    <a href="ModificarRuta.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-wrench"></i>MODIFICAR RUTA</a>
    <a href="EliminarRuta.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-trash"></i>ELIMINAR RUTA</a>

    <style>
        .fas {
            font-size: 35px;
            padding-right: 15px;
        }
    </style>
</asp:Content>
