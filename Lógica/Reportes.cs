using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;
using Datos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Diagnostics;


namespace Lógica
{
    public class Reportes
    {
        Conexion con = new Conexion();
        Venta ven = new Venta();
        Cliente cli = new Cliente();
        public void exportar_excel(DataGridView tabla)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            int indiceColumna = 0;

            foreach (DataGridViewColumn col in tabla.Columns)
            {
                indiceColumna++;
                excel.Cells[1, indiceColumna] = col.Name;
            }
            int indexFila = 0;
            foreach (DataGridViewRow row in tabla.Rows)
            {
                indexFila++;
                indiceColumna = 0;
                foreach (DataGridViewColumn col in tabla.Columns)
                {
                    indiceColumna++;
                    excel.Cells[indexFila + 1, indiceColumna] = row.Cells[col.Name].Value;
                }
            }
            excel.Visible = true;
        }
        public void PDF_venta(DataGridView Ta)
        {
            Document doc = new Document(PageSize.A5.Rotate(), 10, 10, 10, 10);
            string filename = "Factura.pdf";
            FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            PdfWriter.GetInstance(doc, file);
            doc.Open();
            Paragraph f = new Paragraph();
            f.Alignment = Element.ALIGN_CENTER;
            f.Add("COLEGIO CLARETIANO \n PEI: ''Camino hacia la identidad'' \n\n");
            doc.Add(f);
            Chunk Linea1 = new Chunk("Vendedor", FontFactory.GetFont("Courier New", 12, BaseColor.BLACK));
            Chunk Linea2 = new Chunk("Nombre vendedor: " + cli.Nombre, FontFactory.GetFont("Courier New", 12, BaseColor.DARK_GRAY));
            Chunk Linea3 = new Chunk("Fecha venta: " + ven.Fecha, FontFactory.GetFont("Courier New", 12, BaseColor.DARK_GRAY));

            /*1*/
            doc.Add(new Paragraph(Linea1));
            /*2*/
            doc.Add(new Paragraph(Linea2));
            /*3*/
            doc.Add(new Paragraph(Linea3));
            doc.Add(new Paragraph("\n \n \n \n "));
            GenerarDocumento(doc, Ta);
            doc.Close();
            Process.Start(filename);
        }

        public void GenerarDocumento(Document document, DataGridView Table)
        {
            //se crea un objeto PdfTable con el numero de columnas del 
            //dataGridView
            PdfPTable datatable = new PdfPTable(Table.ColumnCount);
            //asignamos algunas propiedades para el diseño del pdf
            datatable.DefaultCell.Padding = 3;
            float[] headerwidths = TamañoColumnas(Table);
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 100;
            datatable.DefaultCell.BorderWidth = 2;
            datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //SE GENERA EL ENCABEZADO DE LA TABLA EN EL PDF
            for (int i = 0; i < Table.ColumnCount; i++)
            {
                datatable.AddCell(Table.Columns[i].HeaderText);
            }
            datatable.HeaderRows = 1;
            datatable.DefaultCell.BorderWidth = 1;
            //SE GENERA EL CUERPO DEL PDF
            for (int i = 0; i < Table.RowCount; i++)
            {
                for (int j = 0; j < Table.ColumnCount; j++)
                {
                    datatable.AddCell(Table[j, i].Value.ToString());
                }
                datatable.CompleteRow();
            }
            //SE AGREGAR LA PDFPTABLE AL DOCUMENTO
            document.Add(datatable);
        }


        public float[] TamañoColumnas(DataGridView dg)
        {
            float[] values = new float[dg.ColumnCount];
            for (int i = 0; i < dg.ColumnCount; i++)
            {
                values[i] = (float)dg.Columns[i].Width;
            }
            return values;
        }

        public System.Data.DataTable Buscar(string Tabla, string Nombre)
        {

            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("busca_" + Tabla + " '" + Nombre + "'", a);
            SqlDataReader cons = consultar.ExecuteReader();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Load(cons);
            a.Close();
            return dt;
        }
        public System.Data.DataTable Consultas(string nom_consulta)
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select * from Consulta_" + nom_consulta, a);
            SqlDataReader Consu = consultar.ExecuteReader();
            System.Data.DataTable Tabla = new System.Data.DataTable();
            Tabla.Load(Consu);
            a.Close();
            return Tabla;
        }
        public string Fecha,  Nombre;
        public long Ingresos, Gastos, Capital, Total;
        public void Obtener_datos_tran(string Fechat)
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select Capital_total, Fecha_trans from Transaccion where Fecha_trans =" + Fechat, a);
            SqlDataReader Consu = consultar.ExecuteReader();
            if (Consu.Read())
            {
                Reportes repo = new Reportes();
                repo.Capital = Consu.GetInt64(0);
                repo.Fecha = Consu.GetString(1);
                Capital = repo.Capital;
                Fecha = repo.Fecha;
            }
            a.Close();
        }
        public void Obtener_datos_ven(string Fechat)
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select Fecha_venta, Valor_total from Venta where Fecha_venta =" + Fechat, a);
            SqlDataReader Consu = consultar.ExecuteReader();
            if (Consu.Read())
            {
                Reportes repo = new Reportes();
                repo.Fecha = Consu.GetString(0);
                repo.Total = Consu.GetInt64(1);
                Fecha = repo.Fecha;
                Total = repo.Total;
            }
            a.Close();
        }
        public void Obtener_datos_cli(string Fechat, string ID)
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select Cliente.Nombre_cliente, Venta.Fecha_venta, Venta.Valor_total from Cliente inner join Venta on Cliente.Numero_identificacion = Venta.Numero_identificacion where Venta.Fecha_venta ='" + Fechat + "' and Cliente.Numero_identificacion =" + ID, a);
            SqlDataReader Consu = consultar.ExecuteReader();
            if (Consu.Read())
            {
                Reportes repo = new Reportes();
                repo.Nombre = Consu.GetString(0);
                repo.Fecha = Consu.GetString(1);
                repo.Total = Consu.GetInt64(2);
                Fecha = repo.Fecha;
                Total = repo.Total;
                Nombre = repo.Nombre;
            }
            a.Close();
        }
    }
}
