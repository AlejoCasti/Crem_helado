using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Data;
using System.Data.SqlClient;

namespace Lógica
{
    public class Maquinaria
    {
        Conexion Con = new Conexion();
        public Empleado usu = new Empleado();
        private string Nombre_maquinaria, Fecha_cambio, Tipo_maquinaria, Estado_maquinaria;
        private long Codigo_maquinaria;

        public string Nombre
        {
            get { return Nombre_maquinaria; }
            set { Nombre_maquinaria = value; }
        }
        public string Fecha
        {
            get { return Fecha_cambio; }
            set { Fecha_cambio = value; }
        }
        public string Tipo
        {
            get { return Tipo_maquinaria; }
            set { Tipo_maquinaria = value; }
        }
        public string Estado
        {
            get { return Estado_maquinaria; }
            set { Estado_maquinaria = value; }
        }
        public long Codigo
        {
            get { return Codigo_maquinaria; }
            set { Codigo_maquinaria = value; }
        }

        public bool Registrar_maquinaria()
        {
            SqlConnection a = Con.conexion();
            a.Open();
            SqlCommand Registrar = new SqlCommand("Registrar_maquinaria '"+Nombre+"','"+usu.user+"','"+Fecha+"','"+Tipo+"','"+Estado+"'", a);
            SqlDataReader regi = Registrar.ExecuteReader();
            if (regi.Read())
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
        public void Actualizar_maquinaria()
        {
            SqlConnection a = Con.conexion();
            a.Open();
            SqlCommand Actualizar = new SqlCommand("Actualizar_maquinaria "+Codigo+",'"+Nombre+"','"+usu.user+"','"+Fecha+"','"+Tipo+"','"+Estado+"'", a);
            SqlDataReader act = Actualizar.ExecuteReader();
            a.Close();
        }
        public DataTable ConsultarMaquinaria()
        {
            SqlConnection a = Con.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select * from Consulta_maquinaria", a);
            SqlDataReader cons = consultar.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(cons);
            a.Close();
            return dt;
        }
        public long obtener_codigo()
        {
            SqlConnection a = Con.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Codigo_maquinaria from maquinaria where Nombre_maquinaria ='" + Nombre + "'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Codigo_maquinaria"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public string consultar_llegadas()
        {
            SqlConnection a = Con.conexion();
            a.Open();
            SqlCommand asd = new SqlCommand("select Nombre_maquinaria from maquinaria where fecha_cambio ='"+DateTime.Today.ToShortDateString()+"'",a);
            SqlDataReader dr = asd.ExecuteReader();
            if (dr.Read())
            {
                string Nombre = (string)dr["Nombre_maquinaria"];
                a.Close();
                return Nombre;
            }
            else 
            {
                a.Close();
                return "";
            }
        }
        public void Actualizar_estado()
        {
            SqlConnection a = Con.conexion();
            a.Open();
            SqlCommand Actua = new SqlCommand("update maquinaria set Estado_maquina = 'Inactivo' where Nombre_maquinaria = '"+Nombre+"'", a);
            SqlDataReader Red = Actua.ExecuteReader();
            a.Close();
        }
        public void cargar_Maquina()
        {
            List<Empleado> listica = new List<Empleado>();
            SqlConnection a = Con.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select Nombre_maquinaria, Tipo_maquina from Maquinaria where Nombre_maquinaria ='" + Nombre + "'", a);
            SqlDataReader ex = consultar.ExecuteReader();
            if (ex.Read())
            {
                Nombre = (string)ex["Nombre_maquinaria"];
                Tipo = (string)ex["Tipo_maquina"];
            }
        }
    }
}
