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
    public partial class Login : Form 
    {
        public Login()
        {
            InitializeComponent();
        }
        Validaciones validacion = new Validaciones();
        Menu home = new Menu();
        InicioSesion ini = new InicioSesion();
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" | textBox2.Text == "")
            {
                MessageBox.Show("Por favor ingrese todos los datos");
            }
            else
            {
                ini.User = textBox1.Text;
                ini.Contraseña = textBox2.Text;
                string mensaje = ini.Logger();
                if (mensaje != "")
                {
                    MessageBox.Show(mensaje);
                    home.Nombre_usu = ini.User;
                    home.Cargo_usuario = ini.obtener_cargo();
                    if (home.Cargo_usuario == "Empleado")
                    {
                        home.pictureBox2.Enabled = false;
                        home.pictureBox3.Enabled = false;
                        home.pictureBox4.Enabled = false;
                        home.pictureBox7.Enabled = false;
                        home.pictureBox9.Enabled = false;
                        home.ventasToolStripMenuItem2.Enabled = false;
                    }
                    home.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Nombre de usuario o contraseña incorectos \n \nVerifique sus datos");
                }
            }
        }
      
        private void pictureBox1_Click(object sender, EventArgs e)
        {
           DialogResult a =  MessageBox.Show("¿Desea salir del programa?","Salir",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
           if (a == DialogResult.Yes)
           {
               Application.Exit();
           }
           
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            validacion.Cuentas(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacion.Pass(e);
        }
    }
}
