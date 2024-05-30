<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inicio.aspx.cs" Inherits="inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
    <!-- jQuery -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha384-KyZXEAg3QhqLMpG8r+Knujsl5+zEK5ymZ46jP/uJ9SAwiZ+nd8vV4P5CV9cnXqKC" crossorigin="anonymous"></script>

<!-- Bootstrap CSS -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">

<!-- Bootstrap JS Bundle -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>

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
                            <a class="nav-link active" aria-current="page" href="Categorias.aspx">Categorias</a>
                            <a class="nav-link active" aria-current="page" href="Proveedores.aspx">Proveedores</a>
                            <a class="nav-link active" aria-current="page" href="Usuarios.aspx">Usuarios</a>
                            <a class="nav-link active" aria-current="page" href="Unidades.aspx">Unidades</a>
                            <a class="nav-link active" aria-current="page" href="Resportes.aspx">Reportes</a>
                        </div>
                    </div>
                </div>
            </nav>

             <div class="titulo">
                <h2>Productos</h2>
                <!-- Botón para abrir el modal de agregar producto -->
                <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalAgregarProducto">Agregar Producto</button>
            </div>

            <!-- Contenido principal -->
            <main class="container" style="margin-top: 120px;">
                <!-- ListView para mostrar los productos -->
                <asp:ListView ID="ListView1" runat="server" OnPagePropertiesChanging="listIngredientes_PagePropertiesChanging">
                    <LayoutTemplate>
                        <table class="table table-striped table-hover table-bordered" style="margin-top: 30px;">
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Nombre</th>
                                    <th>Fecha de entrada</th>
                                    <th>Caducidad</th>
                                    <th>Cantidad</th>
                                    <th>Id categoria</th>
                                    <th>Id proveedores</th>
                                    <th> Id Unidades</th>
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
        <td><%# Eval("ID_Producto") %></td>
        <td><%# Eval("Nombre") %></td>
        <td><%# Eval("Fecha_Entrada") %></td>
        <td><%# Eval("Caducidad") %></td>
        <td><%# Eval("Cantidad_Peso_Stock") %></td>
        <td><%# Eval("ID_Categoria") %></td>
        <td><%# Eval("Id_Proveedor") %></td>
         <td><%# Eval("Unidades") %></td>
        <td>
            <!-- Botón para editar -->
          <a href='<%# "EditarProductos.aspx?id=" + Eval("ID_Producto") %>' class="btn btn-warning">Editar</a>


            <!-- Botón para eliminar con modal de confirmación -->
            <button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#modalEliminarProducto_<%# Eval("ID_Producto") %>">Eliminar</button>

            <!-- Modal de confirmación para eliminar producto -->
            <div class="modal fade" id="modalEliminarProducto_<%# Eval("ID_Producto") %>" tabindex="-1" aria-labelledby="modalEliminarProductoLabel" aria-hidden="true">
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
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="EliminarProducto" CommandArgument='<%# Eval("ID_Producto") %>' OnClick="btnEliminar_Click" />
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
            <!-- Modal para agregar productos -->
            <div class="modal fade" id="modalAgregarProducto" tabindex="-1" aria-labelledby="modalAgregarProductoLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="modalAgregarProductoLabel">Agregar Nuevo Producto</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Campos del formulario (Nombre, Fecha de entrada, Caducidad, etc.) -->
                            <!-- Puedes personalizar según los campos que tengas en tu base de datos -->
                         <div class="mb-3">
                            <label for="" class="form-label">Nombre del producto</label>
                           <input type="text" id="txtNombre" runat="server" class="form-control" />
                        </div>

                            <div class="mb-3">
                               <label for="Text1" class="form-label">Fecha de entrada del producto</label>
                               <input type="date" id="Text1" runat="server" class="form-control" />
                            </div>
                         

                          <div class="mb-3">
                            <label for="" class="form-label">Caducidad del Producto</label>
                              <input type="date" id="Text2" runat="server" class="form-control" />
                          </div>

                           <div class="mb-3">
                            <label for="" class="form-label">Cantidad</label>
                              <input type="text" id="Text3" runat="server" class="form-control" />
                          </div>

                            <div class="mb-3">
                            <label for="" class="form-label">Tipo</label>
                               <asp:DropDownList ID="Text6" runat="server" CssClass="form-control"></asp:DropDownList>
                          </div>
                         
                            <div class="mb-3">
                              <label for="" class="form-label">Categoria del Producto</label>
                               <!-- ComboBox para la categoría -->
                            <asp:DropDownList ID="Text4" runat="server" CssClass="form-control"></asp:DropDownList>
                          </div>
            
                            <div class="mb-3">
                              <label for="" class="form-label">Proveedor del Producto</label>
                               <!-- ComboBox para el proveedor -->
                            <asp:DropDownList ID="Text5" runat="server" CssClass="form-control"></asp:DropDownList>
                          </div>
            
                            
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="Button1" runat="server" Text="Guardar Producto" CssClass="btn btn-primary" OnClick="btnGuardarProducto_Click" data-bs-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
      </form>
    
    <script>
        // Obtener la fecha actual
        var fechaActual = new Date().toISOString().split('T')[0];

        // Establecer el valor predeterminado del campo de fecha
        document.getElementById('Text1').value = fechaActual;

        // Deshabilitar el campo de fecha para que no se pueda modificar
        document.getElementById('Text1').setAttribute('readonly', 'readonly');
    </script>
</body>
</html>
