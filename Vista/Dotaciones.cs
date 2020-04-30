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
    public partial class Dotaciones : Form
    {
        public Dotaciones()
        {
            InitializeComponent();
        }
        Cliente cli = new Cliente();
        Dotacion dota = new Dotacion();
        public string Nombre_usu, Cargo_usu,Nombre_cli;
        private void Dotacion_Load(object sender, EventArgs e)
        {
            comboBox2.DataSource = cli.fkcliente();
            comboBox2.ValueMember = "Nombre";
            comboBox1.DataSource = dota.Consultar_dotacion();
            comboBox1.ValueMember = "Nombre";
            if (comboBox1.Text == "" | comboBox2.Text == "")
            {
                panel2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                dota.Cantidad = int.Parse(numericUpDown1.Value.ToString());
                if (radioButton1.Checked == true)
                {
                    if (textBox1.Text == "" | numericUpDown1.Value == 0)
                    {
                        MessageBox.Show("Por favor ingrese todos los valores, recuerde que debe tener una existencia");
                    }
                    else
                    {
                        dota.Nombre = textBox1.Text;
                        dota.Cantidadlibre = dota.Cantidad;
                        dota.Registrar_dotacion();
                        comboBox1.DataSource = dota.Consultar_dotacion();
                        comboBox1.ValueMember = "Nombre";
                        MessageBox.Show("Se ha registrado exitosamente");
                        if (comboBox1.Text == "" | comboBox2.Text == "")
                        {
                            panel2.Enabled = false;
                        }
                        else
                        {
                            panel2.Enabled = true;
                        }
                    }
                }
                else
                {
                    if (comboBox1.Text == "" | numericUpDown1.Value == 0)
                    {
                        MessageBox.Show("Por favor ingrese todos los valores, recuerde que debe tener una existencia");
                    }
                    else
                    {
                        dota.Nombre = comboBox3.Text;
                        dota.Cantidad = int.Parse(numericUpDown1.Value.ToString());
                        dota.Actualizar_dotacion();
                        radioButton2.Checked = false;
                        MessageBox.Show("Se ha actualizado exitosamente");

                    }
                }
                radioButton1.Checked = false;
                panel1.Enabled = false;
                textBox1.Clear();
                numericUpDown1.Value = 0;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dota.cli.Nombre = comboBox2.Text;
            dota.cli.ID = dota.cli.obtener_codigo();
            if (dota.Consultar_3dotacion() == false)
            {
                if (textBox2.Text != "" | textBox3.Text != "" | textBox4.Text != "")
                {
                    dota.Nombre = comboBox1.Text;
                    if (dota.obtener_exitencia() == 0)
                    {
                        DialogResult mensaje = MessageBox.Show("No hay " + dota.Nombre + " libre, ¿Deseas actualizar la dotacion?", "Dotacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (mensaje == DialogResult.Yes)
                        {
                            panel2.Enabled = false;
                            textBox1.Focus();
                        }
                    }
                    else
                    {
                        dota.Nombre = comboBox1.Text;
                        dota.Nombrefi = textBox3.Text;
                        dota.ID = long.Parse(textBox2.Text);
                        dota.Telefono = long.Parse(textBox4.Text);
                        dota.Registrar_3dotacion();
                        MessageBox.Show("Se ha registrado exitosamente");
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Por favor llene todos los datos correspondientes");
                }

            }
            else
            {
                MessageBox.Show("Usted ya asigno esta dotacion a este cliente");
            }
        }
            
        Menu men = new Menu();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            men.Nombre_usu = Nombre_usu;
            men.Cargo_usuario = Cargo_usu;
            if (men.Cargo_usuario == "Empleado")
            {
                men.pictureBox2.Enabled = false;
                men.pictureBox3.Enabled = false;
                men.pictureBox4.Enabled = false;
                men.pictureBox7.Enabled = false;
                men.pictureBox9.Enabled = false;
                men.ventasToolStripMenuItem2.Enabled = false;
            }
            men.Nombre_usu = Nombre_usu;
            men.label2.Text = Cargo_usu;
            men.Show();
            this.Close();
        }
        Validaciones vali = new Validaciones();
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Números(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Nombres(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Números(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Nombres(e);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
                button3.Text = "Registrar";
                panel1.Enabled = true;
                comboBox3.Visible = false;   
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                comboBox3.DataSource = dota.Consultar_dotacion();
                if (comboBox3.Text == "")
                {
                    radioButton2.Checked = false;
                    radioButton1.Checked = true;
                    MessageBox.Show("Por favor registre dotaciones");
                    textBox1.Focus();
                }
                else
                {
                    button3.Text = "Actualizar";
                    panel1.Enabled = true;
                    comboBox3.Visible = true;
                }
            }
            else
            {
                comboBox3.Visible = false;
            }
            
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            dota.Nombre = comboBox3.Text;
            numericUpDown1.Value = dota.obtener_exitencia();
        }



    }
}
