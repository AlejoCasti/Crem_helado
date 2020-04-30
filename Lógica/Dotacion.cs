using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Datos;

namespace Lógica
{
    public class Dotacion
    {
        Conexion con = new Conexion();
        public Cliente cli = new Cliente();
        private string Nombre_dotacion, Nombre_fiador;
        private long  ID_fiador, Telefono_fiador;
        int Existencia_dotacion, Cantidad_libre;

        public string Nombre
        {
            get {return Nombre_dotacion;}
            set {Nombre_dotacion = value;}
        }
        public int Cantidad
        {
            get {return Existencia_dotacion; }
            set { Existencia_dotacion = value; }
        }
        public int Cantidadlibre
        {
            get { return Cantidad_libre; }
            set { Cantidad_libre = value; }
        }
        public string Nombrefi
        {
            get {return Nombre_fiador ;}
            set { Nombre_fiador = value; }
        }
        public long ID
        {
            get { return ID_fiador; }
            set { ID_fiador = value; }
        }
        public long Telefono
        {
            get { return Telefono_fiador; }
            set { Telefono_fiador = value; }
        }
        public void Registrar_dotacion()
        {
            SqlConnection conexion = con.conexion();
            conexion.Open();
            SqlCommand consultar = new SqlCommand("Select Nombre_dotacion from Dotacion where Nombre_dotacion ='" + Nombre + "'", conexion);
            SqlDataReader cons = consultar.ExecuteReader();
            if (cons.Read() == false)
            {
                cons.Close();
                SqlCommand registrar = new SqlCommand("Registrar_dotacion '" + Nombre + "'," + Cantidad +","+Cantidad_libre, conexion);
                SqlDataReader exe = registrar.ExecuteReader();
            }
            conexion.Close();
        }

        public List<Dotacion> Consultar_dotacion()
        {
            List<Dotacion> listica = new List<Dotacion>();
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select Nombre_dotacion from Dotacion", a);
            SqlDataReader ex = consultar.ExecuteReader();
            while (ex.Read())
            {
                Dotacion ar = new Dotacion();
                ar.Nombre = ex.GetString(0);
                listica.Add(ar);
            }
            return listica;
        }
        public void Registrar_3dotacion()
        {
            SqlConnection conexion = con.conexion();
            conexion.Open();
            SqlCommand registrar = new SqlCommand("Registrar_3dotacion "+cli.ID+",'"+Nombre+"',"+ID+",'"+Nombrefi+"',"+Telefono+",1", conexion);
            SqlDataReader exe = registrar.ExecuteReader();
            conexion.Close();
        }
        public bool consultar_dota()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand consul = new SqlCommand("select * from dotacion");
            SqlDataReader re = consul.ExecuteReader();
            if (re.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int obtener_exitencia()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Cantidad_libre from Dotacion where Nombre_dotacion = '" + Nombre + "'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                int cod = (int)dr["Cantidad_libre"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public void Actualizar_dotacion()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand actua = new SqlCommand("Update Dotacion set Cantidad_dotacion ="+Cantidad+" where Nombre_dotacion = '"+Nombre+"'",a);
            SqlDataReader ac = actua.ExecuteReader();
            a.Close();
        }
        public bool Consultar_3dotacion()
        {
            SqlConnection a = con.conexion();
            a.Open();
            SqlCommand cona = new SqlCommand("Select * from Detalle_dotacion where Numero_identificacion ="+cli.ID+" and Nombre_dotacion ='"+Nombre+"'",a);
            SqlDataReader cons = cona.ExecuteReader();
            if (cons.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
