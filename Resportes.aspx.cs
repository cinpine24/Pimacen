using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;

public partial class Resportes : System.Web.UI.Page
{
    string cadena;
    protected void Page_Load(object sender, EventArgs e)
    {
        cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        if (!IsPostBack)
        {
            // Consulta SQL para obtener la cantidad de productos por categoría y los días faltantes para la caducidad
            string query = "SELECT Nombre, Caducidad, DATEDIFF(day, GETDATE(), Caducidad) AS DiasFaltantes FROM Productos";

            // Adaptador de datos para ejecutar la consulta y llenar un DataTable
            SqlDataAdapter adapter = new SqlDataAdapter(query, cadena);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Ordenar el DataTable por la columna 'DiasFaltantes' en orden descendente
            dataTable.DefaultView.Sort = "DiasFaltantes DESC";
            DataTable sortedTable = dataTable.DefaultView.ToTable();

            // Configurar la serie de datos
            var series = Chart1.Series["Series1"];
            series.Points.DataBindXY(
                sortedTable.AsEnumerable().Select(row => row["Caducidad"].ToString() + " - " + row["Nombre"].ToString()).ToArray(),
                sortedTable.AsEnumerable().Select(row => Math.Max(0, Convert.ToInt32(row["DiasFaltantes"]))).ToArray()
            );

            // Configurar el tipo de gráfico
            series.ChartType = SeriesChartType.Column;

            // Configurar títulos y leyendas
            Chart1.Titles.Add("Cantidad de Productos por Categoría");
            Chart1.ChartAreas[0].AxisX.Title = "Caducidad - Nombre";
            Chart1.ChartAreas[0].AxisY.Title = "Días Faltantes";

            // Cambiar el color de las barras según los días faltantes
            foreach (DataPoint point in series.Points)
            {
                int diasFaltantes = (int)point.YValues[0];

                if (diasFaltantes >= 20)
                {
                    point.Color = System.Drawing.Color.Green;
                }
                else if (diasFaltantes <= 20 && diasFaltantes > 5)
                {
                    point.Color = System.Drawing.Color.Orange;
                }
                else
                {
                    point.Color = System.Drawing.Color.Red;
                }
            }

            // Agregar leyenda
            Chart1.Legends.Add(new Legend("SignificadoColores"));
          
            Chart1.Legends["SignificadoColores"].Title = "Significado de los Colores";
            Chart1.Legends["SignificadoColores"].CustomItems.Add(System.Drawing.Color.Green, "Caducidad >= 20 días");
            Chart1.Legends["SignificadoColores"].CustomItems.Add(System.Drawing.Color.Orange, "Caducidad entre 5 y 19 días");
            Chart1.Legends["SignificadoColores"].CustomItems.Add(System.Drawing.Color.Red, "Caducidad <= 4 días");
        }
    }

}

