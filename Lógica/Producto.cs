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
    public class Producto
    {
        Conexion conec = new Conexion();
        //Atributos
        private string Cod_producto, nombre_pro;
        private long Existencia_pro, Cant_caja, Cost_caja, Precio_mayor, Precio_vendedor, Precio_publico;
        //Get-Set
        public string Codigo
        {
            get {return Cod_producto; }
            set { Cod_producto = value; }
        }
        public string Nombre
        {
            get { return nombre_pro; }
            set { nombre_pro = value; }
        }
        public long Existencia
        {
            get { return Existencia_pro; }
            set { Existencia_pro = value; }
        }
        public long Cantidad_ca
        {
            get { return Cant_caja; }
            set {Cant_caja = value;}
        }
        public long Costo_ca
        {
            get { return Cost_caja; }
            set { Cost_caja = value; }
        }
        public long Precio_M
        {
            get { return Precio_mayor; }
            set { Precio_mayor = value; }
        }
        public long Precio_V
        {
            get { return Precio_vendedor; }
            set { Precio_vendedor = value; }
        }
        public long Precio_P
        {
            get { return Precio_publico; }
            set { Precio_publico = value; }
        }
       
        //Métodos

        public bool Ingresar_producto()
        {
            
            SqlConnection a = conec.conexion();
            a.Open();
                SqlCommand Registro_producto = new SqlCommand("Insertar_Producto '" + Nombre + "'," + Existencia + "," + Cantidad_ca + "," + Costo_ca + "," + Precio_M + "," + Precio_V + "," + Precio_P , a);
                SqlDataReader reg = Registro_producto.ExecuteReader();

                if (reg.Read() == false)
                {
                    a.Close();
                    return true;
                }
                else
                {
                    a.Close();
                    return false;
                }
        }
        public DataTable consultar_productos()
        {
          
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select * from Consulta_producto",a);
            SqlDataReader cons = consultar.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(cons);
            a.Close();
            return dt;
            
        }
        public DataTable ConsultarProductosEmpleado()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select * from Producto where Nombre_producto='"+Nombre+"'", a);
            SqlDataReader cons = consultar.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(cons);
            a.Close();
            return dt;
        }
        public bool actualizar_producto()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand actualizar = new SqlCommand("Actualizar_Producto '"+Codigo+"','"+Nombre+"',"+Existencia+","+Cantidad_ca+","+Costo_ca+","+Precio_M+","+Precio_V+","+Precio_P ,a);
            SqlDataReader re = actualizar.ExecuteReader();
            if (re.Read() == true)
            {
                a.Close();
                return true;
            }
            else
            {
                a.Close();
                return false;
            }
        }
        public List<Producto> cargar_productoV()
        {
            List<Producto> listica = new List<Producto>();
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select  Nombre_Producto from producto where existencia_producto >= 50", a);
            SqlDataReader ex = consultar.ExecuteReader();
            while (ex.Read())
            {
                Producto ar = new Producto();
                ar.Nombre = ex.GetString(0);
                listica.Add(ar);
            }
            return listica;
        }
        public List<Producto> cargar_productoP()
        {
            List<Producto> listica = new List<Producto>();
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand consultar = new SqlCommand("Select  Nombre_Producto from producto", a);
            SqlDataReader ex = consultar.ExecuteReader();
            while (ex.Read())
            {
                Producto ar = new Producto();
                ar.Nombre = ex.GetString(0);
                listica.Add(ar);
            }
            return listica;
        }
        public long obtener_codigo()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Codigo_producto from producto where Nombre_producto ='"+Nombre+"'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Codigo_producto"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public long obtener_PrecioV()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Precio_vendedor from producto where Codigo_producto ='" + Codigo + "'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Precio_vendedor"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public long obtener_PrecioP()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Precio_publico from producto where Codigo_producto ='" + Codigo + "'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Precio_publico"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public long obtener_CantCaja()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Cantidad_caja from producto where Codigo_producto ='" + Codigo + "'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Cantidad_caja"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public long obtener_CostCaja()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Costo_caja from producto where Codigo_producto ='" + Codigo + "'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Costo_caja"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public void Actualizar_existencia()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand Actu = new SqlCommand("update Producto set Existencia_producto = "+Existencia+" where Nombre_producto = '"+Nombre+"'",a);
            SqlDataReader Actua = Actu.ExecuteReader();
            a.Close();
        }
        public long obtener_existencia()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Existencia_producto from producto where Nombre_producto = '" + Nombre + "'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                long cod = (long)dr["Existencia_producto"];
                a.Close();
                return cod;
            }
            else
            {
                a.Close();
                return 0;
            }
        }
        public string Nombre_producto()
        {
            SqlConnection a = conec.conexion();
            a.Open();
            SqlCommand sss = new SqlCommand("select Nombre_producto from producto where Codigo_producto = '" + Codigo + "'", a);
            SqlDataReader dr = sss.ExecuteReader();
            if (dr.Read())
            {
                string cod = (string)dr["Nombre_producto"];
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
