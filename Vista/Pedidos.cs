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
    public partial class Pedidos : Form
    {
        public Pedidos()
        {
            InitializeComponent();
        }
        Producto pro = new Producto();
        Pedido ped = new Pedido();
        public string User, cargo;
        private void Pedidos_Load(object sender, EventArgs e)
        {
            comboBox2.DataSource = ped.prove.fkproveedor();
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "Nit";
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.MinDate = DateTime.Today;
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
            if (comboBox2.Text == "")
            {
                MessageBox.Show("Debes registrar primero un proveedor");
                Proveedores pro = new Proveedores();
                pro.usuario = User;
                pro.cargo = cargo;
                pro.Show();
                this.Close();
            }
            else
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = ped.pro.cargar_productoP();
                dataGridView1.Columns[0].DataPropertyName = "Nombre";
                DateTime fecha = DateTime.Now;
                dateTimePicker1.MaxDate = DateTime.Now;
                dateTimePicker1.Value = DateTime.Today;
                int b = 0;
                while (b < dataGridView1.RowCount)
                {
                    dataGridView1.Rows[b].Cells[1].Value = "0";
                    b = b + 1;
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Menu men = new Menu();
            men.Nombre_usu = User;
            men.Cargo_usuario = cargo;
            if (men.Cargo_usuario == "Empleado")
            {
                men.pictureBox2.Enabled = false;
                men.pictureBox3.Enabled = false;
                men.pictureBox4.Enabled = false;
                men.pictureBox7.Enabled = false;
                men.pictureBox8.Enabled = false;
                men.pictureBox9.Enabled = false;
                men.ventasToolStripMenuItem2.Enabled = false;
            }
            men.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
                ped.prove.Nombre = comboBox2.Text;
                ped.prove.Nit = ped.prove.obtener_codigo();
                ped.Fecha = dateTimePicker1.Value.ToShortDateString();
                ped.entrega = dateTimePicker2.Value.ToShortDateString();
                ped.usu.user = User;
                int b = 0;
                ped.RegistrarPedido();
                long total = 0, Subtotal = 0;
                while (b < dataGridView1.RowCount)
                {
                    ped.Codigo = ped.obtener_ultcodigo();
                    ped.pro.Nombre = dataGridView1.Rows[b].Cells[0].Value.ToString();
                    ped.pro.Codigo = ped.pro.obtener_codigo().ToString();
                    ped.Cantidad = long.Parse(dataGridView1.Rows[b].Cells[1].Value.ToString());
                    if (ped.Cantidad != 0)
                    {
                        Subtotal = ped.pro.obtener_CostCaja() * ped.Cantidad;
                        total = total + Subtotal;
                        ped.Total = total;
                        ped.Registrar_3pedido();
                    }
                    b = b + 1;
                }
                ped.Codigo = ped.obtener_ultcodigo();
                ped.ActualizarPedido();
                dataGridView2.DataSource = ped.consultarPedido();
                dataGridView2.ClearSelection();
                int aab = 0;
                while (aab < dataGridView1.RowCount)
                {
                    dataGridView1.Rows[aab].Cells[1].Value = "0";
                    aab = aab + 1;
                }   
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
