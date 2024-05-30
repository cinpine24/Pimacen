<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Proveedores.aspx.cs" Inherits="Proveedores" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
    <style>
        /* Estilo para centrar el título */
        .titulo {
            text-align: center;
        }

        /* Estilo para la tabla */
        .tabla {
            margin: 0 auto; /* Centra la tabla horizontalmente */
            width: 80%; /* Ancho de la tabla */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <nav class="navbar navbar-expand-lg navbar-light bg-light">
                <div class="container-fluid">
                    <img src="Images/alma.jpg" alt="Imagen de círculo" style="width: 50px; height: 50px; border-radius: 50%; display: block; margin: 0 auto;" />
                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                        <div class="navbar-nav">
                            <a class="nav-link active" aria-current="page" href="inicio.aspx">Productos</a>
                            <a class="nav-link active" aria-current="page" href="Categorias.aspx">Categorias</a>
                            <a class="nav-link active" aria-current="page" href="Usuarios.aspx">Usuarios</a>
                             <a class="nav-link active" aria-current="page" href="Unidades.aspx">Unidades</a>
                            <a class="nav-link active" aria-current="page" href="Resportes.aspx">Reportes</a>
                        </div>
                    </div>
                </div>
            </nav>

            <!-- Título centrado antes de la tabla -->
            <div class="titulo">
                <h2>Proveedores </h2>
                  
                <!-- Botón para abrir el modal de agregar proveedor -->
                <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalAgregarProveedores">Agregar Proveedor</button>
            </div>
            <main class="container" style="margin-top: 120px;">
                <asp:ListView ID="ListView2" runat="server" OnPagePropertiesChanging="listIngredientes_PagePropertiesChanging">
                    <LayoutTemplate>
                        <table class="table table-striped table-hover table-bordered" style="margin-top: 30px;">
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Nombre Empresa</th>
                                <th>Movimientos</th>
                               </tr>
                    </thead>
                    <tbody>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                    </tbody>
                </table>
            </LayoutTemplate>
         <ItemTemplate>
    <tr>
        <td><%# Eval("ID") %></td>
        <td><%# Eval("NombreEmpresa") %></td>
        <td>
            <!-- Botón para editar -->
            <a href='<%# "EditarProveedores.aspx?id=" + Eval("ID") %>' class="btn btn-warning">Editar</a>

            <!-- Botón para eliminar con modal de confirmación -->
            <button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#modalEliminarProducto_<%# Eval("ID") %>">Eliminar</button>

            <!-- Modal de confirmación para eliminar producto -->
            <div class="modal fade" id="modalEliminarProducto_<%# Eval("ID") %>" tabindex="-1" aria-labelledby="modalEliminarProductoLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="modalEliminarProductoLabel">Eliminar Producto</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            ¿Estás seguro de que quieres eliminar este producto?
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="EliminarProducto" CommandArgument='<%# Eval("ID") %>' OnClick="btnEliminar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </td>
    </tr>
</ItemTemplate>

                    <EmptyDataTemplate>
                        <tr>
                            <td colspan="5">No hay datos disponibles</td>
                        </tr>
                    </EmptyDataTemplate>
                </asp:ListView>
            </main>           

        </div>

        <!-- Modal para agregar proveedores -->
        <div class="modal fade" id="modalAgregarProveedores" tabindex="-1" aria-labelledby="modalAgregarProveedoresLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalAgregarProveedoresLabel">Agregar un nuevo Proveedor</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <!-- Campo del formulario para el nombre del proveedor -->
                        <input type="text" id="txtNombreProveedor" runat="server" placeholder="Nombre de la Empresa" class="form-control" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnGuardarProveedor" runat="server" Text="Guardar Proveedor" CssClass="btn btn-primary" OnClick="btnGuardarProveedor_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
