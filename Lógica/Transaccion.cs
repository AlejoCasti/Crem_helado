using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Data;
using System.Data.SqlClient;


namespace Lógica
{
    public class Transaccion
    {
        Conexion Con = new Conexion();
        public Venta ven = new Venta();
        public Pedido ped = new Pedido();

        private string Fecha_trans, Codigo_pre, Descripcion_tran;
        private long Codigo_trans, Ingresos_trans, Gastos_trans, Capital_total;

        public string Fecha
        {
            get { return Fecha_trans; }
            set { Fecha_trans = value; }
        }
        public string Descripcion
        {
            get { return Descripcion_tran; }
            set { Descripcion_tran = value; }
        }
        public string CodigoPre
        {
            get { return Codigo_pre; }
            set { Codigo_pre = value; }
        }
        public long Codigo
        {
            get { return Codigo_trans; }
            set { Codigo_trans = value; }
        }
        public long Ingresos
        {
            get { return Ingresos_trans; }
            set { Ingresos_trans = value; }
        }
        public long Gastos
        {
            get { return Gastos_trans; }
            set { Gastos_trans = value; }
        }
        public long Capital
        {
            get { return Capital_total; }
            set { Capital_total = value; }
        }
        public void Registrar_transven()
        {
            SqlConnection Conec = Con.conexion();
            Conec.Open();
            SqlCommand Registrar = new SqlCommand("Registrar_trans null ,"+ven.Codigo+",'Empresarial','"+Fecha+"',"+Ingresos+","+Gastos+","+Capital, Conec);
            SqlDataReader Reg = Registrar.ExecuteReader();
            Conec.Close();
        }
        public void Registrar_transped()
        {
            SqlConnection Conec = Con.conexion();
            Conec.Open();
            SqlCommand Registrar = new SqlCommand("Registrar_trans " + ped.Codigo + ",null ,'Empresarial'," + Fecha + "'," + Ingresos + "," + Gastos + "," + Capital, Conec);
            SqlDataReader Reg = Registrar.ExecuteReader();
            Conec.Close();
        }
        public void Registrar_trans()
        {
            SqlConnection Conec = Con.conexion();
            Conec.Open();
            SqlCommand Registrar = new SqlCommand("Registrar_trans null, null ,'"+Descripcion+" Individual','" + Fecha + "',"+Ingresos+", null," + Capital, Conec);
            SqlDataReader Reg = Registrar.ExecuteReader();
            Conec.Close();
        }
        public long obtener_ultcodigo()
        {
            SqlConnection C = Con.conexion();
            C.Open();
            SqlCommand sss = new SqlCommand("select Top 1 Codigo_trans from Transaccion Order by Codigo_trans Desc ", C);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Codigo_trans"];
                C.Close();
                return cod;
            }
            else
            {
                C.Close();
                return 0;
            }
        }
        public long obtener_Capital()
        {
            SqlConnection a = Con.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Capital_total from Transaccion where Codigo_trans =" + Codigo , a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Capital_total"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public void Actualizar_transVen()
        {
            SqlConnection Cone = Con.conexion();
            Cone.Open();
            SqlCommand Actualizar = new SqlCommand("Actualizar_transV "+Codigo+","+Ingresos+","+Capital , Cone);
            SqlDataReader Actu = Actualizar.ExecuteReader();
            Cone.Close();
        }
        public void Actualizar_transPed()
        {
            SqlConnection Cone = Con.conexion();
            Cone.Open();
            SqlCommand Actualizar = new SqlCommand("Actualizar_transP " + Codigo + "," + Gastos + "," + Capital, Cone);
            SqlDataReader Actu = Actualizar.ExecuteReader();
            Cone.Close();
        }
        public long obtener_Codigo()
        {
            SqlConnection a = Con.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Codigo_trans from Transaccion where Codigo_venta =" + ven.Codigo, a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Codigo_trans"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public long obtener_ultcodigo2()
        {
            SqlConnection C = Con.conexion();
            C.Open();
            SqlCommand sss = new SqlCommand("select Top 2 Codigo_trans from Transaccion Order by Codigo_trans Desc ", C);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Codigo_trans"];
                C.Close();
                return cod;
            }
            else
            {
                C.Close();
                return 0;
            }
        }
        public DataTable Consulta()
        {
            SqlConnection a = Con.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select * from Consulta_transaccion2 ", a);
            SqlDataReader Consu = consultar.ExecuteReader();
            DataTable Tabla = new DataTable();
            Tabla.Load(Consu);
            a.Close();
            return Tabla;
        }
    }
}
