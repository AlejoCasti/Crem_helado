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
    public class Loggear
    {
        Conexion cone = new Conexion();

        public string Logger(string Nombre_usu, string Contraseña_usu, string Cargo_usu)
        {
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand coma = new SqlCommand("Select Cargo_Usuario from Empleado where Nombre_usu ='" + Nombre_usu + "'and Contraseña ='" + Contraseña_usu + "'and Cargo_Usuario = '" + Cargo_usu + "'", a);
            SqlDataReader data = coma.ExecuteReader();
            if (data.Read() == true)
            {

                if (Cargo_usu == "Empleado")
                {
                    a.Close();
                    return Cargo_usu;
                    
                }
                else
                {
                    a.Close();
                    return Cargo_usu;
                }
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos");
                a.Close();
                return "";
            }
        }
            public string estado (string Nombre_usu)
            {
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand coma = new SqlCommand("Select Estado_Usuario from Empleado where Nombre_usu ='" + Nombre_usu +"' and Estado_Usuario = 'Inactivo'", a);
            SqlDataReader data = coma.ExecuteReader();
            if (data.Read() == true)
            {
                a.Close();
                return "";
            }
            else
            {
                a.Close();
                return "Activo";
                
            }
            
            }

        }
       
    }

