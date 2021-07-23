<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="UsuarioAdministrador.aspx.cs" Inherits="ProyectoIngenieria.Administrador.WebForm8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">USUARIO</h1>
    <a href="RegistrarUsuario.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-plus-circle"></i>REGISTRAR USUARIO</a>
    <a href="ModificarUsuario.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-wrench"></i>MODIFICAR USUARIO</a>
    <a href="EliminarUsuario.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-trash"></i>ELIMINAR USUARIO</a>

    <style>
        .fas {
            font-size: 35px;
            padding-right: 15px;
        }
    </style>
</asp:Content>
