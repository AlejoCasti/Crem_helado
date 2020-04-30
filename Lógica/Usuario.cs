using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Data;
using System.Data.SqlClient;

namespace Lógica
{
    public class Usuario 
    {
        Conexion cone = new Conexion();

        public void Actualizacion_usuario(string nombre_cuenta, string nombre_usu, string contraseña_usu, string ID_usu, string Estado_usu, string Cargo_usu)
        {
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand Actualizacion = new SqlCommand("Update Empleado set Nombre_usu ='" + nombre_cuenta + "',Nombre_empleado = '" + nombre_usu + "',ID_Usuario = '" + ID_usu + "',Estado_Usuario = '" + Estado_usu + "',Cargo_Usuario = '" + Cargo_usu + "' where Nombre_usu ='" + nombre_cuenta + "'", a);
            SqlDataReader actualizacion = Actualizacion.ExecuteReader();
            a.Close();
        }
        public void Eliminar(string Nombre_usuario)
        {
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand Actualizacion = new SqlCommand("Delete Empleado where Nombre_usu = '" + Nombre_usuario + "'", a);
            SqlDataReader actualizacion = Actualizacion.ExecuteReader();
            a.Close();
        }
        public DataTable Buscar_usu(string Nombre_cuenta)
        {
            SqlConnection a = cone.conexion();
            a.Open();
            SqlCommand Actualizacion = new SqlCommand("Select * from Empleado where Nombre_usu = '"+Nombre_cuenta+"'",a);
            DataTable grid = new DataTable();
            SqlDataReader actualizacion = Actualizacion.ExecuteReader();
            grid.Load(actualizacion);
            a.Close();
            return grid;
        }
    }
}
