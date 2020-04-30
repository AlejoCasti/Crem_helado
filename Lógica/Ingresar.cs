using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lógica
{
    public class Ingresar
    {
        Conexion Cone = new Conexion();
        public void Registro_usu(string Nombre_Usuario, string Nombre_Empleado, string Contraseña, string ID, string Cargo)
        {
            SqlConnection Cconexion = Cone.conexion();
            Cconexion.Open();
            SqlCommand Consulta_usuario_existente = new SqlCommand("Select Nombre_usu from Empleado where Nombre_usu = '" + Nombre_Usuario + "'", Cconexion);
            SqlDataReader Consulta = Consulta_usuario_existente.ExecuteReader();
            if (Consulta.Read())
            {
                MessageBox.Show("El usuario ya existe");
            }
            else
            {
                Consulta.Close();
                SqlCommand Registro_usu = new SqlCommand("Insert into Empleado values ('" + Nombre_Usuario + "','" + Nombre_Empleado + "','" + Contraseña + "','" + ID + "','Activo','" + Cargo + "')", Cconexion);
                SqlDataReader registrar = Registro_usu.ExecuteReader();
                registrar.Close();
                Cconexion.Close();
                MessageBox.Show("El registro ha sido todo un exito");
            }
        }
        public DataTable Registro_grid()
        {
            SqlConnection Cconexion = Cone.conexion();
            Cconexion.Open();
            SqlCommand Consulta_usuario_reg = new SqlCommand("Select * from Empleado", Cconexion);
            SqlDataReader Con = Consulta_usuario_reg.ExecuteReader();
            DataTable DG = new DataTable();
            DG.Load(Con);
            Cconexion.Close();
            return DG;
        }
        public DataTable Registro_usu_empleado()
        {
            SqlConnection Cconexion = Cone.conexion();
            Cconexion.Open();
            SqlCommand registro_empleado = new SqlCommand("Select Nombre_usu,Nombre_empleado,ID_Usuario,Estado_Usuario, Cargo_Usuario from Empleado ",Cconexion);
            SqlDataReader Regis = registro_empleado.ExecuteReader();
            DataTable Grid = new DataTable();
            Grid.Load(Regis);
            Cconexion.Close();
            return Grid;
        }
        public DataTable Consulta_usuarios()
        {
            SqlConnection Cconexion = Cone.conexion();
            Cconexion.Open();
            SqlCommand registro_empleado = new SqlCommand("Select * from Empleado",Cconexion);
            SqlDataReader Regis = registro_empleado.ExecuteReader();
            DataTable Grid = new DataTable();
            Grid.Load(Regis);
            Cconexion.Close();
            return Grid;
        }

    }
}
