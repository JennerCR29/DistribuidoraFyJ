<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="ProyectoIngenieria.InicioSesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Inicio de sesión</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.8/css/solid.css" />
    <script src="https://use.fontawesome.com/releases/v5.0.7/js/all.js"></script>
    <link rel="stylesheet" type="text/css" href="Administrador/vendor/EstilosMaster.css" />
</head>
<body>
    <%--<form id="form1" runat="server">--%>
    <div class="modal-dialog text-center">
        <div class="col-sm-8 main-section">
            <div class="modal-content">
                <div class="col-12 user-img">
                    <img src="Administrador/Imagenes/Logo.png" />
                </div>
                <form class="col-12" runat="server">
                    <div class="form-group" id="user-group">
                        <input id="inptID" type="text" class="form-control" placeholder="Identificador" runat="server" />
                    </div>
                    <div class="form-group" id="password-group">
                        <input id="inptPSW" type="password" class="form-control" placeholder="Contraseña" runat="server" />
                    </div>
                    <div class="form" id="button-group">
                        <asp:Button ID="btnIngresar" runat="server" CssClass="btn btn-login" Text="INGRESAR" OnClick="btnIngresar_Clicked"/>
                    </div>
                    <br />
                </form>
            </div>
        </div>
    </div>
    <%--</form>--%>
</body>
</html>
