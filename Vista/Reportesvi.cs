using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lógica;

namespace Vista
{
    public partial class Reportesvi : Form
    {
        public Reportesvi()
        {
            InitializeComponent();
        }

        Reportes Repor = new Reportes();
        Venta ven = new Venta();
        Cliente cli = new Cliente();
        public string Nombre_usu, Cargo_usu;


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Menu home = new Menu();
            home.Nombre_usu = Nombre_usu;
            home.Cargo_usuario = Cargo_usu;
            if (home.Cargo_usuario == "Empleado")
            {
                home.pictureBox2.Enabled = false;
                home.pictureBox3.Enabled = false;
                home.pictureBox4.Enabled = false;
                home.pictureBox7.Enabled = false;
                home.pictureBox9.Enabled = false;
                home.ventasToolStripMenuItem2.Enabled = false;
            }
            home.Show();
            this.Close();
        }
        Reportes re = new Reportes();
        private void button3_Click(object sender, EventArgs e)
        {
            re.PDF_venta(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            re.exportar_excel(dataGridView1);
        }
        Transaccion tran = new Transaccion();
        public void retornar()
        {
            this.Width = 710;
            this.Height = 344;
            pictureBox2.Left = 12;
            pictureBox2.Top = 292;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "Venta")
            {
                if (comboBox1.Text == "Transaccion")
                {
                    this.Width = 710;
                    this.Height = 518;
                    pictureBox2.Left = 12;
                    pictureBox2.Top = 450;
                    dataGridView2.Visible = true;
                    dataGridView2.DataSource = tran.Consulta();
                    dataGridView1.DataSource = Repor.Consultas(comboBox1.Text);

                }
                else
                {

                    dataGridView1.DataSource = Repor.Consultas(comboBox1.Text);
                    dataGridView2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    Nombrecli.Visible = false;
                    Calendario.Visible = false;
                    dataGridView1.Width = 630;
                }
            }
            else
            {
                dataGridView2.Visible = false;
                label3.Visible = true;
                label4.Visible = true;
                Nombrecli.Visible = true;
                Calendario.Visible = true;
                dataGridView1.Width = 294;

            }
            
        }

        private void Reportesvi_Load(object sender, EventArgs e)
        {
            Nombrecli.DisplayMember = "Nombre";
            Nombrecli.DataSource = cli.fkcliente();
            Calendario.MaxDate = DateTime.Now;
            Calendario.Value = DateTime.Today;
        }

        private void Nombrecli_SelectedIndexChanged(object sender, EventArgs e)
        {
            ven.cli.Nombre = Nombrecli.Text;
            ven.Fecha = Calendario.Value.ToShortDateString();
            ven.cli.ID = ven.cli.obtener_codigo();
            ven.Codigo = ven.obtener_codigo();
            dataGridView1.DataSource = ven.Busca_3venta();
        }

    }
}
