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
    public partial class Productos : Form
    {
        Producto prod = new Producto();
        Validaciones Vali = new Validaciones();
        public string Cargo,Nombre;
        public Productos()
        {
            InitializeComponent();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
                panel2.Enabled = false;
                DataTable ne = prod.consultar_productos();
                dataGridView1.DataSource = ne;
                dataGridView1.ClearSelection();
                button1.Visible = false;
                button3.Visible = false;                       
        }

     

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                panel2.Enabled = true;
                button1.Visible = true;
                button3.Visible = false;
                textBox3.Enabled = true;
                dataGridView1.ClearSelection();
                limpiar();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                panel2.Enabled = false;
                textBox3.Enabled = false;
                DataTable ne = prod.consultar_productos();
                dataGridView1.DataSource = ne;
                dataGridView1.ClearSelection();
                MessageBox.Show("Seleccione el codigo del producto que desea actualizar");
                button1.Visible = false;
                button3.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
          if ( textBox2.Text == "" | textBox3.Text == "" | textBox4.Text == "" | textBox5.Text == "" | textBox6.Text == "" | textBox7.Text == "" | textBox8.Text == "")
          {
              MessageBox.Show("Por favor llena todos los datos correspondientes");
          }
          else
          {
              prod.Nombre = textBox2.Text;
              prod.Existencia = long.Parse(textBox3.Text);
              prod.Cantidad_ca = long.Parse(textBox4.Text);
              prod.Costo_ca = long.Parse(textBox5.Text);
              prod.Precio_M = long.Parse(textBox6.Text);
              prod.Precio_V = long.Parse(textBox7.Text);
              prod.Precio_P = long.Parse(textBox8.Text);
              if (prod.Ingresar_producto() == true)
              {
                  MessageBox.Show("Se ha ingresado el producto correctamente");
                  DataTable ne = prod.consultar_productos();
                  dataGridView1.DataSource = ne;
                  dataGridView1.ClearSelection();
              }
              else
              {
                  MessageBox.Show("Ha ocurrido un error al ingresar el producto, revisa tus datos");
              }
          }
          limpiar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable ne = prod.consultar_productos();
            dataGridView1.DataSource = ne;
            dataGridView1.ClearSelection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" | textBox3.Text == "" | textBox4.Text == "" | textBox5.Text == "" | textBox6.Text == "" | textBox7.Text == "" | textBox8.Text == "")
            {
                MessageBox.Show("Por favor llena todos los datos correspondientes");
            }
            else
            {
                prod.Nombre = textBox2.Text;
                prod.Codigo = prod.obtener_codigo().ToString(); ;
                prod.Existencia = long.Parse(textBox3.Text);
                prod.Cantidad_ca = long.Parse(textBox4.Text);
                prod.Costo_ca = long.Parse(textBox5.Text);
                prod.Precio_M = long.Parse(textBox6.Text);
                prod.Precio_V = long.Parse(textBox7.Text);
                prod.Precio_P = long.Parse(textBox8.Text);
                if (prod.actualizar_producto() == true)
                {
                    MessageBox.Show("El producto se ha actualizado correctamente");
                }
                else 
                {
                    MessageBox.Show("Ha ocurrido un error al actualizar el producto, revisa tus datos");
                }
                DataTable ne = prod.consultar_productos();
                dataGridView1.DataSource = ne;
                limpiar();
                dataGridView1.ClearSelection();
            }
        }

        public void limpiar()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBox8.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                panel2.Enabled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == null)
            {
                MessageBox.Show("Por favor ingrese el nombre");
            }
            else
            {
                prod.Nombre = textBox9.Text;
                DataTable ne = prod.ConsultarProductosEmpleado();
                dataGridView1.DataSource = ne;
                dataGridView1.ClearSelection();
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Properties.Resources.buscar;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Properties.Resources.segundobuscar;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Menu Home = new Menu();
            Home.Cargo_usuario = Cargo;
            Home.Nombre_usu = Nombre;
            if (Cargo == "Empleado")
            {
                Home.pictureBox2.Enabled = false;
                Home.pictureBox3.Enabled = false;
                Home.pictureBox4.Enabled = false;
                Home.pictureBox7.Enabled = false;
                Home.pictureBox8.Enabled = false;
                Home.pictureBox9.Enabled = false;
                Home.ventasToolStripMenuItem2.Enabled = false;
            }
            Home.label2.Text = Cargo;
            Home.Show();
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Vali.Nombres(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Vali.Números(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            Vali.Números(e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            Vali.Números(e);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            Vali.Números(e);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            Vali.Números(e);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            Vali.Números(e);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            Vali.Nombres(e);
        }
        Reportes Repo = new Reportes();
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text != "")
            {
                dataGridView1.DataSource = Repo.Buscar("Producto", textBox9.Text);
                dataGridView1.ClearSelection();
            }
        }
    }
}
