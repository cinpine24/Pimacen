using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Usuarios
/// </summary>
public class Usuarios
{
    //variables
    private string cadena;

    //propiedades
    public int id { get; set; }
    public string nombre { get; set; }
    public string noEmpleado { get; set; }
    public string usuario { get; set; }
    public string password { get; set; }
    public string oficina { get; set; }
    public string sam { get; set; }
    public string activo { get; set; }
    public string telefono { get; set; }
    public string email { get; set; }
    public string foto { get; set; }
    public int categoria { get; set; }
    public int puesto { get; set; }
    public int edad { get; set; }
    public int area { get; set; }
    public string sexo { get; set; }
    public string nivel { get; set; }
    public string depa { get; set; }

    public DateTime fechaAlta { get; set; }
    public int idRutaControlDocumentos { get; set; }
    public int idRol { get; set; }

    //Constructor
    public Usuarios()
    {
        cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
    }

    //metodo para login
    public bool login(string user, string pass)
    {
        try
        {
            string comando = "SELECT usuario, Nombre, password, Id, foto, fechaAlta, categoria, puesto, edad, sexo, noEmpleado, oficina, email, idRutaControlDocumentos ";
            comando += " FROM users WHERE usuario = @user AND password = @pass AND activo = 'SI'";

            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@user", user));
                    cmd.Parameters.Add(new SqlParameter("@pass", pass));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                usuario = reader["usuario"].ToString();
                                nombre = reader["nombre"].ToString();
                                password = reader["password"].ToString();
                                id = int.Parse(reader["id"].ToString());
                                foto = reader["foto"].ToString();
                                fechaAlta = DateTime.Parse(reader["fechaAlta"].ToString());
                                puesto = int.Parse(reader["puesto"].ToString());
                                categoria = int.Parse(reader["categoria"].ToString());
                                edad = int.Parse(reader["edad"].ToString());
                                sexo = reader["sexo"].ToString();
                                email = reader["email"].ToString();
                                int idRuta;
                                if (int.TryParse(reader["idRutaControlDocumentos"].ToString(), out idRuta)) {
                                    idRutaControlDocumentos = idRuta;
                                }
                                else {
                                    idRutaControlDocumentos = 1;
                                }
                                
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

    //metodo para insertar un nuevo usuario
    public void insertar()
    {
        try
        {
            string comando = "INSERT INTO users(Nombre, Usuario, Password, Activo, Telefono, Email, FechaAlta, foto, categoria, puesto, edad, sexo, noEmpleado, oficina, sam)";
            comando += "VALUES(@nombre, @usuario, @password,  'SI', @telefono, @email, GETDATE(), @foto, @categoria, @puesto, @edad, @sexo, @empleado, @oficina, @sam)";

            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
                    cmd.Parameters.Add(new SqlParameter("@password", password));
                    cmd.Parameters.Add(new SqlParameter("@telefono", telefono));
                    cmd.Parameters.Add(new SqlParameter("@email", email));
                    cmd.Parameters.Add(new SqlParameter("@foto", foto));
                    cmd.Parameters.Add(new SqlParameter("@categoria", categoria));
                    cmd.Parameters.Add(new SqlParameter("@puesto", puesto));
                    cmd.Parameters.Add(new SqlParameter("@edad", edad));
                    cmd.Parameters.Add(new SqlParameter("@sexo", sexo));
                    cmd.Parameters.Add(new SqlParameter("@empleado", noEmpleado));
                    cmd.Parameters.Add(new SqlParameter("@oficina", oficina));
                    cmd.Parameters.Add(new SqlParameter("@sam", sam));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    //metdodo para actualizar un usuario
    public void modificar()
    {
        try
        {
            string comando = "UPDATE users SET Nombre = @nombre, Usuario = @usuario, Password = @password, Activo = @activo,";
            comando += " Telefono = @telefono, Email = @email, Foto = @foto, categoria = @categoria, edad = @edad, sexo = @sexo,";
            comando += " noEmpleado = @empleado, oficina = @oficina, sam = @sam WHERE Id = @id";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
                    cmd.Parameters.Add(new SqlParameter("@password", password));
                    cmd.Parameters.Add(new SqlParameter("@activo", activo));
                    cmd.Parameters.Add(new SqlParameter("@telefono", telefono));
                    cmd.Parameters.Add(new SqlParameter("@email", email));
                    cmd.Parameters.Add(new SqlParameter("@foto", foto));
                    cmd.Parameters.Add(new SqlParameter("@categoria", categoria));
                    cmd.Parameters.Add(new SqlParameter("@edad", edad));
                    cmd.Parameters.Add(new SqlParameter("@sexo", sexo));
                    cmd.Parameters.Add(new SqlParameter("@empleado", noEmpleado));
                    cmd.Parameters.Add(new SqlParameter("@oficina", oficina));
                    cmd.Parameters.Add(new SqlParameter("@sam", sam));
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


    //metodo para eliminar un usuario
    public void eliminar()
    {
        try
        {
            string comando = "DELETE users WHERE id = @id";
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

    //metodo para buscar un usuario
    public void obtenerUsuario()
    {
        try
        {
            string comando = "SELECT * FROM users WHERE id = @id";
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
                                nombre = reader["Nombre"].ToString();
                                noEmpleado = reader["noEmpleado"].ToString();
                                oficina = reader["oficina"].ToString();
                                usuario = reader["Usuario"].ToString();
                                password = reader["Password"].ToString();
                                activo = reader["Activo"].ToString();
                                telefono = reader["Telefono"].ToString();
                                email = reader["Email"].ToString();
                                foto = reader["foto"].ToString();
                                categoria = int.Parse(reader["categoria"].ToString());
                                puesto = int.Parse(reader["puesto"].ToString());
                                edad = int.Parse(reader["edad"].ToString());
                                sexo = reader["sexo"].ToString();
                                sam = reader["sam"].ToString();
                                // Validación para el campo "area"
                                if (!reader.IsDBNull(reader.GetOrdinal("area")))
                                {
                                    area = int.Parse(reader["area"].ToString());
                                }
                                else
                                {
                                    // El campo "area" es nulo, puedes asignar un valor por defecto o manejarlo según tus necesidades.
                                    area = 0; // Por ejemplo, asignamos 0 si es nulo.
                                }
                                int idRuta;
                                if (int.TryParse(reader["idRutaControlDocumentos"].ToString(), out idRuta)) {
                                    idRutaControlDocumentos = idRuta;
                                }
                                else {
                                    idRutaControlDocumentos = 1;
                                }
                                if (reader["idRol"] != DBNull.Value)
                                {
                                    idRol = int.Parse(reader["idRol"].ToString());
                                }
                                else
                                {
                                    // Asignar un valor predeterminado o manejar la situación según tus necesidades
                                    idRol = 0; // Cambia esto según tu lógica
                                }
                            }
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

    //metodo para buscar el departamento del usuario
    public void obtenerDepartamentoUsuario()
    {
        try
        {
            string comando = "SELECT U.categoria, C.categoria AS departamento FROM users as U inner join categoria as C on U.categoria = C.idCat WHERE id = @id";
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
                                depa = reader["departamento"].ToString();
                                categoria = int.Parse(reader["categoria"].ToString()); 
                            }
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

    //metodos para buscar todos los usuarios
    public DataTable users(string param)
    {
        try
        {
            string comando = "SELECT users.id AS id, nombre, telefono, categoria.categoria, puesto.puesto, email, fechaAlta, activo, foto";
            comando += " FROM dbo.users JOIN categoria ON idCat = users.categoria JOIN puesto ON puesto.id = users.puesto WHERE users.Id > 0" + param + " ORDER BY Id DESC";
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

    public DataTable users()
    {
        try
        {
            string comando = "SELECT id, Nombre FROM users ORDER BY Nombre";

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

    //metodo para saber si ya existe el usuario registrado
    public bool existeUsuario(string user)
    {
        try
        {
            string comando = "SELECT Id FROM users WHERE usuario = @user";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@user", user));
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

    //metodo para saber si ya se registró el nombre del usuario
    public bool existeNombreUsuario()
    {
        try
        {
            string comando = "SELECT Id FROM users WHERE Nombre = @nombre";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
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

    //metodo para modificar el perfil del usuario
    public void modificarPerfil()
    {
        try
        {
            string comando = "UPDATE users SET Usuario = @usuario, Password = @password, Email = @email, Foto = @foto";
            comando += " WHERE Id = @id";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
                    cmd.Parameters.Add(new SqlParameter("@password", password));
                    cmd.Parameters.Add(new SqlParameter("@email", email));
                    cmd.Parameters.Add(new SqlParameter("@foto", foto));
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

    //metodo para obtener el id y nombre de usuarios



    #region LPA

    //metodos para buscar todos los usuarios
    public DataTable usersLPA()
    {
        try
        {
            string comando = "SELECT users.id AS id, nombre, Usuario, nivel, telefono, categoria.categoria, puesto.puesto, email, fechaAlta, activo, foto, email";
            comando += " FROM dbo.users JOIN categoria ON idCat = users.categoria JOIN puesto ON puesto.id = users.puesto WHERE users.Id > 0" + " ORDER BY Id DESC";
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
    public DataTable usersLPA(string param)
    {
        try
        {
            string comando = "SELECT users.id AS id, nombre, Usuario, nivel, telefono, categoria.categoria, puesto.puesto, email, fechaAlta, activo, foto, email";
            comando += " FROM dbo.users JOIN categoria ON idCat = users.categoria JOIN puesto ON puesto.id = users.puesto " +
                "WHERE users.Id > 0" + param + " ORDER BY Id DESC";
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

    // Método para actualizar un usuario LPA
    public void modificarLPA()
    {
        try
        {
            string comando = "UPDATE users SET Nombre = @nombre, Usuario = @usuario, Password = @password,";
            comando += " Categoria = @categoria, Puesto = @puesto, Nivel = @nivel, Foto = @foto, Email = @email, idRutaControlDocumentos = @idRutaControlDocumentos, idRol = @idRol " +
                " WHERE Id = @id";

            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
                    cmd.Parameters.Add(new SqlParameter("@password", password));
                    cmd.Parameters.Add(new SqlParameter("@nivel", nivel));
                    cmd.Parameters.Add(new SqlParameter("@foto", foto));
                    cmd.Parameters.Add(new SqlParameter("@categoria", categoria));
                    cmd.Parameters.Add(new SqlParameter("@puesto", puesto));
                    cmd.Parameters.Add(new SqlParameter("@email", email));
                    cmd.Parameters.Add(new SqlParameter("@idRutaControlDocumentos", idRutaControlDocumentos));
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@idRol", idRol));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    //metodo para insertar un nuevo user en LPA
    public void insertarUserLPA()
    {
        try
        {

            string comando = "INSERT INTO users(Nombre, Usuario, Password,  categoria, puesto, nivel, foto, email, Activo, fechaalta, edad, idRutaControlDocumentos, idRol)";
            comando += "VALUES(@nombre, @usuario, @password, @categoria, @puesto, @nivel, @foto, @email, @Activo, GETDATE(), @edad, @idRutaControlDocumentos, @idRol)";


            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
                    cmd.Parameters.Add(new SqlParameter("@password", password));
                    cmd.Parameters.Add(new SqlParameter("@nivel", nivel));
                    if (foto == null || foto.Length == 0)
                    {
                        foto = "";
                    }
                    cmd.Parameters.Add(new SqlParameter("@foto", foto));

                    cmd.Parameters.Add(new SqlParameter("@categoria", categoria));
                    cmd.Parameters.Add(new SqlParameter("@puesto", puesto));

                    cmd.Parameters.Add(new SqlParameter("@Activo", "SI"));
                    cmd.Parameters.Add(new SqlParameter("@edad", 1));


                    cmd.Parameters.Add(new SqlParameter("@email", email));
                    cmd.Parameters.Add(new SqlParameter("@idRutaControlDocumentos", idRutaControlDocumentos));

                    cmd.Parameters.Add(new SqlParameter("@idRol", idRol));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    // Método para buscar un usuario LPA
    public void obtenerUsuarioLPA()
    {
        try
        {
            string comando = "SELECT Nombre, Usuario, Password, Categoria, Puesto, Nivel, Foto, Email, idRol FROM users WHERE id = @id";
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
                                nombre = reader["Nombre"].ToString();
                                usuario = reader["Usuario"].ToString();
                                password = reader["Password"].ToString();
                                foto = reader["Foto"].ToString();
                                categoria = int.Parse(reader["Categoria"].ToString());
                                puesto = int.Parse(reader["Puesto"].ToString());
                                nivel = reader["Nivel"].ToString();
                                email = reader["Email"].ToString();
                                // Validación para evitar errores si idRol es nulo en la base de datos
                                if (reader["idRol"] != DBNull.Value)
                                {
                                    idRol = int.Parse(reader["idRol"].ToString());
                                }
                                else
                                {
                                    // Asignar un valor predeterminado o manejar la situación según tus necesidades
                                    idRol = 0; // Cambia esto según tu lógica
                                }
                            }
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


    // Método para buscar un usuario LPA por su nombre
    public void obtenerUsuarioLPAByName()
    {
        try
        {
            string comando = "SELECT id, Nombre, Usuario, Password, Categoria, Puesto, Nivel, Foto, Email FROM users WHERE nombre = @nombre";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                id = int.Parse(reader["id"].ToString());
                                nombre = reader["Nombre"].ToString();
                                usuario = reader["Usuario"].ToString();
                                password = reader["Password"].ToString();
                                foto = reader["Foto"].ToString();
                                categoria = int.Parse(reader["Categoria"].ToString());
                                puesto = int.Parse(reader["Puesto"].ToString());
                                nivel = reader["Nivel"].ToString();
                                email = reader["Email"].ToString();
                            }
                        }
                        else {
                            id = 0;
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

    // Método para buscar un usuario LPA por su id
    public void obtenerUsuarioLPAByID()
    {
        try
        {
            string comando = "SELECT id, Nombre, Usuario, Password, Categoria, Puesto, Nivel, Foto, Email FROM users WHERE id = @id";
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
                                id = int.Parse(reader["id"].ToString());
                                nombre = reader["Nombre"].ToString();
                                usuario = reader["Usuario"].ToString();
                                password = reader["Password"].ToString();
                                foto = reader["Foto"].ToString();
                                categoria = int.Parse(reader["Categoria"].ToString());
                                puesto = int.Parse(reader["Puesto"].ToString());
                                nivel = reader["Nivel"].ToString();
                                email = reader["Email"].ToString();
                            }
                        }
                        else
                        {
                            id = 0;
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

    public bool usuarioExiste(string nombre) {
        try {
            string comando = "SELECT COUNT(*) FROM users WHERE nombre = @nombre";
            using (SqlConnection conn = new SqlConnection(cadena)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public bool usuarioExisteMod(string nombre) {
        try {
            string comando = "SELECT COUNT(*) FROM users WHERE nombre = @nombre AND id <> @id";
            using (SqlConnection conn = new SqlConnection(cadena)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    #endregion
}