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
    public partial class Maquinarias : Form
    {
        public Maquinarias()
        {
            InitializeComponent();
        }
        public string user, cargo;
        Maquinaria maq = new Maquinaria();
        void Limpiar()
        {
            textBox2.Clear();
            textBox4.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void Maquinarias_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.Today;
            dateTimePicker1.Value = DateTime.Now;
            panel2.Enabled = false;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            DataTable ne = maq.ConsultarMaquinaria();
            dataGridView1.DataSource = ne;
            dataGridView1.ClearSelection();
            button1.Visible = false;
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                Limpiar();
                panel2.Enabled = true;
                panel2.BorderStyle = BorderStyle.Fixed3D;
                button1.Visible = true;
                button1.Text = "Registrar maquinaria";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                Limpiar();
                panel2.Enabled = false;
                panel2.BorderStyle = BorderStyle.FixedSingle;
                DataTable ne = maq.ConsultarMaquinaria();
                dataGridView1.DataSource = ne;
                dataGridView1.ClearSelection();
                button1.Visible = false;
                MessageBox.Show("Seleccione la maquinaria que desea actualizar");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" | textBox4.Text == "" )
            {
                MessageBox.Show("Por favor ingresa todos los datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                maq.Estado = "Activo";
                maq.Fecha = dateTimePicker1.Value.ToShortDateString();
                maq.Tipo = textBox4.Text;
                maq.Nombre = textBox2.Text;
                maq.usu.user = user;
                maq.Codigo = maq.obtener_codigo();
                if (radioButton2.Checked == true)
                {
                    if (maq.Registrar_maquinaria())
                    {
                        MessageBox.Show("La maquinaria ha sido registrada correctamente");
                        DataTable ne = maq.ConsultarMaquinaria();
                        dataGridView1.DataSource = ne;
                        dataGridView1.ClearSelection();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("La maquinaria ha sido registrada anteriormente");
                    }
                }
                else if (radioButton3.Checked == true)
                {
                    maq.Actualizar_maquinaria();
                    DataTable ne = maq.ConsultarMaquinaria();
                    dataGridView1.DataSource = ne;
                    dataGridView1.ClearSelection();
                    Limpiar();
                }
            }     
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Menu men = new Menu();
            men.Nombre_usu = user;
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
        Validaciones val = new Validaciones();
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Nombres(e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Nombres(e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                panel2.Enabled = true;
                panel2.BorderStyle = BorderStyle.Fixed3D;
                button1.Text = "Actualizar maquinaria";
                button1.Visible = true;
            }
        }
        Reportes Repo = new Reportes();
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text != "")
            {
                dataGridView1.DataSource = Repo.Buscar("Maquinaria", textBox9.Text);
                dataGridView1.ClearSelection();
            }
        }
    }
}
