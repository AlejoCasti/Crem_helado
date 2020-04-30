using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Data;
using System.Data.SqlClient;

namespace Lógica
{
    public class Venta
    {
        Conexion con = new Conexion();
        private string  Fecha_venta,Tipo_venta;
        private long Cantidad_recarga, Codigo_venta, Valor_total, Cantidad_Entrega, Cantidad_Recibida;
        public Producto pro = new Producto();
        public Cliente cli = new Cliente();
        public Empleado usu = new Empleado();

        public long Codigo
        {
            get { return Codigo_venta; }
            set { Codigo_venta = value; }
        }
        public string Fecha
        {
            get { return Fecha_venta; }
            set { Fecha_venta = value; }
        }
        public long Recarga
        {
            get { return Cantidad_recarga; }
            set { Cantidad_recarga = value; }
        }
        public long Total
        {
            get { return Valor_total; }
            set { Valor_total = value; }
        }
        public long Entrega
        {
            get { return Cantidad_Entrega; }
            set { Cantidad_Entrega = value; }
        }
        public long Recibida 
        {
            get { return Cantidad_Recibida; }
            set { Cantidad_Recibida = value; }
        }
        public string Tipo
        {
            get { return Tipo_venta; }
            set { Tipo_venta = value; }
        }

        public bool RegistrarVenta()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand Registro_Venta = new SqlCommand("Registrar_venta '" + usu.user + "'," + cli.ID + ",'" + Fecha + "',"+ Total+",'"+Tipo+"'", a);
            SqlDataReader Actu = Registro_Venta.ExecuteReader();
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
        public bool ActualizarVenta()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand Act = new SqlCommand("Actualizar_venta'"+Codigo+"','"+usu.user+"',"+cli.ID+",'"+Fecha+"',"+Total ,a);
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
        public bool Registrar_3venta()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand Reg_3 = new SqlCommand("registrar_3venta '"+Codigo+"','"+pro.Codigo+"',"+Entrega+","+Recibida+","+Recarga , a);
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
        public bool Actualizar_3venta()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand Act_3 = new SqlCommand("Actualizar_3venta '" + Codigo + "','" + pro.Codigo + "'," + Entrega + "," + Recibida, a);
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
        public long obtener_codigo()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Codigo_venta from Venta where Numero_identificacion =" + cli.ID + " and Fecha_venta = '"+Fecha+"'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Codigo_venta"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public DataTable Consulta_venta()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand consulta = new SqlCommand("select * from Venta", a);
            SqlDataReader cona = consulta.ExecuteReader();
            DataTable b = new DataTable();
            b.Load(cona);
            a.Close();
            return b;
        }
        public void Actualizacion_cliente()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand Actualizacion = new SqlCommand("Actualizar_venta " + Codigo + ",'" + usu.user + "','" + cli.ID + "'," + Fecha + ",'" + Total + "'", a);
            SqlDataReader actualizacion = Actualizacion.ExecuteReader();
            a.Close();
        }
        public DataTable Busca_3venta()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Consulta_3venta '"+Codigo+"','"+Fecha+"'", a);
            SqlDataReader ex = consultar.ExecuteReader();
            DataTable aa = new DataTable();
            aa.Load(ex);
            a.Close();
            return aa;   
        }
        public long obtener_ultcodigo()
        {
            SqlConnection C = con.conexion();
            C.Open();
            SqlCommand sss = new SqlCommand("select Top 1 Codigo_venta from Venta Order by Codigo_venta Desc ", C);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Codigo_venta"];
                C.Close();
                return cod;
            }
            else
            {
                C.Close();
                return 0;
            }
        }
    }
}