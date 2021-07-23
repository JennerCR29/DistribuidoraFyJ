<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="RegistrarUsuario.aspx.cs" Inherits="ProyectoIngenieria.Administrador.Usuario.RegistrarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="textoSecundario">REGISTRAR USUARIO</h1>
    <head>
        <link href="vendor/EstilosMaster.css" rel="stylesheet" />
    </head>
    <body>
        <div class="container">
            <form>
                <div class="form">
                    <label class="col-form-label">Nombre</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtNombreUsuario" runat="server"/>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Número de Cédula</label>
                    <div class="fore-group col-md-4">
                        <input type="number" onkeydown="javascript: return event.keyCode === 8 ||
                        event.keyCode === 46 ? true : !isNaN(Number(event.key))"
                            class="form-control" id="txtID" runat="server"/>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Contraseña</label>
                    <div class="fore-group col-md-4">
                        <input type="text" class="form-control" id="txtContra" runat="server" />
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Rol</label>
                    <div class="form-group col-md-4">
                        <select name="rol" id="cbRol" class="form-control" runat="server">
                            <option value="administrador">Administrador</option>
                            <option value="agenteDeVentas">Agente de Ventas</option>
                        </select>
                    </div>
                </div>

                <div class="form">
                    <label class="col-form-label">Bodega</label>
                    <div class="form-group col-md-4">
                        <asp:DropDownList runat="server" ID="ddlBodega" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
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
