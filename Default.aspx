<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
     <link rel="stylesheet" type="text/css" href="Estilos/StyleSheet.css" />

</head>
<body>
    <div class="login-container">
    <form method="post" runat="server"> 
        <img src="Images/alma.jpg" alt="Imagen de círculo" style="width: 100px; height: 100px; border-radius: 50%; display: block; margin: 0 auto;" />
        <h2>Iniciar Sesión</h2>
        <asp:TextBox id="txtUsuario" placeholder="ID" runat="server" />
        <asp:TextBox TextMode="password" id="txtContraseña" placeholder="Contraseña" runat="server"></asp:TextBox>
        <asp:Button ID="loginbtn" runat="server" Text="Iniciar Sesión" OnClick="btnLogin_Click" />
    </form>
</div>
</body>
</html>
