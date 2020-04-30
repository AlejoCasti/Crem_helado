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
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }
        //Objetos
        Cliente cliente = new Cliente();
        Validaciones vali = new Validaciones();
        Reportes Repo = new Reportes();
        public string Cargoa,Nombre;
        public void Limpiar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" & textBox3.Text != "" & textBox4.Text != "" & textBox5.Text != "" )
            {
                cliente.ID = long.Parse(textBox1.Text);
                cliente.Nombre = textBox3.Text;
                cliente.Correo = textBox2.Text;
                cliente.Telefono = long.Parse(textBox4.Text);
                cliente.Direccion = textBox5.Text;
                if (radioButton2.Checked == true)
                {
                        string a = cliente.Registro_cli();
                        DataTable Consultar = cliente.Consulta_cliente();
                        dataGridView1.DataSource = Consultar;
                        dataGridView1.ClearSelection();
                        if (dataGridView1.CurrentRow.Cells[0].Value.ToString() == null)
                        {
                            radioButton3.Enabled = true;
                        }
                        MessageBox.Show(a);
                        DialogResult Resul = MessageBox.Show("¿Desea asignar dotacion al cliente?","Dotaicion",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                        if (Resul == DialogResult.Yes)
                        {
                            Dotaciones dot = new Dotaciones();
                            dot.Nombre_usu = Nombre;
                            dot.Cargo_usu = Cargoa;
                            dot.Nombre_cli = cliente.Nombre;
                            dot.comboBox2.Text = dot.Nombre_cli;
                            dot.comboBox2.Enabled = false;
                            dot.Show();
                            this.Close();
                        }
                        else
                        {
                            Limpiar();
                        }

                }
                else
                {
                    if (radioButton3.Checked == true)
                    {
                        if (textBox2.ForeColor == Color.Red)
                        {
                            MessageBox.Show("Verifica tu correo");
                            textBox2.Focus();
                        }
                        else
                        {
                            textBox1.Enabled = false;
                            cliente.Actualizacion_cliente();
                            DataTable Consultar = cliente.Consulta_cliente();
                            dataGridView1.DataSource = Consultar;
                            dataGridView1.ClearSelection();
                            Limpiar();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese los datos correspondientes");
            }
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.pictureBox3, "Por favor revisa que tu correo tenga un @ y la terminacion .com o .es");
            dataGridView1.DataSource = cliente.Consulta_cliente();
            dataGridView1.ClearSelection();
            
                panel1.Enabled = false;
                button3.Visible = false;
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Limpiar();
            if (radioButton2.Checked == true)
            {
                button3.Text = "Registrar";
                textBox1.Enabled = true;
                panel1.Enabled = true;
                button3.Visible = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Limpiar();
            if (radioButton3.Checked == true)
            {
                button3.Text = "Actualizar";
                MessageBox.Show("Seleccione el cliente en la tabla");
                panel1.Enabled = false;
                button3.Visible = false;
                textBox1.Enabled = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                panel1.Enabled = true;
                button3.Visible = true;
            }
        }

        private void Caja_Usu_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                DataTable Consultar = Repo.Buscar("Cliente", textBox6.Text);
                dataGridView1.DataSource = Consultar;
                dataGridView1.ClearSelection();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Limpiar();
           
                panel1.Enabled = false;
                button3.Visible = false;
            
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.pictureBox3, "Por favor revisa que tu correo tenga un @ y la terminacion .com o .es");
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            cliente.Correo = textBox2.Text;
            if (textBox2.Text != "")
            {
                if (vali.ValidarEmail(cliente.Correo) == false)
                {
                    textBox2.Focus();
                    textBox2.ForeColor = Color.Red;
                    pictureBox3.Visible = true;
                }
                else
                {
                    textBox2.ForeColor = Color.Green;
                    pictureBox3.Visible = false;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Menu Home = new Menu();
            Home.Cargo_usuario = Cargoa;
            if (Cargoa == "Empleado")
            {
                Home.pictureBox2.Enabled = false;
                Home.pictureBox3.Enabled = false;
                Home.pictureBox4.Enabled = false;
                Home.pictureBox7.Enabled = false;
                Home.pictureBox9.Enabled = false;
                Home.ventasToolStripMenuItem2.Enabled = false;
            }
            Home.Nombre_usu = Nombre;
            Home.label2.Text = Cargoa;
            Home.Show();
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Números(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Nombres(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Pass(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Números(e);
        }

    }
}
