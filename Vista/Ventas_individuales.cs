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
    public partial class Ventas_individuales : Form
    {
        public Ventas_individuales()
        {
            InitializeComponent();
        }
        Producto pro = new Producto();
        Menu menu = new Menu();
        private void Ventas_individuales_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = pro.cargar_productoV();
            comboBox1.DisplayMember = "Nombre";
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Debes registrar antes un producto para efectuar ventas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = 0;
            long total = 0, subtotal;
            while (a < dataGridView1.RowCount)
            {
                pro.Nombre = dataGridView1.Rows[a].Cells[0].Value.ToString();
                pro.Existencia = pro.obtener_existencia() - long.Parse(dataGridView1.Rows[a].Cells[1].Value.ToString());
                pro.Actualizar_existencia();
                pro.Codigo = pro.obtener_codigo().ToString();
                subtotal = pro.obtener_PrecioP() * long.Parse(dataGridView1.Rows[a].Cells[1].Value.ToString());
                total = total + subtotal;
                a = a + 1;

            }
            MessageBox.Show("El total de la venta es: $"+total);
            DialogResult ab = MessageBox.Show("¿Desea agregar algún producto más?", "salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ab == DialogResult.No)
            {
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(comboBox1.Text, numericUpDown1.Value.ToString());
  
        }
    }
}
