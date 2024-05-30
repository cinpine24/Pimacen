using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditarUsuarios : System.Web.UI.Page
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

    private void CargarDatosProducto(int id)
    {
        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "SELECT * FROM Usuarios WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                // Llenar los controles del formulario con los datos del producto
                txtNombre.Text = reader["Nombre"].ToString();
                txtApe.Text = reader["Apellidos"].ToString();
                txtRol.Text = reader["Rol"].ToString();
                txtcontra.Text = reader["Contraseña"].ToString();

            }

            else
                {
                // Si no se encuentra el producto con el ID especificado, redirigir a la página de inicio u otra página apropiada
                Response.Redirect("Usuarios.aspx");
            }
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        int idUsuario = Convert.ToInt32(Request.QueryString["id"]);
        string nombre = txtNombre.Text;
        string Apellios = txtApe.Text;
        string Rol = txtRol.Text;
        string Contraseña = txtcontra.Text;

        // Actualizar los datos del usuario en la base de datos
        using (SqlConnection con = new SqlConnection(cadena))
        {
            // Crear la consulta SQL con los parámetros
            string query = "UPDATE Usuarios SET Nombre = @Nombre, Apellidos = @Apellidos, Rol = @Rol, Contraseña = @Contraseña WHERE ID = @ID";

            // Crear el objeto SqlCommand y asignar la consulta y la conexión
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Asignar los valores de los parámetros
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellidos", Apellios);
                cmd.Parameters.AddWithValue("@Rol", Rol);
                cmd.Parameters.AddWithValue("@Contraseña", Contraseña);
                cmd.Parameters.AddWithValue("@ID", idUsuario);

                // Abrir la conexión y ejecutar la consulta
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Redirigir a la página de usuarios después de guardar los cambios
        Response.Redirect("Usuarios.aspx");
    }

}