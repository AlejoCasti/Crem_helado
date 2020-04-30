using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Data;
using System.Data.SqlClient;


namespace Lógica
{
    public class InicioSesion
    {
        Conexion cone = new Conexion();
        //Atributos
        private string Nombre_usu, Password, Cargo_usu;
        //Get-Set
        public string User
        {
            get { return Nombre_usu; }
            set { Nombre_usu = value; }
        }
        public string Contraseña
        {
            get { return Password; }
            set { Password = value; }
        }
        public string Cargo
        {
            get { return Cargo_usu; }
            set { Cargo_usu = value; }
        }

        //Metodos

        public string Logger()
        {
            int logueado=0;
            string mensaje = "";
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand logear = new SqlCommand("loggin");
            logear.CommandType = CommandType.StoredProcedure;
            logear.Connection = a;
            logear.Parameters.Add("@nombre", User);
            logear.Parameters.Add("@contraseña", Contraseña);
            SqlParameter logg = new SqlParameter("@logg",0);
            logg.Direction = ParameterDirection.Output;
            logear.Parameters.Add(logg);
            SqlParameter Mss = new SqlParameter("@mensaje",SqlDbType.NVarChar);
            Mss.Size = 50;
            Mss.Direction = ParameterDirection.Output;
            logear.Parameters.Add(Mss);
            logear.ExecuteNonQuery();
            
            logueado = Int32.Parse(logear.Parameters["@logg"].Value.ToString());

            if (logueado > 0)
            {
                mensaje = logear.Parameters["@mensaje"].Value.ToString();
            }

            a.Close();
            return mensaje;
        }
        public string obtener_cargo()
        {
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Cargo_usuario from Empleado where Nombre_usu ='" + User + "'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                string cod = (string)dr["cargo_usuario"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return "";
            }
        }

    }
}

