using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        // Obtener las credenciales ingresadas por el usuario
        string usuario = txtUsuario.Text.Trim();
        string contraseña = txtContraseña.Text.Trim();

        // Cadena de conexión a la base de datos desde el archivo web.config
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        // Consulta SQL para verificar las credenciales
        string query = "SELECT COUNT(*) FROM Usuarios WHERE ID = @Usuario AND Contraseña = @Contraseña";

        // Utilizar un bloque try-catch para manejar excepciones
        try
        {
            // Establecer la conexión con la base de datos
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                con.Open();

                // Crear un comando SQL y asignar la conexión y la consulta
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Agregar parámetros para evitar la inyección SQL
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Contraseña", contraseña);

                    // Ejecutar la consulta y obtener el resultado (número de filas afectadas)
                    int count = (int)cmd.ExecuteScalar();

                    // Verificar si se encontraron coincidencias de credenciales
                    if (count > 0)
                    {
                        // Credenciales válidas, redirigir a la página de inicio
                        Response.Redirect("inicio.aspx");
                    }
                    else
                    {
                        // Credenciales inválidas, mostrar un mensaje de error
                        Response.Write("<script>alert('Usuario o contraseña incorrectos');</script>");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Capturar y manejar cualquier excepción
       
        }
    }
}
