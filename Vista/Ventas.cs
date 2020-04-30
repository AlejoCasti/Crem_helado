using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Lógica;

namespace Vista
{
    public partial class Ventas : Form
    {
        public Ventas()
        {
            InitializeComponent();
        }

        Menu menu = new Menu();
        Venta ven = new Venta();
        Cliente cli = new Cliente();
        Producto pro = new Producto();
        Reportesvi repo = new Reportesvi();
        Transaccion tran = new Transaccion();
        public string User, cargo;
        
        private void Ventas_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.DataSource = cli.fkcliente();
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "ID";
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = pro.cargar_productoV();
            dataGridView1.Columns[0].DataPropertyName = "Nombre";
            dataGridView2.DataSource = ven.Consulta_venta();
            DateTime fecha = DateTime.Now;
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker1.Value = DateTime.Today;
            int b = 0;
            while (b < dataGridView1.RowCount)
            {
                    dataGridView1.Rows[b].Cells[1].Value = "0";
                    dataGridView1.Rows[b].Cells[2].Value = "0";
                    dataGridView1.Rows[b].Cells[3].Value = "0";
                    b = b + 1;
            }
            if (comboBox2.Text == "" | dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Debes registrar antes un cliente y/o producto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                menu.Nombre_usu = User;
                menu.Cargo_usuario = cargo;
                menu.Show();
                this.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                ven.cli.Nombre = comboBox2.Text;
                ven.Fecha = dateTimePicker1.Value.ToShortDateString();
                ven.cli.ID = ven.cli.obtener_codigo();
                ven.usu.user = User;
                ven.Tipo = comboBox1.Text;
                int b = 0;
                ven.RegistrarVenta();
                long total = 0, Subtotal = 0, Totaltran = 0, Subtotaltran = 0;
                int contadora = 0;
                long diferencia = 0;
                while (b < dataGridView1.RowCount)
                {
                    ven.Codigo = ven.obtener_ultcodigo();
                    ven.pro.Nombre = dataGridView1.Rows[b].Cells[0].Value.ToString();
                    ven.pro.Codigo = ven.pro.obtener_codigo().ToString();
                    ven.Entrega = long.Parse(dataGridView1.Rows[b].Cells[1].Value.ToString());
                    ven.Recibida = long.Parse(dataGridView1.Rows[b].Cells[2].Value.ToString());
                    ven.Recarga = long.Parse(dataGridView1.Rows[b].Cells[3].Value.ToString());
                    Subtotal = ven.pro.obtener_PrecioV() * (ven.Entrega - ven.Recibida + ven.Recarga);
                    total = total + Subtotal;
                    ven.Total = total;
                    ven.Registrar_3venta();
                    diferencia = (ven.Entrega - ven.Recibida + ven.Recarga);
                    Subtotaltran = (ven.pro.obtener_PrecioP() - ven.pro.obtener_PrecioV())* diferencia;
                    Totaltran = Subtotaltran + Totaltran;
                    if (contadora < dataGridView1.RowCount)
                    {
                        ven.pro.Nombre = dataGridView1.Rows[contadora].Cells[0].Value.ToString();
                        ven.pro.Existencia = ven.pro.obtener_existencia() - diferencia;
                        ven.pro.Actualizar_existencia();
                        contadora = contadora + 1;

                    }
                    b = b + 1;
                }
                ven.Codigo = ven.obtener_ultcodigo();
                ven.ActualizarVenta();
                dataGridView2.DataSource = ven.Consulta_venta();
                int aab = 0;
                while (aab < dataGridView1.RowCount)
                {
                    dataGridView1.Rows[aab].Cells[1].Value = "0";
                    dataGridView1.Rows[aab].Cells[2].Value = "0";
                    dataGridView1.Rows[aab].Cells[3].Value = "0";
                    aab = aab + 1;

                }
                tran.ven.Codigo = ven.obtener_ultcodigo();
                tran.Fecha = ven.Fecha;
                tran.Gastos = 0;
                tran.Ingresos = Totaltran;
                tran.Codigo = tran.obtener_ultcodigo();
                tran.Capital = tran.obtener_Capital()+tran.Ingresos;
                tran.Registrar_transven();
        }

        Validaciones vali = new Validaciones();
       
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
            
                if (e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar))
                    e.Handled = false;
                else
                    e.Handled = true;
            
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }


        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex != 0)
            {
                ven.Entrega = long.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                ven.Recibida = long.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                ven.Recarga = long.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());
                ven.pro.Nombre = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                long hhh = ven.pro.obtener_existencia() - ven.Entrega + ven.Recibida - ven.Recarga;
                if (hhh < 0)
                {
                    dataGridView1.CurrentRow.Cells[1].Value = ven.pro.obtener_existencia().ToString();
                }
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            menu.Nombre_usu = User;
            menu.Cargo_usuario = cargo;
            if (menu.Cargo_usuario == "Empleado")
            {
                menu.pictureBox2.Enabled = false;
                menu.pictureBox3.Enabled = false;
                menu.pictureBox4.Enabled = false;
                menu.pictureBox7.Enabled = false;
                menu.pictureBox9.Enabled = false;
                menu.ventasToolStripMenuItem2.Enabled = false;
            }
            menu.Show();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Actualizar_venta vent = new Actualizar_venta();
            vent.ShowDialog();
        }

        private void dataGridView1_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex != 0)
            {
                if (dataGridView1.CurrentCell.Value == null)
                {
                    dataGridView1.CurrentCell.Value = "0";
                }
                ven.pro.Nombre = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                ven.pro.Existencia = ven.pro.obtener_existencia();
                long sda = long.Parse(dataGridView1.CurrentCell.Value.ToString());
                if (sda > ven.pro.Existencia)
                {
                    dataGridView1.CurrentCell.Value = ven.pro.Existencia - 50;
                }
            }
        }
    }
}
