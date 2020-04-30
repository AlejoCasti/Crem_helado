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
    public partial class Transacciones : Form
    {
        public Transacciones()
        {
            InitializeComponent();
        }

        int total;
        Transaccion tran = new Transaccion();
        private void Transacciones_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
                DialogResult dia = MessageBox.Show("¿Esta seguro que desea ejecutar esta transacción?", "Transacción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dia == DialogResult.Yes)
                {
                    tran.Codigo = tran.obtener_ultcodigo();
                    tran.Capital = total + tran.obtener_Capital();
                    tran.Ingresos = long.Parse(textBox3.Text);
                    tran.Registrar_trans();
                    this.Close();
                }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tran.Fecha = DateTime.Today.ToShortDateString();
            tran.Descripcion = textBox1.Text;
            tran.Codigo = tran.obtener_ultcodigo();
            tran.Capital = tran.obtener_Capital();
            tran.Ingresos = long.Parse(textBox2.Text);
            tran.Registrar_trans();
            dataGridView1.Rows.Add(textBox1.Text, textBox2.Text,comboBox1.Text);
            if (comboBox1.Text == "Gasto")
            {
                total = total - int.Parse(textBox2.Text);
                textBox3.Text = Math.Abs(total).ToString();
            }
            else
            {
                total = total + int.Parse(textBox2.Text);
                textBox3.Text = Math.Abs(total).ToString();
            }
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
