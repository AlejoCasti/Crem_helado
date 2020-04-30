using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Data;
using System.Data.SqlClient;

namespace Lógica
{
    public class Proveedor
    {
        Conexion conec = new Conexion();
        //Atributos
        private string Nombre_proveedor, Direccion_proveedor, correo_proveedor;
        private long Nit_proveedor, telefono_proveedor;
        //Get-Set
        public long Nit
        {
            get { return Nit_proveedor; }
            set { Nit_proveedor = value; }
        }
        public string Nombre
        {
            get { return Nombre_proveedor; }
            set { Nombre_proveedor = value; }
        }
        public string Direccion
        {
            get { return Direccion_proveedor; }
            set { Direccion_proveedor = value; }
        }
        public long telefono
        {
            get { return telefono_proveedor; }
            set { telefono_proveedor = value; }
        }
        public string Correo
        {
            get { return correo_proveedor; }
            set { correo_proveedor = value; }
        }

        //Metodos

        public string ingresar_proveedor()
        {
            int contador;
            string mensaje;
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand Registro_Proveedor = new SqlCommand("Registrar_Prove");
            Registro_Proveedor.CommandType = CommandType.StoredProcedure;
            Registro_Proveedor.Connection = a;
            Registro_Proveedor.Parameters.Add("@Nit_proveedor", Nit);
            Registro_Proveedor.Parameters.Add("@Nombre_proveedor", Nombre);
            Registro_Proveedor.Parameters.Add("@Direccion", Direccion);
            Registro_Proveedor.Parameters.Add("@Telefono", telefono);
            Registro_Proveedor.Parameters.Add("@Correo_proveedor", Correo);
            SqlParameter Contador = new SqlParameter("@Contador", 0);
            Contador.Direction = ParameterDirection.Output;
            Registro_Proveedor.Parameters.Add(Contador);
            SqlParameter Mss = new SqlParameter("@mensaje", SqlDbType.NVarChar);
            Mss.Size = 50;
            Mss.Direction = ParameterDirection.Output;
            Registro_Proveedor.Parameters.Add(Mss);
            Registro_Proveedor.ExecuteNonQuery();
            contador = int.Parse(Registro_Proveedor.Parameters["@Contador"].Value.ToString());
            if (contador > 0)
            {
                mensaje = Registro_Proveedor.Parameters["@mensaje"].Value.ToString();
            }
            else
            {
                mensaje = "El usuario ha sido registrado exitosamente";
            }
            a.Close();
            return mensaje;
        }
        public DataTable ConsultarProveedor()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand consulta = new SqlCommand("Select* from proveedor", a);
            SqlDataReader dr = consulta.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            a.Close();
            return dt;
        }
        public void ActualizarProveedor()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand update = new SqlCommand("Actualizar_Prove " + Nit + ",'" + Nombre + "','" + Direccion + "'," + telefono + ",'" + Correo + "'", a);
            SqlDataReader dr = update.ExecuteReader();
            a.Close();
        }
        public List<Proveedor> fkproveedor()
        {
            List<Proveedor> listica = new List<Proveedor>();
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select Nit_proveedor, Nombre_proveedor from Proveedor", a);
            SqlDataReader ex = consultar.ExecuteReader();
            while (ex.Read())
            {
                Proveedor ar = new Proveedor();
                ar.Nit = ex.GetInt64(0);
                ar.Nombre = ex.GetString(1);
                listica.Add(ar);
            }
            return listica;
        }
        public long obtener_codigo()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Nit_proveedor from proveedor where Nombre_proveedor ='" + Nombre + "'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Nit_proveedor"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
    }
}
