<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="FacturaAdministrador.aspx.cs" Inherits="ProyectoIngenieria.Administrador.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">FACTURA</h1>
    <a href="CrearFactura.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-receipt"></i>CREAR FACTURA</a>
    <a href="ConsultarFactura.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-search"></i>CONSULTAR FACTURA</a>
    <a href="EliminarFactura.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color: #0E2635"><i class="fas fa-trash"></i>ELIMINAR FACTURA</a>

    <style>
        .fas {
            font-size: 35px;
            padding-right: 15px;
        }
    </style>
</asp:Content>
