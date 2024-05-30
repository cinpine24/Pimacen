using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class EditarProductos : System.Web.UI.Page
{
    string cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int idProducto = Convert.ToInt32(Request.QueryString["id"]);
            CargarDatosProducto(idProducto);
        }
    }

    private void CargarDatosProducto(int idProducto)
    {
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "SELECT * FROM Productos WHERE ID_Producto = @ID_Producto";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID_Producto", idProducto);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                // Llenar los controles del formulario con los datos del producto
                txtNombre.Text = reader["Nombre"].ToString();
                TextBox3.Text = reader["Cantidad_Peso_stock"].ToString();
                int idCategoria = Convert.ToInt32(reader["ID_Categoria"]);
                int idProveedor = Convert.ToInt32(reader["ID_Proveedor"]);

                // Fecha de entrada
                if (reader["Fecha_Entrada"] != DBNull.Value)
                {
                    TextBox1.Text = ((DateTime)reader["Fecha_Entrada"]).ToString("yyyy-MM-dd");
                }
                // Caducidad
                if (reader["Caducidad"] != DBNull.Value)
                {
                    TextBox2.Text = ((DateTime)reader["Caducidad"]).ToString("yyyy-MM-dd");
                }

                // Aquí debes cargar los datos de los ComboBox de acuerdo a tu lógica de negocios
                CargarCategorias();
                CargarProveedores();
                ddlCategoria.SelectedValue = idCategoria.ToString();
                ddlProveedor.SelectedValue = idProveedor.ToString();

                // Completa el resto de los campos del formulario de acuerdo a tu base de datos
            }
            else
            {
                // Si no se encuentra el producto con el ID especificado, redirigir a la página de inicio u otra página apropiada
                Response.Redirect("inicio.aspx");
            }
        }
    }

    private void CargarCategorias()
    {
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "SELECT ID, NombreCategoria FROM Categorias";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            ddlCategoria.DataSource = reader;
            ddlCategoria.DataTextField = "NombreCategoria";
            ddlCategoria.DataValueField = "ID";
            ddlCategoria.DataBind();
        }
    }

    private void CargarProveedores()
    {
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "SELECT ID, NombreEmpresa FROM Proveedores";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            ddlProveedor.DataSource = reader;
            ddlProveedor.DataTextField = "NombreEmpresa";
            ddlProveedor.DataValueField = "ID";
            ddlProveedor.DataBind();
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        int idProducto = Convert.ToInt32(Request.QueryString["id"]);
        string nombre = txtNombre.Text;
        string fechaEntrada = TextBox1.Text;
        string caducidad = TextBox2.Text;
        string cantidadPeso = TextBox3.Text;
        int idCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);
        int idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);

        // Actualizar los datos del producto en la base de datos
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "UPDATE Productos SET Nombre = @Nombre, Fecha_Entrada = @FechaEntrada, Caducidad = @Caducidad, Cantidad_Peso_Stock = @CantidadPeso, ID_Categoria = @ID_Categoria, ID_Proveedor = @ID_Proveedor WHERE ID_Producto = @ID_Producto";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@FechaEntrada", fechaEntrada);
            cmd.Parameters.AddWithValue("@Caducidad", caducidad);
            cmd.Parameters.AddWithValue("@CantidadPeso", cantidadPeso);
            cmd.Parameters.AddWithValue("@ID_Categoria", idCategoria);
            cmd.Parameters.AddWithValue("@ID_Proveedor", idProveedor);
            cmd.Parameters.AddWithValue("@ID_Producto", idProducto);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // Redirigir a la página de inicio u otra página apropiada después de guardar los cambios
        Response.Redirect("inicio.aspx");
    }
}
