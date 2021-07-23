<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="CuentaAdministrador.aspx.cs" Inherits="ProyectoIngenieria.Administrador.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">CUENTA</h1>
    <a href="AbonarCuenta.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-donate"></i>ABONAR A CUENTA</a>
    <a href="CrearCuenta.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" id="btnCrearCuenta" style="color: #0E2635"><i class="fas fa-plus-circle"></i>CREAR CUENTA</a>
    <a href="ConsultarCuenta.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-search"></i>CONSULTAR CUENTA</a>

    <style>
        .fas {
            font-size: 35px;
            padding-right: 15px;
        }
    </style>
</asp:Content>
