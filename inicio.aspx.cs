using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class inicio : System.Web.UI.Page
{
    string cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Poblar los ComboBox solo la primera vez que se carga la página
            PopulateCategoriaDropDown();
            PopulateProveedorDropDown();
            PopulateTipoUnidadDropDown(); // Agregar esta línea para cargar los tipos de unidades
            BindData();
        }
    }

    protected void listIngredientes_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        // Establecer la conexión con la base de datos
        using (SqlConnection con = new SqlConnection(cadena))
        {
            // Consulta SQL para obtener los datos
            string query = "SELECT ID_Producto, Nombre, Fecha_Entrada, Caducidad, Cantidad_Peso_Stock, ID_Categoria, ID_Proveedor, Unidades FROM Productos ";

            // Crear el comando SQL y el adaptador de datos
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            // Llenar el DataTable con los datos de la base de datos
            adapter.Fill(dt);

            // Asignar el DataTable como origen de datos del ListView
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
    }

    protected void btnGuardarProducto_Click(object sender, EventArgs e)
    {
        string nombreProducto = txtNombre.Value;
        DateTime fechaEntrada = DateTime.Parse(Text1.Value);
        DateTime Caducidad = DateTime.Parse(Text2.Value);
        string Cantidad = Text3.Value;
        string Categoria = Text4.SelectedItem.Text;
        string Proveedor = Text5.SelectedItem.Text;
        string TipoUnidad = Text6.SelectedItem.Text; // Obtener el texto seleccionado del DropDownList Text6

        int Unidades = 0;
        // Obtener el ID del tipo de unidad seleccionado
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "SELECT ID FROM Unidades WHERE Tipo = @Tipo";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Tipo", TipoUnidad);

            con.Open();
            Unidades = (int)cmd.ExecuteScalar(); // Suponiendo que el ID es de tipo INT
        }

        // Realiza la inserción en la base de datos
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "INSERT INTO Productos (Nombre, Fecha_Entrada, Caducidad, Cantidad_Peso_Stock, ID_Categoria, ID_Proveedor, Unidades) VALUES (@Nombre, @Fecha_Entrada, @Caducidad, @Cantidad_Peso_Stock, (SELECT ID FROM Categorias WHERE NombreCategoria = @NombreCategoria), (SELECT ID FROM Proveedores WHERE NombreEmpresa = @NombreEmpresa), @Unidades)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Nombre", nombreProducto);
            cmd.Parameters.AddWithValue("@Fecha_Entrada", fechaEntrada);
            cmd.Parameters.AddWithValue("@Caducidad", Caducidad);
            cmd.Parameters.AddWithValue("@Cantidad_Peso_Stock", Cantidad);
            cmd.Parameters.AddWithValue("@NombreCategoria", Categoria);
            cmd.Parameters.AddWithValue("@NombreEmpresa", Proveedor);
            cmd.Parameters.AddWithValue("@Unidades", Unidades);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // Vuelve a cargar los datos en el ListView para que se muestre el nuevo producto
        BindData();
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        // Obtener el ID del producto que se va a eliminar
        Button btnEliminar = (Button)sender;
        string idProducto = btnEliminar.CommandArgument;

        // Realizar la eliminación en la base de datos
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "DELETE FROM Productos WHERE ID_Producto = @ID_Producto";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID_Producto", idProducto);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // Volver a cargar los datos en el ListView para que se refleje la eliminación
        BindData();
    }

    private void PopulateCategoriaDropDown()
    {
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "SELECT NombreCategoria FROM Categorias";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Limpiar el ComboBox antes de agregar los elementos
            Text4.Items.Clear();

            while (reader.Read())
            {
                Text4.Items.Add(reader["NombreCategoria"].ToString());
            }
        }
    }

    private void PopulateProveedorDropDown()
    {
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "SELECT NombreEmpresa FROM Proveedores";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Limpiar el ComboBox antes de agregar los elementos
            Text5.Items.Clear();

            while (reader.Read())
            {
                Text5.Items.Add(reader["NombreEmpresa"].ToString());
            }
        }
    }

    private void PopulateTipoUnidadDropDown()
    {
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "SELECT Tipo FROM Unidades";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Limpiar el ComboBox antes de agregar los elementos
            Text6.Items.Clear();

            while (reader.Read())
            {
                Text6.Items.Add(reader["Tipo"].ToString());
            }
        }
    }
}
