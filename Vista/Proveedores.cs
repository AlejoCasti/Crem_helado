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
    public partial class Proveedores : Form
    {
        public Proveedores()
        {
            InitializeComponent();
        }
        Validaciones vali = new Validaciones();
        Proveedor prove = new Proveedor();
        public string usuario, cargo;
        private void Proveedores_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.pictureBox3, "Por favor revisa que tu correo tenga un @ y la terminacion .com o .es");
            
                panel2.Enabled = false;
                panel2.BorderStyle = BorderStyle.FixedSingle;
                DataTable ne = prove.ConsultarProveedor();
                dataGridView1.DataSource = ne;
                dataGridView1.ClearSelection();
                button1.Visible = false;
            
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                panel2.Enabled = true;
                panel2.BorderStyle = BorderStyle.Fixed3D;
                button1.Visible = true;
                button1.Text = "Registrar proveedor";
                textBox1.Enabled = true;
            }
        }

     
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                panel2.Enabled = false;
                panel2.BorderStyle = BorderStyle.FixedSingle;
                DataTable ne = prove.ConsultarProveedor();
                dataGridView1.DataSource = ne;
                dataGridView1.ClearSelection();
                button1.Visible = false;
                MessageBox.Show("Seleccione el proveedor que desea actualizar");
            }
        }

        public void limpiar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox1.Enabled = false;
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                panel2.Enabled = true;
                panel2.BorderStyle = BorderStyle.Fixed3D;
                button1.Visible = true;
                button1.Text = "Actualizar proveedor";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" | textBox2.Text == "" | textBox3.Text == "" | textBox4.Text == "" )
            {
                MessageBox.Show("Por favor ingresa todos los datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                prove.Nit = long.Parse(textBox1.Text);
                prove.Nombre = textBox2.Text;
                prove.Direccion = textBox3.Text;
                prove.telefono = long.Parse(textBox4.Text);
                prove.Correo = textBox5.Text;
                   if (radioButton2.Checked == true)
                    {
                        MessageBox.Show(prove.ingresar_proveedor());
                        DataTable ne = prove.ConsultarProveedor();
                        dataGridView1.DataSource = ne;
                        dataGridView1.ClearSelection();
                    }
                    else if (radioButton3.Checked == true)
                    {
                        prove.ActualizarProveedor();
                        DataTable ne = prove.ConsultarProveedor();
                        dataGridView1.DataSource = ne;
                        dataGridView1.ClearSelection();
                    }
                    limpiar();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Números(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Nombres(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Números(e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Pass(e);
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            prove.Correo = textBox5.Text;
            if (textBox2.Text != "")
            {
                if (vali.ValidarEmail(prove.Correo) == false)
                {
                    textBox5.Focus();
                    textBox5.ForeColor = Color.Red;
                    pictureBox3.Visible = true;
                }
                else
                {
                    textBox5.ForeColor = Color.Green;
                    pictureBox3.Visible = false;
                }
            }
        }
        Reportes Repo = new Reportes();
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text != "")
            {
                dataGridView1.DataSource = Repo.Buscar("Proveedor", textBox9.Text);
                dataGridView1.ClearSelection();
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.pictureBox3, "Por favor revisa que tu correo tenga un @ y la terminacion .com o .es");
            
        }
       
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Menu men = new Menu();
            men.Nombre_usu = usuario;
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
    }
}  
