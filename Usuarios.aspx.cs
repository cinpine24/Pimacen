using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Usuarios : System.Web.UI.Page
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
            string query = "SELECT ID, Nombre, Apellidos, Rol, Contraseña FROM Usuarios ";

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

    protected void btnGuardarUsuario_Click(object sender, EventArgs e)
    {
        string nombreUsuario = txtNombreUsuario.Value;
        string apellidosUsuario = txtApellidosUsuario.Value;
        string rolUsuario = txtRolUsuario.Value;
        string contraseñaUsuario = txtContraseñaUsuario.Value;

        // Realizar la inserción en la base de datos
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "INSERT INTO Usuarios (Nombre, Apellidos, Rol, Contraseña) VALUES (@Nombre, @Apellidos, @Rol, @Contraseña)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Nombre", nombreUsuario);
            cmd.Parameters.AddWithValue("@Apellidos", apellidosUsuario);
            cmd.Parameters.AddWithValue("@Rol", rolUsuario);
            cmd.Parameters.AddWithValue("@Contraseña", contraseñaUsuario);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // Volver a cargar los datos en el ListView para mostrar el nuevo usuario
        BindData();

        // Limpiar los campos del formulario después de agregar el usuario
        txtNombreUsuario.Value = "";
        txtApellidosUsuario.Value = "";
        txtRolUsuario.Value = "";
        txtContraseñaUsuario.Value = "";
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        // Obtener el ID del producto que se va a eliminar
        Button btnEliminar = (Button)sender;
        string id = btnEliminar.CommandArgument;

        // Realizar la eliminación en la base de datos
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "DELETE FROM Usuarios WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", id);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // Volver a cargar los datos en el ListView para que se refleje la eliminación
        BindData();
    }
}
