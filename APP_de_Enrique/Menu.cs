using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;


/// <summary>
/// Descripción breve de Menu
/// </summary>
public class Menu
{
    private string cadena;

    public int idMenu { get; set; }
    public int idMenuPadre { get; set; }
    public string descripcion { get; set; }
    public string paginaUrl { get; set; }
    public string seccion { get; set; }
    public string icono { get; set; }
    //public string url { get; set; }

    //public DateTime fechaCreacion { get; set; }

    public List<Menu> SubMenu { get; set; }
    public Menu()
    {
        cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
    }


    //metodo para obtener los Menus con parametros
    public DataTable obtenerMenus(string param = "")
    {
        try
        {
            string comando = "SELECT * FROM Menu WHERE idMenu > 0" + param + " ORDER BY seccion, idMenu";
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

    //metodo para obtener los Menus con parametros
    public DataTable obtenerMenuPorId(int idMenu)
    {
        try
        {
            string comando = "SELECT * FROM Menu WHERE idMenu = @idMenu";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@idMenu", idMenu));

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

    //metodo para obtener los Menus Padres
    public DataTable obtenerMenusPadres()
    {
        try
        {
            string comando = "SELECT * FROM Menu WHERE idMenuPadre IS NULL ORDER BY seccion, idMenu";
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

    //metodo para insertar un menú
    public void insertar(int? idMenuPadre)
    {
        try
        {
            string comando = "INSERT INTO Menu(descripcion, idMenuPadre, paginaUrl, seccion, icono)";
            comando += " VALUES(@descripcion, @idMenuPadre, @paginaUrl, @seccion, @icono)";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                    cmd.Parameters.Add(new SqlParameter("@paginaUrl", paginaUrl));
                    cmd.Parameters.Add(new SqlParameter("@seccion", seccion));
                    cmd.Parameters.Add(new SqlParameter("@idMenuPadre", idMenuPadre ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@icono", icono));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    //metodo para modificar un menú
    public void modificar(int? idMenuPadre)
    {
        try
        {
            string comando = "UPDATE Menu SET descripcion = @descripcion, paginaUrl = @paginaUrl, seccion = @seccion, idMenuPadre = @idMenuPadre WHERE idMenu = @idMenu";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@idMenu", idMenu));
                    cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                    cmd.Parameters.Add(new SqlParameter("@paginaUrl", paginaUrl));
                    cmd.Parameters.Add(new SqlParameter("@seccion", seccion));
                    cmd.Parameters.Add(new SqlParameter("@idMenuPadre", idMenuPadre ?? (object)DBNull.Value));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    public bool tieneHijos(int idMenuPadre)
    {
        try
        {
            string comando = "SELECT COUNT(*) FROM Menu WHERE idMenuPadre = @idMenuPadre";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@idMenuPadre", idMenuPadre));

                    int cantidadHijos = (int)cmd.ExecuteScalar();
                    return cantidadHijos > 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    public List<Menu> ObtenerMenusAuditorDesdeBD()
    {
        List<Menu> menus = new List<Menu>();

        try
        {


            string comando = "SELECT * FROM Menu WHERE idMenu > 0 AND seccion = 'AUDITOR' ORDER BY idMenuPadre, seccion";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<int, Menu> menuDictionary = new Dictionary<int, Menu>();

                        while (reader.Read())
                        {
                            Menu menu = new Menu
                            {
                                idMenu = Convert.ToInt32(reader["idMenu"]),
                                idMenuPadre = reader["idMenuPadre"] != DBNull.Value ? Convert.ToInt32(reader["idMenuPadre"]) : 0,
                                descripcion = Convert.ToString(reader["descripcion"]),
                                //paginaUrl = Convert.ToString(reader["paginaUrl"]),
                                paginaUrl = Convert.ToString(reader["paginaUrl"]),
                                seccion = Convert.ToString(reader["seccion"]),
                                icono = Convert.ToString(reader["icono"])
                            };

                            // Ajustar la URL según la sección y la estructura actual
                            //menu.paginaUrl = AjustarUrl(menu, seccionActual);

                            // Agregar el menú al diccionario usando el idMenu como clave
                            menuDictionary.Add(menu.idMenu, menu);

                            // Si es un menú hijo, agrégalo al menú padre correspondiente
                            if (menu.idMenuPadre != 0 && menuDictionary.ContainsKey(menu.idMenuPadre))
                            {
                                if (menuDictionary[menu.idMenuPadre].SubMenu == null)
                                {
                                    menuDictionary[menu.idMenuPadre].SubMenu = new List<Menu>();
                                }

                                menuDictionary[menu.idMenuPadre].SubMenu.Add(menu);
                            }
                            else
                            {
                                // Si es un menú padre, agrégalo a la lista principal
                                menus.Add(menu);
                            }
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

        return menus;
    }

    public List<Menu> ObtenerMenusAdminDesdeBD()
    {
        List<Menu> menus = new List<Menu>();

        try
        {


            string comando = "SELECT * FROM Menu WHERE idMenu > 0 AND seccion = 'ADMIN' ORDER BY idMenuPadre, seccion";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<int, Menu> menuDictionary = new Dictionary<int, Menu>();

                        while (reader.Read())
                        {
                            Menu menu = new Menu
                            {
                                idMenu = Convert.ToInt32(reader["idMenu"]),
                                idMenuPadre = reader["idMenuPadre"] != DBNull.Value ? Convert.ToInt32(reader["idMenuPadre"]) : 0,
                                descripcion = Convert.ToString(reader["descripcion"]),
                                //paginaUrl = Convert.ToString(reader["paginaUrl"]),
                                paginaUrl = Convert.ToString(reader["paginaUrl"]),
                                seccion = Convert.ToString(reader["seccion"]),
                                icono = Convert.ToString(reader["icono"])
                            };

                            // Ajustar la URL según la sección y la estructura actual
                            //menu.paginaUrl = AjustarUrl(menu, seccionActual);

                            // Agregar el menú al diccionario usando el idMenu como clave
                            menuDictionary.Add(menu.idMenu, menu);

                            // Si es un menú hijo, agrégalo al menú padre correspondiente
                            if (menu.idMenuPadre != 0 && menuDictionary.ContainsKey(menu.idMenuPadre))
                            {
                                if (menuDictionary[menu.idMenuPadre].SubMenu == null)
                                {
                                    menuDictionary[menu.idMenuPadre].SubMenu = new List<Menu>();
                                }

                                menuDictionary[menu.idMenuPadre].SubMenu.Add(menu);
                            }
                            else
                            {
                                // Si es un menú padre, agrégalo a la lista principal
                                menus.Add(menu);
                            }
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

        return menus;
    }

    //obtener todos los menus de la bd
    public List<Menu> ObtenerMenusDesdeBD()
    {
        List<Menu> menus = new List<Menu>();

        try
        {
            

            string comando = "SELECT * FROM Menu WHERE idMenu > 0 ORDER BY idMenuPadre, seccion";
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(comando, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<int, Menu> menuDictionary = new Dictionary<int, Menu>();

                        while (reader.Read())
                        {
                            Menu menu = new Menu
                            {
                                idMenu = Convert.ToInt32(reader["idMenu"]),
                                idMenuPadre = reader["idMenuPadre"] != DBNull.Value ? Convert.ToInt32(reader["idMenuPadre"]) : 0,
                                descripcion = Convert.ToString(reader["descripcion"]),
                                //paginaUrl = Convert.ToString(reader["paginaUrl"]),
                                paginaUrl = Convert.ToString(reader["paginaUrl"]),
                                seccion = Convert.ToString(reader["seccion"]),
                                icono = Convert.ToString(reader["icono"])
                            };

                            // Ajustar la URL según la sección y la estructura actual
                            //menu.paginaUrl = AjustarUrl(menu, seccionActual);

                            // Agregar el menú al diccionario usando el idMenu como clave
                            menuDictionary.Add(menu.idMenu, menu);

                            // Si es un menú hijo, agrégalo al menú padre correspondiente
                            if (menu.idMenuPadre != 0 && menuDictionary.ContainsKey(menu.idMenuPadre))
                            {
                                if (menuDictionary[menu.idMenuPadre].SubMenu == null)
                                {
                                    menuDictionary[menu.idMenuPadre].SubMenu = new List<Menu>();
                                }

                                menuDictionary[menu.idMenuPadre].SubMenu.Add(menu);
                            }
                            else
                            {
                                // Si es un menú padre, agrégalo a la lista principal
                                menus.Add(menu);
                            }
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

        return menus;
    }

    //private string AjustarUrl(Menu menu, string seccionActual)
    //{
    //    if (!string.IsNullOrEmpty(menu.paginaUrl))
    //    {
    //        string[] urlParts = menu.paginaUrl.Split('/');

    //        // Verificar si la URL comienza con la sección actual y una barra inclinada
    //        if (urlParts.Length > 1 && urlParts[0].Equals(seccionActual, StringComparison.OrdinalIgnoreCase))
    //        {
    //            // Eliminar la primera parte si coincide con la sección actual
    //            return string.Join("/", urlParts.Skip(1));
    //        }
    //    }

    //    // Devolver la URL original si no se realiza ningún ajuste
    //    return menu.paginaUrl;
    //}




}