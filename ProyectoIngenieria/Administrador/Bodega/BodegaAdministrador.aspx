<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="BodegaAdministrador.aspx.cs" Inherits="ProyectoIngenieria.Administrador.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="textoSecundario">BODEGA</h1>

    <a href="RegistrarBodega.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-plus-circle"></i>REGISTRAR BODEGA</a>
    <a href="ModificarBodega.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-wrench"></i>MODIFICAR BODEGA</a>
    <a href="EliminarBodega.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-trash"></i>ELIMINAR BODEGA</a>

    <style>
        .fas {
            font-size: 35px;
            padding-right: 15px;
        }
    </style>
</asp:Content>
