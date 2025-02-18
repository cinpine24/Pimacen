﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditarProveedores : System.Web.UI.Page
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
            string query = "SELECT * FROM Proveedores WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                // Llenar los controles del formulario con los datos del producto
                txtNombre.Text = reader["NombreEmpresa"].ToString();
            }
            else
            {
                // Si no se encuentra el producto con el ID especificado, redirigir a la página de inicio
                Response.Redirect("Proveedores.aspx");
            }
        }
    }



    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        int idProveedor  = Convert.ToInt32(Request.QueryString["id"]);
        string nombre = txtNombre.Text;

        using (SqlConnection con = new SqlConnection(cadena))
        {
            string query = "UPDATE Proveedores SET NombreEmpresa = @NombreEmpresa WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@NombreEmpresa", nombre);
            cmd.Parameters.AddWithValue("@ID", idProveedor);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        Response.Redirect("Proveedores.aspx");
    }

}