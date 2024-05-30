using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Puestos
/// </summary>
public class Puestos
{
    //variables
    private string cadena;

    //propiedades
    public int id { get; set; }
    public string puesto { get; set; }

    public Puestos()
    {
        cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
    }

    //metodo para insertar en la tabla de puestos
    public void insertar()
    {
        try
        {
            string comando = "INSERT INTO puesto(puesto) VALUES(@puesto)";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@puesto", puesto));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {

            throw(ex);
        }
    }

    //metodo para modificar un puesto
    public void modificar()
    {
        try
        {
            string comando = "UPDATE puesto SET puesto = @puesto WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@puesto", puesto));
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

    //metodo para eliminar un puesto
    public void eliminar()
    {
        try
        {
            string comando = "DELETE puesto WHERE id = @id";
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

            throw(ex);
        }
    }

    //metodo para ontener un puesto
    public bool obtenerPuesto()
    {
        try
        {
            string comando = "SELECT * FROM Puesto WHERE id = @id";
            
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
                                puesto = reader["puesto"].ToString();
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

    //metodo para buscar todos los puestos y llenar un filtro
    public DataSet puestos()
    {
        try
        {
            string comando = "SELECT * FROM Puesto ORDER BY puesto";
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

    //metodo para mostrar todos los puestos con parametros
    public DataSet puestos(string param)
    {
        try
        {
            string comando = "SELECT * FROM Puesto WHERE id > 0"+ param +" ORDER BY id DESC";
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

}