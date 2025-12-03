using Microsoft.Data.SqlClient;

namespace Capa_Dato
{
    public class Conexion
    {
        //Format de conectarse a una base de datos SQL Server

        public static SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source=DESKTOP-V3LCRJU\\SQLEXPRESS;Initial Catalog=DBCarrito;Integrated Security=True;Trust Server Certificate=True");
        }

        //private static string cadena = "Data Source=DESKTOP-V3LCRJU\\SQLEXPRESS;Initial Catalog=DBCarrito;Integrated Security=True;Trust Server Certificate=True";

        //public static SqlConnection GetConnection()
        //{
        //    return new SqlConnection(cadena);
        //}

        //public static SqlConnection GetConnection()
        //{
        //    SqlConnection cadenaConexion = new SqlConnection("Data Source=DESKTOP-V3LCRJU\\SQLEXPRESS;Initial Catalog=DBCarrito;Integrated Security=True;Trust Server Certificate=True");
        //    //cadenaConexion.Open();
        //    return cadenaConexion;
        //}

        //una forma de realizar la conexion pero en la version 4.8 para abajo

        //public static string cn = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
    }
}
