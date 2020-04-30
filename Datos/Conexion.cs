using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Datos
{
    public class Conexion
    {

        public SqlConnection conexion()
        {
            SqlConnection conexión = new SqlConnection("data source = CRISTIAN-PC\\SQLEXPRESS;Database =Crem_Helado;Integrated Security = true");
            return conexión;
        }
    }
}
