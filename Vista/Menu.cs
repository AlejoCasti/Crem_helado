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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        Producto pro = new Producto();
        Pedido ped = new Pedido();
        Pedidos pedi = new Pedidos();
        Reportesvi repo = new Reportesvi();
        Maquinaria maqui = new Maquinaria();
        Transaccion tran = new Transaccion();
        string[] nombre = new string[100];
        long[] cantidad = new long[100];
        public string Cargo_usuario,Nombre_usu;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("¿Desea salir del programa?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (a == DialogResult.Yes)
            {
                Application.Exit();
            } 
        }
        private void Cargar_existencias()
        {
            int Cod_pro = 1, Comoquiera;
            pro.Codigo = Cod_pro.ToString();
            for (Cod_pro = 1; Cod_pro < 100;Cod_pro++ )
            {
                pro.Codigo = Cod_pro.ToString();
                nombre[Cod_pro] = pro.Nombre_producto();
            }
            for (Comoquiera = 1; Comoquiera < 100; Comoquiera++)
            {
                pro.Nombre = nombre[Comoquiera];
                cantidad[Comoquiera] = pro.obtener_existencia();
                if (cantidad[Comoquiera] > 0 & cantidad[Comoquiera] <= 50)
                {
                    ddToolStripMenuItem.Image = Properties.Resources.Final;
                    ToolStripMenuItem ddd = new ToolStripMenuItem();
                    ddd.Text = "El producto " + nombre[Comoquiera] + "\nno tiene la cantidad suficiente para una venta";
                    ddd.Click += new EventHandler(ddd_click);
                    ddToolStripMenuItem.DropDownItems.Add(ddd);
                }
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Usuarios newusu = new Usuarios();
            this.Close();
            newusu.Cargo_usu = Cargo_usuario;
            newusu.Nombre = Nombre_usu;
            newusu.label8.Text = Cargo_usuario;
            newusu.Show(); 
        }
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Properties.Resources.can_stock_photo_csp3516471;
        }
        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Properties.Resources.SegundoCan;
        }
        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackgroundImage = Properties.Resources.box;
        }
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackgroundImage = Properties.Resources.box2;
        }
        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.BackgroundImage = Properties.Resources.cart_add_icon;
        }
        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackgroundImage = Properties.Resources.SegundoCar;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Productos pro = new Productos();
            pro.Cargo = Cargo_usuario;
            pro.Nombre = Nombre_usu;
            if (Cargo_usuario == "Administrador")
            {
                pro.label11.Visible = true;
                pro.Show();   
            }
            else if (Cargo_usuario == "Empleado")
            {
                
                pro.label2.Text = "Busca el nombre del producto";
                pro.radioButton2.Visible = false;
                pro.radioButton3.Visible = false;
                pro.panel2.Visible = false;
                pro.panel1.Visible = false;
                pro.button1.Visible = false;
                pro.textBox9.Top = 200;
                pro.textBox9.Left = 73;
                pro.pictureBox1.Top = 200;
                pro.pictureBox1.Left = 215;
                pro.dataGridView1.Top = 247;
                pro.dataGridView1.Height = 340;
                pro.label2.Top = 170;
                pro.label1.Text = "Consultar productos";
                pro.Show();
            }
            this.Close();
        }
        private void asf_click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("¿El pedido ha llegado correctamente? ¿Desea corregirlo?", "Pedidos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                Actualizar_pedido act = new Actualizar_pedido();
                act.Nombre = Nombre_usu;
                act.cargo = Cargo_usuario;
                act.ShowDialog();
            }
            else if (a == DialogResult.No) 
            {
                Actualizar_pedido act = new Actualizar_pedido();
                act.Nombre = Nombre_usu;
                act.cargo = Cargo_usuario;
                act.Show();
                act.ped.prove.Nombre = act.comboBox2.Text;
                act.ped.prove.Nit = act.ped.prove.obtener_codigo();
                act.ped.entrega = DateTime.Today.ToShortDateString();
                act.ped.Codigo = act.ped.obtener_pedido();
                act.dataGridView1.DataSource = act.ped.Busca_3pedido();
                act.dataGridView1.Show();
                while (act.b < act.dataGridView1.RowCount)
                {
                    act.ped.Codigo = act.ped.obtener_pedido();
                    act.ped.Cantidad = long.Parse(act.dataGridView1.Rows[act.b].Cells[1].Value.ToString());
                    if (act.ped.Cantidad != 0)
                    {
                        if (act.b < act.dataGridView1.RowCount)
                        {
                            act.ped.pro.Nombre = act.dataGridView1.Rows[act.b].Cells[0].Value.ToString();
                            act.ped.pro.Existencia = act.ped.pro.obtener_existencia() + act.ped.Cantidad * act.ped.pro.obtener_CantCaja();
                            act.ped.pro.Actualizar_existencia();
                        }
                    }
                    act.b = act.b + 1;
                }
                act.Close();
            }
        }
        private void adf_click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("Esta maquina pasará a estar inactiva \n¿Desea posponer la fecha de cambio?","Maquinaria",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (a == DialogResult.No)
            {
                maqui.Nombre = maqui.consultar_llegadas();
                maqui.Actualizar_estado();
            }
            else
            {
                Maquinarias maq = new Maquinarias();
                maq.radioButton3.Checked = true;
                maq.user = Nombre_usu;
                maq.cargo = Cargo_usuario;
                maqui.cargar_Maquina();
                maq.textBox2.Text = maqui.Nombre;
                maq.textBox4.Text = maqui.Tipo;
                maq.dateTimePicker1.Value = DateTime.Now;
                maq.Show();
                this.Close();
            }
            
        }
        private void ddd_click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("¿Desea registrar un pedido?", "Pedidos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                Pedidos fff = new Pedidos();
                fff.User = Nombre_usu;
                fff.cargo = Cargo_usuario;
                fff.Show();
                this.Close();
            }
        }
        private void Menu_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.pictureBox3, "Nuestros Productos");
            toolTip1.SetToolTip(this.pictureBox2, "Gestion de Empleados");
            toolTip1.SetToolTip(this.pictureBox4, "Pedidos");
            toolTip1.SetToolTip(this.pictureBox5, "Clientes");
            toolTip1.SetToolTip(this.pictureBox1, "Salir");
            toolTip1.SetToolTip(this.pictureBox6, "Ventas");
            toolTip1.SetToolTip(this.pictureBox8, "Dotaciones");
            toolTip1.SetToolTip(this.pictureBox7, "Proveedores");
            toolTip1.SetToolTip(this.pictureBox9, "Maquinaria");
            label2.Text = Nombre_usu;
            Cargar_existencias();
            ped.entrega = DateTime.Now.ToShortDateString();
            if (DateTime.Now.Hour >= 12)
            {
                if (ped.consultar_llegadas() == true)
                {
                    ddToolStripMenuItem.Image = Properties.Resources.Final;
                    ToolStripMenuItem asf = new ToolStripMenuItem();
                    asf.Text = "Ha llegado un nuevo pedido";
                    asf.Click += new EventHandler(asf_click);
                    ddToolStripMenuItem.DropDownItems.Add(asf);
                }
                else
                {
                    ddToolStripMenuItem.Image = Properties.Resources.Final2;
                }
            }
            string maquina = maqui.consultar_llegadas();
            if (maquina != "")
            {
                ddToolStripMenuItem.Image = Properties.Resources.Final;
                ToolStripMenuItem adf = new ToolStripMenuItem();
                adf.Text = "La fecha de cambio de "+maquina+" es hoy";
                adf.Click += new EventHandler(adf_click);
                ddToolStripMenuItem.DropDownItems.Add(adf);
            
            }
            tran.Codigo = tran.obtener_ultcodigo();
            long capital = tran.obtener_Capital();;

            if (capital <= 300000)
            {
                ddToolStripMenuItem.Image = Properties.Resources.Final;
                ToolStripMenuItem Capital = new ToolStripMenuItem();
                Capital.Text = "El capital es menor de $300.000";
                ddToolStripMenuItem.DropDownItems.Add(Capital);
            }
        }        
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.Cargoa = Cargo_usuario;
            clientes.Nombre = Nombre_usu;
            clientes.Show();
            this.Close();
        }
        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackgroundImage = Properties.Resources.finance_56_128;
        }
        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.BackgroundImage = Properties.Resources.finance_56_128__1_;
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {

            Ventas Ven = new Ventas();
            Ven.User = Nombre_usu;
            Ven.cargo = Cargo_usuario;
            Ven.Show();
            this.Hide();
        }
        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackgroundImage = Properties.Resources.venta2;
        }
        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.BackgroundImage = Properties.Resources.venta;
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Proveedores pro = new Proveedores();
            pro.usuario = Nombre_usu;
            pro.cargo = Cargo_usuario;
            pro.Show();
            this.Close();
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Dotaciones Dota = new Dotaciones();
            Dota.Nombre_usu = Nombre_usu;
            Dota.Cargo_usu = Cargo_usuario;
            if (Cargo_usuario == "Empleado")
            {
                Dota.panel3.Enabled = false;
            }
            Dota.Show();
            this.Close();
        }
        private void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
            pictureBox8.BackgroundImage = Properties.Resources.dota;
        }
        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.BackgroundImage = Properties.Resources.dota2;
        }
        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.BackgroundImage = Properties.Resources.Provedores;
        }
        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.BackgroundImage = Properties.Resources.Provedores2;
        }
        private void ventasIndividualesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ventas_individuales ven = new Ventas_individuales();
            ven.Show();
        }
        private void miPerfilToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Mi_perfil perfil = new Mi_perfil();
            perfil.user = Nombre_usu;
            perfil.Show();
        }
        private void cerrarSesiónToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("¿Desea cerrar la sesión?", "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (a == DialogResult.Yes)
            {
                Login log = new Login();
                Cargo_usuario = "";
                log.Show();
                this.Close();
            } 
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Pedidos pedi = new Pedidos();
            pedi.User = Nombre_usu;
            pedi.cargo = Cargo_usuario;
            pedi.Show();
            this.Close();
        }
        private void ddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ddToolStripMenuItem.Image = Properties.Resources.Final2;
        }
        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            repo.Nombre_usu = Nombre_usu;
            repo.Cargo_usu = Cargo_usuario;
            repo.comboBox1.Items.Add("Producto");
            repo.comboBox1.Items.Add("Empleado");
            repo.comboBox1.Items.Add("Cliente");
            repo.comboBox1.Items.Add("Dotacion");
            repo.comboBox1.Items.Add("Maquinaria");
            repo.comboBox1.SelectedIndex = -1;
            repo.Show();
            this.Close();   
        }
        private void ventasToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            repo.Nombre_usu = Nombre_usu;
            repo.Cargo_usu = Cargo_usuario;
            repo.comboBox1.Items.Add("Venta");
            repo.comboBox1.Items.Add("Pedido");
            repo.comboBox1.Items.Add("Transaccion");
            repo.label1.Text = "Facturación";
            repo.comboBox1.SelectedIndex = -1;
            repo.Show();
            this.Close();
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Maquinarias maq = new Maquinarias();
            maq.user = Nombre_usu;
            maq.cargo = Cargo_usuario;
            maq.Show();
            this.Close();
        }
        private void pictureBox9_MouseEnter(object sender, EventArgs e)
        {
            pictureBox9.BackgroundImage = Properties.Resources.Maquinaria;
        }
        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            pictureBox9.BackgroundImage = Properties.Resources.Maquinaria2;

        }

        private void transaccionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transacciones tran = new Transacciones();
            tran.ShowDialog();
        }

        private void estadísticasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graficas gra = new Graficas();
            gra.user = Nombre_usu;
            gra.cargo = Cargo_usuario;
            gra.Show();
            this.Hide();
        }
        
    }

}
