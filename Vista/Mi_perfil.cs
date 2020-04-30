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
    public partial class Mi_perfil : Form
    {
        public Mi_perfil()
        {
            InitializeComponent();
        }

        public string user;
        Empleado em = new Empleado();

        private void Mi_perfil_Load(object sender, EventArgs e)
        {
            em.Nombre = user;
            em.cargar_empleado();
            textBox1.Text = em.user;
            textBox2.Text = em.Nombre;
            textBox3.Text = em.Contraseña;
            textBox4.Text = em.ID.ToString();
            textBox5.Text = em.Telefono.ToString();
            textBox6.Text = em.Correo;
            textBox7.Text = em.Cargo;
        }

        private void habilitar(bool a)
        {
            textBox1.Enabled = a;
            textBox2.Enabled = a;
            textBox3.Enabled = a;
            textBox4.Enabled = a;
            textBox5.Enabled = a;
            textBox6.Enabled = a;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                habilitar(true);
                button1.Visible = true;
            }
            else
            {
                habilitar(false);
                button1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            em.user = textBox1.Text;
            em.Nombre = textBox2.Text;
            em.Contraseña = textBox3.Text;
            em.ID = long.Parse(textBox4.Text);
            em.Telefono = long.Parse(textBox5.Text); 
            em.Correo = textBox6.Text;
            em.Cargo = textBox7.Text;
            em.Estado = "Activo";
            em.Actualizacion_usuario();
            MessageBox.Show("Su perfil se ha actualizado correctamente");
            this.Close();
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = true;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
