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
    public partial class Actualizar_venta : Form
    {
        public Actualizar_venta()
        {
            InitializeComponent();
        }
        public string Nombre, cargo;
        Venta ven = new Venta();
        Cliente cli = new Cliente();
        Transaccion tran = new Transaccion();
        private void Actualizar_venta_Load(object sender, EventArgs e)
        {
            comboBox2.DataSource = cli.fkcliente();
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "ID";
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker1.Value = DateTime.Today;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int b = 0, contadora = 0;
            long diferencia = 0, Subtotaltran, Total = 0;
            while (b < dataGridView1.RowCount)
            {
                long total = 0, Subtotal = 0;
                ven.pro.Nombre = dataGridView1.Rows[b].Cells[0].Value.ToString();
                ven.pro.Codigo = ven.pro.obtener_codigo().ToString();
                ven.Entrega = long.Parse(dataGridView1.Rows[b].Cells[1].Value.ToString());
                ven.Recibida = long.Parse(dataGridView1.Rows[b].Cells[2].Value.ToString());
                ven.Recarga = long.Parse(dataGridView1.Rows[b].Cells[3].Value.ToString());
                Subtotal = ven.pro.obtener_PrecioV() * (ven.Entrega - ven.Recibida + ven.Recarga);
                total = total + Subtotal;
                ven.Total = total;
                ven.cli.Nombre = comboBox2.Text;
                ven.cli.ID = ven.cli.obtener_codigo();
                ven.Fecha = dateTimePicker1.Value.ToShortDateString();
                ven.Codigo = ven.obtener_codigo();
                ven.Actualizar_3venta();
                diferencia = (ven.Recibida - ven.Recarga);
                Subtotaltran = (ven.pro.obtener_PrecioP() - ven.pro.obtener_PrecioV()) * diferencia;
                Total = Subtotaltran + total;
                if (contadora < dataGridView1.RowCount)
                {
                    ven.pro.Nombre = dataGridView1.Rows[contadora].Cells[0].Value.ToString();
                    ven.pro.Existencia = ven.pro.obtener_existencia() + diferencia;
                    ven.pro.Actualizar_existencia();
                    contadora = contadora + 1;
                }
                textBox1.Text = "" + total+","+ven.Codigo;
                b = b + 1;
            }
            ven.ActualizarVenta();
            tran.Codigo = tran.obtener_ultcodigo2();
            tran.Capital = tran.obtener_Capital() + Total;
            tran.Codigo = tran.obtener_Codigo();
            tran.Ingresos = Total;
            tran.Actualizar_transVen();
            DialogResult a = MessageBox.Show("¿Desea actualizar algo mas?","salir",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (a == DialogResult.No)
            {
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Ventas v = new Ventas();
            v.button1.Enabled = true;
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ven.cli.Nombre = comboBox2.Text;
            ven.Fecha = dateTimePicker1.Value.ToShortDateString();
            ven.cli.ID = ven.cli.obtener_codigo();
            ven.Codigo = ven.obtener_codigo();
            dataGridView1.DataSource = ven.Busca_3venta();
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
