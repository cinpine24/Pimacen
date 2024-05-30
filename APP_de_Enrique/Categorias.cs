using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for Categoria
/// </summary>
public class Categorias
{
    //variables
    private string cadena;

    //propiedades
    public int idCat { get; set; }
    public string categoria { get; set; }
    public int idusuario { get; set; }

    //constructor
    public Categorias()
    {
        cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
    }

    //metodo para insertar una categoria
    public void insertar()
    {
        try
        {
            string comando = "INSERT INTO categoria(categoria)";
            comando += " VALUES(@categoria)";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@categoria", categoria));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {

            throw(ex);
        }
    }

    //metodo para modificar una categoria
    public void modificar()
    {
        try
        {
            string comando = "UPDATE categoria SET categoria = @categoria WHERE IdCat = @id";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@categoria", categoria));
                    cmd.Parameters.Add(new SqlParameter("@id", idCat));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {

            throw (ex);
        }
    }

    //metodo parar eliminar una categoria
    public void eliminar()
    {
        try
        {
            string comando = "DELETE categoria WHERE idCat = @id";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", idCat));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {

            throw(ex);
        }
    }

    //metodo para buscar todas las categorias
    public bool buscarCategoria()
    {
        try
        {
            string comando = "SELECT categoria FROM categoria WHERE idCat = @id";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", idCat));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                categoria = reader["categoria"].ToString();
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


    //metodo para buscar todas las categorias lpa
    public string buscarCategoriaLPA()
    {
        try
        {
            string comando = "SELECT categoria FROM categoria WHERE idCat = @id";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", idCat));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                categoria = reader["categoria"].ToString();
                            }

                            return categoria;
                        }
                        else
                        {
                            return "";
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



    //metodo para buscar si ya existe una categoria con el mismo nombre
    public bool existeCategoria(string cat)
    {
        try
        {
            string comando = "SELECT Id FROM categoria WHERE categoria = @cat";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@cat", cat));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
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

    //metodo para traer todos los deptos
    public DataTable deptos()
    {
        try
        {
            string comando = "SELECT idCat, categoria FROM categoria ORDER BY categoria";
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

    //metodo para obtener los deptos con parametros
    public DataTable obtenerDeptos(string param = "")
    {
        try
        {
            string comando = "SELECT idCat, categoria FROM categoria WHERE idCat > 0"+ param +" ORDER BY categoria";
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