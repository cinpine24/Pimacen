using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Proveedores : System.Web.UI.Page
{
    string cadena;

    protected void Page_Load(object sender, EventArgs e)
    {
        cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        if (!IsPostBack)
        {
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
            string query = "SELECT ID, NombreEmpresa FROM Proveedores ";

            // Crear el comando SQL y el adaptador de datos
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            // Llenar el DataTable con los datos de la base de datos
            adapter.Fill(dt);

            // Asignar el DataTable como origen de datos del ListView
            ListView2.DataSource = dt;
            ListView2.DataBind();
        }
    }

    protected void btnGuardarProveedor_Click(object sender, EventArgs e)
    {
        string nombreProveedor = txtNombreProveedor.Value; // Obtener el nombre del proveedor ingresado en el formulario

        // Realizar la inserción en la base de datos
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "INSERT INTO Proveedores (NombreEmpresa) VALUES (@NombreEmpresa)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@NombreEmpresa", nombreProveedor);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // Volver a cargar los datos en el ListView para mostrar el nuevo proveedor
        BindData();

        // Limpiar el campo de texto después de agregar el proveedor
        txtNombreProveedor.Value = "";
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        // Obtener el ID del proveedor que se va a eliminar
        Button btnEliminar = (Button)sender;
        string id = btnEliminar.CommandArgument;

        // Verificar si existen productos asociados al proveedor
        bool productosAsociados = false;
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "SELECT COUNT(*) FROM Productos WHERE ID_Proveedor = @ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", id);

            con.Open();
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                productosAsociados = true;
            }
        }

        // Mostrar alerta si existen productos asociados
        if (productosAsociados)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se puede eliminar este proveedor porque tiene productos asociados.');", true);
        }
        else
        {
            // Realizar la eliminación en la base de datos si no hay productos asociados
            using (SqlConnection con = new SqlConnection(cadena))
            {
                string query = "DELETE FROM Proveedores WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // Volver a cargar los datos en el ListView para que se refleje la eliminación
            BindData();
        }
    }
}
