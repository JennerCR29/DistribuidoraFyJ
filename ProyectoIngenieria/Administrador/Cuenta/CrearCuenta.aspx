<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="CrearCuenta.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Cuenta.CrearCuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">CREAR CUENTA</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Cliente</label>
                    <div class="form-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="form">
                    <label class="col-form-label">Tipo de cuenta</label>
                    <div class="form-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlCuenta" CssClass="form-control" AutoPostBack="true">
                            <asp:ListItem>
                                Contado
                            </asp:ListItem>
                            <asp:ListItem>
                                Crédito
                            </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form">
                    <div class="col-md-4 ">
                        <br />
                        <asp:Button ID="btnCrear" runat="server" CssClass="fondoBoton form-control" Text="CREAR" OnClick="btnCrear_Click" />
                    </div>
                </div>
            </form>
        </div>

    </body>
</asp:Content>
