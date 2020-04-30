using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Data;
using System.Data.SqlClient;

namespace Lógica
{
    public class Empleado 
    {
        Conexion cone = new Conexion();
        //Atributos
        private string Nombre_usu, Nombre_E, Password, Correo_usu, Cargo_usu, Estado_usu;
        private long ID_e, Telefono_e;
        //Metodos get-set
        public string user
        {
            get { return Nombre_usu; }
            set { Nombre_usu = value; }
        }
        public string Nombre
        {
            get { return Nombre_E; }
            set { Nombre_E = value; }
        }
        public string Contraseña
        {
            get { return Password; }
            set { Password = value; }
        }
        public string Correo
        {
            get { return Correo_usu; }
            set { Correo_usu = value; }
        }
        public string Cargo
        {
            get { return Cargo_usu; }
            set { Cargo_usu = value; }
        }
        public long ID
        {
            get { return ID_e; }
            set { ID_e = value; }
        }
        public long Telefono
        {
            get { return Telefono_e; }
            set { Telefono_e = value; }
        }
        public string Estado
        {
            get { return Estado_usu; }
            set { Estado_usu = value; }
        }

        //Métodos

        public string Registro_usu()
        {
            int contador;
            string mensaje;
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand Registro_Empleado = new SqlCommand("Registrar_usu");
            Registro_Empleado.CommandType = CommandType.StoredProcedure;
            Registro_Empleado.Connection = a;
            Registro_Empleado.Parameters.Add("@Nombre_usu", user);
            Registro_Empleado.Parameters.Add("@Nombre_Empleado", Nombre);
            Registro_Empleado.Parameters.Add("@Contraseña_usu", ID);
            Registro_Empleado.Parameters.Add("@Cargo_usu", Cargo);
            Registro_Empleado.Parameters.Add("@ID_usu", ID);
            Registro_Empleado.Parameters.Add("@Telefono_usu", Telefono);
            Registro_Empleado.Parameters.Add("@Estado_usu", "Activo");
            Registro_Empleado.Parameters.Add("@Correo_usu", Correo);
            SqlParameter Contador = new SqlParameter("@Contador", 0);
            Contador.Direction = ParameterDirection.Output;
            Registro_Empleado.Parameters.Add(Contador);
            SqlParameter Mss = new SqlParameter("@mensaje", SqlDbType.NVarChar);
            Mss.Size = 50;
            Mss.Direction = ParameterDirection.Output;
            Registro_Empleado.Parameters.Add(Mss);
            Registro_Empleado.ExecuteNonQuery();

            contador = Int32.Parse(Registro_Empleado.Parameters["@Contador"].Value.ToString());

            if (contador > 0)
            {
                mensaje = Registro_Empleado.Parameters["@mensaje"].Value.ToString();
                return mensaje;
            }
            mensaje = "El usuario ha sido registrado correctamente";
            a.Close();
            return mensaje;
        }
        public void Eliminar()
        {
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand Actualizacion = new SqlCommand("Delete Empleado where Nombre_usu = '" + user + "'", a);
            SqlDataReader actualizacion = Actualizacion.ExecuteReader();
            a.Close();
        }
        public void Actualizacion_usuario()
        {
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand Actualizacion = new SqlCommand("Actualizar_usu '"+user+"','"+Nombre+"','"+Contraseña+"',"+ID+","+Telefono+",'"+Correo+"','"+Estado+"','"+Cargo+"'", a);
            SqlDataReader actualizacion = Actualizacion.ExecuteReader();
            a.Close();
        }
        public void Actualizacion_usuario2()
        {
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand Actualizacion = new SqlCommand("Actualizar_usu2 '" + user + "','" + Nombre + "'," + ID + "," + Telefono + ",'" + Correo + "','" + Estado + "','" + Cargo + "'", a);
            SqlDataReader actualizacion = Actualizacion.ExecuteReader();
            a.Close();
        }
        public DataTable Consulta_usuarios()
        {
            SqlConnection Cconexion = cone.conexion();
            Cconexion.Open();
            SqlCommand registro_empleado = new SqlCommand("Select * from Consulta_Empleado", Cconexion);
            SqlDataReader Regis = registro_empleado.ExecuteReader();
            DataTable Grid = new DataTable();
            Grid.Load(Regis);
            Cconexion.Close();
            return Grid;
        }
        public List<Empleado> cargar_empleado()
        {
            List<Empleado> listica = new List<Empleado>();
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select Nombre_usu, Nombre_Empleado, Contraseña, ID_usuario, Telefono_usuario, Correo_usuario, Cargo_usuario from Empleado where Nombre_usu ='"+ Nombre+"'", a);
            SqlDataReader ex = consultar.ExecuteReader();
            while (ex.Read())
            {
                Empleado ar = new Empleado();
                ar.user = ex.GetString(0);
                ar.Nombre = ex.GetString(1);
                ar.Contraseña = ex.GetString(2);
                ar.ID = ex.GetInt64(3);
                ar.Telefono = ex.GetInt64(4);
                ar.Correo = ex.GetString(5);
                ar.Cargo = ex.GetString(6);
                user = ar.user;
                Nombre = ar.Nombre;
                Contraseña = ar.Contraseña;
                ID = ar.ID;
                Telefono = ar.Telefono;
                Correo = ar.Correo;
                Cargo = ar.Cargo;
                listica.Add(ar);
            }
            return listica;
        }
    }
}
