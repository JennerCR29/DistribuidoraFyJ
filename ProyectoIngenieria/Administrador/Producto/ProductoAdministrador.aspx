<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="ProductoAdministrador.aspx.cs" Inherits="ProyectoIngenieria.Administrador.WebForm6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">PRODUCTO</h1>
    <a href="RegistrarProducto.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color:#0E2635"><i class="fas fa-plus-circle"></i>REGISTRAR PRODUCTO</a>
    <a href="ModificarProducto.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color:#0E2635"><i class="fas fa-wrench"></i>MODIFICAR PRODUCTO</a>
    <a href="DespacharProducto.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color:#0E2635"><i class="fas fa-shipping-fast"></i>DESPACHAR PRODUCTO</a>
    <a href="EliminarProducto.aspx" class="colorMenuSecundario1 colorMenuSecundario2 colorMenuSecundario3" style="color:#0E2635"><i class="fas fa-trash"></i>ELIMINAR PRODUCTO</a>

    <style>
        .fas {
            font-size: 35px;
            padding-right: 15px;
        }
    </style>
</asp:Content>
