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
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }
        
        Empleado Usu = new Empleado();
        Validaciones vali = new Validaciones();
        Reportes Repo = new Reportes();
        public string Cargo_usu, Nombre ;
        public void limpiar()
        {
            Caja_Usu.Clear();
            Caja_Empleado.Clear();
            Caja_ID.Clear();
            Combo_cargo.SelectedIndex = -1;
            Combo_Estado.SelectedIndex = -1;
            Caja_correo.Clear();
            Caja_telefono.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                if (Caja_Usu.Text == "" | Caja_Empleado.Text == "" | Caja_ID.Text == "" | Combo_cargo.Text == "")
                {  
                    MessageBox.Show("Por favor ingrese todos los datos");
                }
                else
                {
                    Usu.ID = long.Parse(Caja_ID.Text);
                    Usu.Cargo = Combo_cargo.Text;
                    Usu.Correo = Caja_correo.Text;
                    Usu.Estado = Combo_Estado.Text;
                    Usu.Nombre = Caja_Empleado.Text;
                    Usu.Telefono = long.Parse(Caja_telefono.Text);
                    Usu.user = Caja_Usu.Text;
                        Menu Home = new Menu();
                        Usu.Registro_usu();
                        if (Caja_correo.ForeColor == Color.Red)
                        {
                            MessageBox.Show("Verifica tu correo");
                            Caja_correo.Focus();
                        }
                        else
                        {
                            if (Cargo_usu == "Empleado")
                            {
                                dataGridView1.DataSource = Usu.Consulta_usuarios();
                                limpiar();
                            }
                            else
                            {
                                DataTable Consulta = Usu.Consulta_usuarios();
                                dataGridView1.DataSource = Consulta;
                                radioButton2.Checked = false;
                                limpiar();
                            }
                        }
                   
                }
            }
            else
            {
                if (radioButton3.Checked == true)
                {
                    if (Caja_Usu.Text == "" | Caja_ID.Text == "" | Caja_correo.Text == "" | Combo_cargo.Text == "" | Combo_Estado.Text == "")
                    {
                        MessageBox.Show("Por favor ingrese todos los datos");
                    }
                    else
                    {
                        Usu.ID = long.Parse(Caja_ID.Text);
                        Usu.Cargo = Combo_cargo.Text;
                        Usu.Correo = Caja_correo.Text;
                        Usu.Estado = Combo_Estado.Text;
                        Usu.Telefono = long.Parse(Caja_telefono.Text);
                        Usu.user = Caja_Usu.Text;
                        Usu.Nombre = Caja_Empleado.Text;
                        Usu.Actualizacion_usuario2();
                        DataTable Administrador = Usu.Consulta_usuarios();
                        dataGridView1.DataSource = Administrador;
                        limpiar();
                    }
                }
                else
                {
                    
                }
            }  
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.pictureBox3, "Por favor revisa que tu correo tenga un @ y la terminacion .com o .es");
            if (Cargo_usu != "Empleado")
            {
                dataGridView1.DataSource = Usu.Consulta_usuarios();
            }
            else
            {
                dataGridView1.DataSource = Usu.Consulta_usuarios();
                dataGridView1.Width = 541;

            }
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Menu Home = new Menu();
            Home.Cargo_usuario = Cargo_usu;
            Home.Nombre_usu = Nombre;
            if (Cargo_usu == "Empleado")
            {
                Home.pictureBox2.Enabled = false;
                Home.pictureBox3.Enabled = false;
                Home.pictureBox4.Enabled = false;
                Home.pictureBox7.Enabled = false;
                Home.pictureBox8.Enabled = false;
                Home.pictureBox9.Enabled = false;
                Home.ventasToolStripMenuItem2.Enabled = false;
            }
            Home.label2.Text = Cargo_usu ;
            Home.Show();
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (radioButton3.Checked==true)
            {
            Caja_Usu.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Caja_Empleado.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Caja_ID.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Caja_telefono.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            Caja_correo.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            Combo_Estado.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            Combo_cargo.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Cuentas(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Pass(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Pass(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Nombres(e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Números(e);
        }

        private void Caja_correo_Leave(object sender, EventArgs e)
        {
            Usu.Correo = Caja_correo.Text;
            if (vali.ValidarEmail(Usu.Correo) == false)
            {
                Caja_correo.Focus();
                Caja_correo.ForeColor = Color.Red;
                pictureBox3.Visible = true;
            }
            else
            {
                Caja_correo.ForeColor = Color.Green;
                pictureBox3.Visible = false;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.pictureBox3, "Por favor revisa que tu correo tenga un @ y la terminacion .com o .es");   
        }

        private void Caja_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Números(e);
        }

        private void Caja_telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.Números(e);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text != "")
            {
                dataGridView1.DataSource = Repo.Buscar("Empleado", textBox9.Text);
                dataGridView1.ClearSelection();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            limpiar();
            if (radioButton2.Checked == true)
            {

                Caja_Usu.Visible = true;
                label2.Visible = true;
                panel1.Enabled = true;
                button1.Text = "Registrar usuario";
                Combo_Estado.Enabled = false;
                label9.Visible = false;
                button1.Visible = true;

                if (Cargo_usu == "Empleado")
                {
                    Combo_cargo.Items.Clear();
                    Combo_cargo.Items.Add("Empleado");
                }


            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            limpiar();
            if (radioButton3.Checked == true)
            {
                Caja_Usu.Visible = true;
                Caja_Usu.Enabled = false;
                label2.Visible = true;
                label5.Text = "Estado";
                label9.Visible = false;
                panel1.Enabled = true;
                button1.Visible = true;
                Combo_Estado.Enabled = true;
                label5.Text = "Estado";
                button1.Visible = true;
                button1.Text = "Actualizar usuario";
                MessageBox.Show("Seleccione el nombre de la cuenta que desea actualizar");
            }
        }

    }
}
