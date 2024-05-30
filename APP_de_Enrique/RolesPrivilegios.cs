using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de RolesPrivilegios
/// </summary>
public class RolesPrivilegios
{
    private string cadena;

    public int IdMenu { get; set; }
    public int IdRol { get; set; }
    public RolesPrivilegios()
    {
        cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
    }

    public void GuardarRelacionRolMenu(int idRol, int idMenu)
    {
        try
        {
            // Verificar si la relación ya existe
            if (!ExisteRelacionRolMenu(idRol, idMenu))
            {
                string comando = "INSERT INTO RolesPrivilegios(IdRol, IdMenu) VALUES(@IdRol, @IdMenu)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@IdRol", idRol));
                        cmd.Parameters.Add(new SqlParameter("@IdMenu", idMenu));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Manejar la excepción según tus necesidades
            throw ex;
        }
    }

    // Nuevo método para verificar si la relación ya existe
    private bool ExisteRelacionRolMenu(int idRol, int idMenu)
    {
        try
        {
            string comando = "SELECT COUNT(*) FROM RolesPrivilegios WHERE IdRol = @IdRol AND IdMenu = @IdMenu";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@IdRol", idRol));
                    cmd.Parameters.Add(new SqlParameter("@IdMenu", idMenu));

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        catch (Exception ex)
        {
            // Manejar la excepción según tus necesidades
            throw ex;
        }
    }


    public DataTable ObtenerMenusPorRol(int idRol)
    {
        try
        {
            string comando = "SELECT IdMenu FROM RolesPrivilegios WHERE IdRol = @IdRol";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@IdRol", idRol));
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Manejar la excepción según tus necesidades
            throw ex;
        }
    }

    // Añade este nuevo método para eliminar una relación RolesPrivilegios
    public void EliminarRelacionRolMenu(int idRol, int idMenu)
    {
        try
        {
            string comando = "DELETE FROM RolesPrivilegios WHERE IdRol = @IdRol AND IdMenu = @IdMenu";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@IdRol", idRol));
                    cmd.Parameters.Add(new SqlParameter("@IdMenu", idMenu));

                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            // Manejar la excepción según tus necesidades
            throw ex;
        }
    }

    // Método para obtener los ID de menús a los que el rol tiene acceso
    public List<int> ObtenerMenusPorRolLista(int idRol)
    {
        List<int> menuIds = new List<int>();

        try
        {
            string comando = "SELECT idMenu FROM RolesPrivilegios WHERE idRol = @idRol";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.AddWithValue("@idRol", idRol);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idMenu = Convert.ToInt32(reader["idMenu"]);
                            menuIds.Add(idMenu);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones
            throw ex;
        }

        return menuIds;
    }
}

