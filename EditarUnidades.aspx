<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditarUnidades.aspx.cs" Inherits="EditarUnidades" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <!-- Bootstrap -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <div class="row">
                <div class="col-md-6 offset-md-3">
                    <h2 class="text-center">Editar Unidades</h2>
                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre de la Unidad </label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
