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
    public partial class Actualizar_pedido : Form
    {
        public Actualizar_pedido()
        {
            InitializeComponent();
        }
        public string Nombre, cargo;
        public Pedido ped = new Pedido();
        Proveedor prove = new Proveedor();

        private void Actualizar_pedido_Load(object sender, EventArgs e)
        {
            comboBox2.DataSource = ped.prove.fkproveedor();
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "Nit";
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            ped.prove.Nombre = comboBox2.Text;
            ped.prove.Nit = ped.prove.obtener_codigo();
            ped.entrega = dateTimePicker2.Value.ToShortDateString();
            ped.Codigo = ped.obtener_pedido();
            dataGridView1.DataSource = ped.Busca_3pedido();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex != 0)
            {
                TextBox txt = e.Control as TextBox;
                if (txt != null)
                {
                    txt.KeyPress -= new KeyPressEventHandler(dataGridView1_KeyPress);
                    txt.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress);
                }
            } 
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex != 0)
            {
                if (e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public int b = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            ped.prove.Nombre = comboBox2.Text;
            ped.prove.Nit = ped.prove.obtener_codigo();
            ped.Fecha = dateTimePicker1.Value.ToShortDateString();
            ped.entrega = dateTimePicker2.Value.ToShortDateString();
            ped.usu.user = Nombre;
            
            long total = 0, Subtotal = 0;
            while (b < dataGridView1.RowCount)
            {
                ped.Codigo = ped.obtener_pedido();
                ped.pro.Nombre = dataGridView1.Rows[b].Cells[0].Value.ToString();
                ped.pro.Codigo = ped.pro.obtener_codigo().ToString();
                ped.Cantidad = long.Parse(dataGridView1.Rows[b].Cells[1].Value.ToString());
                if (ped.Cantidad != 0)
                {
                    Subtotal = ped.pro.obtener_CostCaja() * (ped.Cantidad);
                    total = total + Subtotal;
                    ped.Total = total;
                    ped.Actualizar_3pedido();
                    if (b < dataGridView1.RowCount)
                    {
                        ped.pro.Nombre = dataGridView1.Rows[b].Cells[0].Value.ToString();
                        ped.pro.Existencia = ped.pro.obtener_existencia() + (ped.Cantidad*ped.pro.obtener_CantCaja());
                        ped.pro.Actualizar_existencia();
                    }
                }
                b = b + 1;
            }
            ped.ActualizarPedido();
            textBox1.Text = ""+ped.Total;
            int aab = 0;
            while (aab < dataGridView1.RowCount)
            {
                dataGridView1.Rows[aab].Cells[1].Value = "0";
                aab = aab + 1;
            }  
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex != 0)
            {
                if (dataGridView1.CurrentCell.Value == null)
                {
                    dataGridView1.CurrentCell.Value = "0";
                }
            }
        }
    }
}
