using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de RutasControlDocumentos
/// </summary>
public class RutasControlDocumentos {
    private string cadena;

    public int id { get; set; }
    public string nombre { get; set; }
    public DateTime fechaCreacion { get; set; }
    public RutasControlDocumentos() {
        cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
    }

    public int insertar() {
        int idInsertado = -1;
        try {
            string comando = "INSERT INTO rutasControlDocumentos(nombre, fechaCreacion)";
            comando += " VALUES(@nombre, @fechaCreacion); SELECT SCOPE_IDENTITY();";
            using (SqlConnection conn = new SqlConnection(cadena)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@fechaCreacion", fechaCreacion));

                    // Ejecutar la consulta y obtener el ID insertado
                    idInsertado = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        catch (Exception ex) {

            throw (ex);
        }
        return idInsertado;
    }

    //metodo para modificar una categoria
    public void modificar() {
        try {
            string comando = "UPDATE rutasControlDocumentos SET nombre = @nombre WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(cadena)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex) {

            throw (ex);
        }
    }

    //metodo parar eliminar una categoria
    public void eliminar() {
        try {
            string comando = "DELETE rutasControlDocumentos WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(cadena)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex) {

            throw (ex);
        }
    }

    //metodo para obtener los deptos con parametros
    public DataTable obtenerRutas(string param = "") {
        try {
            string comando = "SELECT * FROM rutasControlDocumentos WHERE id > 0" + param + " ORDER BY nombre";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(cadena)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
                        da.Fill(dt);
                        return dt;
                    }
                }

            }
        }
        catch (Exception ex) {

            throw (ex);
        }
    }

    public bool getRuta() {
        try {
            string comando = "SELECT * FROM rutasControlDocumentos WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(cadena)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                nombre = reader["nombre"].ToString();
                                fechaCreacion = DateTime.Parse(reader["fechaCreacion"].ToString());
                                
                            }
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                }
            }
        }
        catch (Exception ex) {
            throw (ex);
        }
    }
}