using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Data;
using System.Data.SqlClient;

namespace Lógica
{
    public class Pedido
    {
        Conexion con = new Conexion();
        //Atributos
        private long Codigo_pedido, valor_total, cantidad_productos;
        private string Fecha_pedido, fecha_entrega;
        public Proveedor prove = new Proveedor();
        public Empleado usu = new Empleado();
        public Producto pro = new Producto();
        //Get-Set
        public long Codigo
        {
            get { return Codigo_pedido; }
            set { Codigo_pedido = value; }
        }
        public long Total
        {
            get { return valor_total; }
            set { valor_total = value; }
        }
        public long Cantidad
        {
            get { return cantidad_productos; }
            set { cantidad_productos = value; }
        }
        public string Fecha
        {
            get { return Fecha_pedido; }
            set { Fecha_pedido = value; }
        }
        public string entrega
        {
            get { return fecha_entrega; }
            set { fecha_entrega = value; }
        }

        //Métodos

        public bool RegistrarPedido()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand Registro_Pedido = new SqlCommand("Registrar_pedido " + prove.Nit + ",'" + usu.user + "','" + Fecha + "','"+entrega+"'," + Total, a);
            SqlDataReader Actu = Registro_Pedido.ExecuteReader();
            if (Actu.Read() == false)
            {
                a.Close();
                return true;
            }
            else
            {
                a.Close();
                return false;
            }
        }
        public bool consultar_llegadas()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand CMD = new SqlCommand("consultar_llegada '"+entrega+"'", a);
            SqlDataReader dr = CMD.ExecuteReader();
            if(dr.Read()==true)
            {
                a.Close();
                return true;
            }
            else
            {
                a.Close();
                return false;
            }
        }
        public bool ActualizarPedido()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand Act = new SqlCommand("Actualizar_pedido " + Codigo + "," + prove.Nit + ",'" +usu.user+ "','" + Fecha + "','"+entrega+"'," + Total, a);
            SqlDataReader Actu = Act.ExecuteReader();
            if (Actu.Read())
            {
                a.Close();
                return true;
            }
            else
            {
                a.Close();
                return false;
            }
        }
        public bool Registrar_3pedido()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand Reg_3 = new SqlCommand("registrar_3pedido '" + Codigo + "','" + pro.Codigo + "'," +Cantidad, a);
            SqlDataReader Reg = Reg_3.ExecuteReader();
            if (Reg.Read())
            {
                a.Close();
                return false;
            }
            else
            {
                a.Close();
                return true;
            }
        }
        public bool Actualizar_3pedido()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand Act_3 = new SqlCommand("Actualizar_3pedido '" + Codigo + "','" + pro.Codigo + "'," +Cantidad, a);
            SqlDataReader Act = Act_3.ExecuteReader();
            if (Act.Read())
            {
                a.Close();
                return true;
            }
            else
            {
                a.Close();
                return false;
            }
        }
        public long obtener_pedido()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Codigo_pedido from Pedido where Nit_proveedor =" + prove.Nit + " and Fecha_entrega = '" + entrega + "'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Codigo_pedido"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public long obtener_ultcodigo()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Top 1 Codigo_pedido from pedido Order by Codigo_pedido Desc ", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Codigo_pedido"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public DataTable consultarPedido()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand cmd = new SqlCommand("Select * from consulta_pedido", a);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }
        public DataTable Busca_3pedido()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Cargar_3pedido '" + Codigo + "','" + entrega + "'", a);
            SqlDataReader ex = consultar.ExecuteReader();
            DataTable aa = new DataTable();
            aa.Load(ex);
            a.Close();
            return aa;
        }
    }
}
