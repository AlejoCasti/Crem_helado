using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;
using Lógica;

namespace Vista
{
    public partial class Graficas : Form
    {
        public Graficas()
        {
            InitializeComponent();
        }
        public string user, cargo;
        Reportes repo = new Reportes();
        Cliente cli = new Cliente();
        string [] Fechas = new string [100];
        long[] totales = new long[100];
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Cliente")
            {
                int v = 0;
                for (int i = dateTimePicker1.Value.Day  ; i <= dateTimePicker2.Value.Day; i++)
                {
                    cli.Nombre = comboBox2.Text;
                    repo.Obtener_datos_cli(dateTimePicker1.Value.AddDays(v).ToShortDateString(), cli.obtener_codigo().ToString());
                    totales[i] = repo.Total;
                        Fechas[i] = repo.Fecha;
                        if (Fechas[i] != null)
                        {
  //                          Series ser = chart1.Series.Add(Fechas[i]);
    //                        ser.Points.Add(totales[i]);
                        }
                        else
                        {
      //                      Series ser = chart1.Series.Add("Ninguno" + i);
        //                    ser.Points.Add(0);
                        }
                        v++;
                }
            }
        }
        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Menu home = new Menu();
            home.Nombre_usu = user;
            home.Cargo_usuario = cargo;
            home.Show();
            this.Close();
        }

        private void Graficas_Load(object sender, EventArgs e)
        {
            comboBox2.DataSource = cli.fkcliente();
            comboBox2.ValueMember = "Nombre";
        }

      
    }
}
