using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Data;
using System.Data.SqlClient;

namespace Lógica
{
    public class Cliente
    {
        Conexion cone = new Conexion();
        //Atributos
        long Numero_identificacion, Telefonos;
        string Nombres,Correos,Direcciones;
        //Get-Set
        public string Nombre
        {
            get { return Nombres; }
            set { Nombres = value; }
        }
        public string Correo
        {
            get { return Correos; }
            set { Correos = value; }
        }
        public string Direccion
        {
            get { return Direcciones; }
            set { Direcciones = value; }
        }
        public long ID
        {
            get { return Numero_identificacion; }
            set { Numero_identificacion = value; }
        }
        public long Telefono
        {
            get { return Telefonos; }
            set { Telefonos = value; }
        }

        //Métodos

        public string Registro_cli()
        {
            int contador;
            string mensaje;
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand Registro_Cliente = new SqlCommand("Registrar_Cli");
            Registro_Cliente.CommandType = CommandType.StoredProcedure;
            Registro_Cliente.Connection = a;
            Registro_Cliente.Parameters.Add("@Numero_identificacion", ID);
            Registro_Cliente.Parameters.Add("@Nombre_cliente", Nombre);
            Registro_Cliente.Parameters.Add("@Correo_cliente", Correo);
            Registro_Cliente.Parameters.Add("@Telefono_cliente", Telefono);
            Registro_Cliente.Parameters.Add("@Direccion_cliente", Direccion);
            SqlParameter Contador = new SqlParameter("@Contador", 0);
            Contador.Direction = ParameterDirection.Output;
            Registro_Cliente.Parameters.Add(Contador);
            SqlParameter Mss = new SqlParameter("@mensaje", SqlDbType.NVarChar);
            Mss.Size = 50;
            Mss.Direction = ParameterDirection.Output;
            Registro_Cliente.Parameters.Add(Mss);
            Registro_Cliente.ExecuteNonQuery();

            contador = int.Parse(Registro_Cliente.Parameters["@Contador"].Value.ToString());

            if (contador > 0)
            {
                mensaje = Registro_Cliente.Parameters["@mensaje"].Value.ToString();
            }
            else
            {
                mensaje = "El usuario ha sido registrado exitosamente";
            }
            a.Close();
            return mensaje;
        }
        public DataTable Consulta_cliente()
        {
            SqlConnection Cconexion = cone.conexion();
            Cconexion.Open();
            SqlCommand Consulta_cli= new SqlCommand("Select * from Consulta_cliente ", Cconexion);
            SqlDataReader Con = Consulta_cli.ExecuteReader();
            DataTable Grid = new DataTable();
            Grid.Load(Con);
            Cconexion.Close();
            return Grid;
        }
        public void Actualizacion_cliente()
        {
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand Actualizacion = new SqlCommand("Actualizar_cli " + ID + ",'" + Nombre + "','" + Correo + "'," + Telefono + ",'" + Direccion + "'", a);
            SqlDataReader actualizacion = Actualizacion.ExecuteReader();
            a.Close();
        }
        public List<Cliente> fkcliente()
        {
            List<Cliente> listica = new List<Cliente>();
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select Numero_identificacion, Nombre_cliente from Cliente", a);
            SqlDataReader ex = consultar.ExecuteReader();
            while (ex.Read())
            {
                Cliente ar = new Cliente();
                ar.ID = ex.GetInt64(0);
                ar.Nombre = ex.GetString(1);
                listica.Add(ar);
            }
            return listica;
        }
        public long obtener_codigo()
        {
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Numero_identificacion from Cliente where Nombre_cliente ='" + Nombre + "'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Numero_identificacion"];
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
