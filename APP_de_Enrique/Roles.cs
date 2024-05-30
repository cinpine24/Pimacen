using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class Roles
{
    // Variables
    private string cadena;

    // Propiedades
    public int id { get; set; }
    public string nombre { get; set; }
    public DateTime fecha_alta { get; set; }

    public Roles()
    {
        cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
    }

    // Método para insertar un nuevo rol en la tabla "Roles"
    public void insertar()
    {
        try
        {
            string comando = "INSERT INTO Roles (nombre, fecha_alta) VALUES (@nombre, GETDATE())";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
          

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    // Método para modificar un rol en la tabla "Roles"
    public void modificar()
    {
        try
        {
            string comando = "UPDATE Roles SET nombre = @nombre, fecha_alta = GETDATE() WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    // Método para eliminar un rol de la tabla "Roles"
    public void eliminar()
    {
        try
        {
            string comando = "DELETE FROM Roles WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    // Método para obtener un rol de la tabla "Roles" por su ID
    public bool obtenerRol()
    {
        try
        {
            string comando = "SELECT * FROM Roles WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                nombre = reader["nombre"].ToString();
                                fecha_alta = Convert.ToDateTime(reader["fecha_alta"]);
                            }
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    // Método para obtener todos los roles de la tabla "Roles"
    public DataSet obtenerRoles()
    {
        try
        {
            string comando = "SELECT * FROM Roles ORDER BY id";
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    //metodo para mostrar todos los roles con parametros
    public DataSet roles(string param)
    {
        try
        {
            string comando = "SELECT * FROM Roles WHERE id > 0" + param + " ORDER BY id DESC";
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                        return ds;
                    }
                }

            }
        }
        catch (Exception ex)
        {

            throw (ex);
        }
    }

    //metodo para obtener un datatable de todos los roles
    public DataTable obtenerRolesDT()
    {
        try
        {
            string comando = "SELECT * FROM Roles order by nombre";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                        return dt;
                    }
                }

            }
        }
        catch (Exception ex)
        {

            throw (ex);
        }
    }



}
