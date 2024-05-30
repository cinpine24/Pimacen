<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditarUsuarios.aspx.cs" Inherits="EditarUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--  Bootstrap -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="editarUsuariosForm" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-6 offset-md-3">
                    <h2 class="text-center">Editar Producto</h2>
                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>

                        <label for="Text1" class="form-label">Apellidos</label>
                        <asp:TextBox ID="txtApe" runat="server" CssClass="form-control"></asp:TextBox>

                        <label for="Text2" class="form-label">Rol</label>
                        <asp:TextBox ID="txtRol" runat="server" CssClass="form-control"></asp:TextBox>

                        <label for="Text3" class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtcontra" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                  
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </form>

    <!--  Bootstrap  -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
