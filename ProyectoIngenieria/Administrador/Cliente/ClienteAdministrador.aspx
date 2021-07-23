<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="ClienteAdministrador.aspx.cs" Inherits="ProyectoIngenieria.Administrador.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">CLIENTE</h1>
    <a href="RegistrarCliente.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="move fas fa-plus-circle"></i>REGISTRAR CLIENTE</a>
    <a href="ModificarCliente.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-wrench"></i>MODIFICAR CLIENTE</a>
    <a href="EliminarCliente.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-trash"></i>ELIMINAR CLIENTE</a>

    <style>
        .fas {
            font-size: 35px;
            padding-right: 15px;
        }
    </style>
</asp:Content>
