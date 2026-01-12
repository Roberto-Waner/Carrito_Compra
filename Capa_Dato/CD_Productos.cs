using Capa_Entidad;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text;

namespace Capa_Datos
{
    public class CD_Productos
    {
        public List<Producto> ListarProductos()
        {
            List<Producto> listarProductos = [];

            try
            {
                using SqlConnection oConexion = Conexion.GetConnection();

                //string query = "";

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("select p.idProducto, p.nombre, p.description,");
                sb.AppendLine("m.idMarca, m.description[Description_Marca],");
                sb.AppendLine("c.idCategoria, c.description[Description_Categoria],");
                sb.AppendLine("p.precio, p.stock, p.rutaImagen, p.nombreImagen, p.activo");
                sb.AppendLine("from PRODUCTO p");
                sb.AppendLine("inner join MARCA m on m.idMarca = p.idMarca");
                sb.AppendLine("inner join CATEGORIA c on c.idCategoria = p.idCategoria");

                SqlCommand cmd = new(sb.ToString(), oConexion);
                cmd.CommandType = CommandType.Text;
                
                oConexion.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    listarProductos.Add(new Producto()
                    {
                        idProducto = Convert.ToInt32(reader["idProducto"]),
                        nombre = reader["nombre"].ToString(),
                        description = reader["description"].ToString(),
                        oMarca = new Marca()
                        {
                            idMarca = Convert.ToInt32(reader["idMarca"]),
                            description = reader["Description_Marca"].ToString()
                        },
                        oCategoria = new Categoria()
                        {
                            idCategoria = Convert.ToInt32(reader["idCategoria"]),
                            description = reader["Description_Categoria"].ToString()
                        },
                        precio = Convert.ToDecimal(reader["precio"], new CultureInfo("en-US")),
                        stock = Convert.ToInt32(reader["stock"]),
                        rutaImagen = reader["rutaImagen"].ToString(),
                        nombreImagen = reader["nombreImagen"].ToString(),
                        activo = Convert.ToBoolean(reader["activo"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar productos: " + ex.Message);
            }

            return listarProductos;
        }

        public int RegistrarProducto(Producto obj, out string mensaje)
        {
            int idAutogenerado = 0;
            mensaje = string.Empty;

            try
            {
                using SqlConnection oConexion = Conexion.GetConnection();
                SqlCommand cmd = new("sp_RegistrarProducto", oConexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("nombre", obj.nombre);
                cmd.Parameters.AddWithValue("description", obj.description);
                cmd.Parameters.AddWithValue("idMarca", obj.oMarca.idMarca);
                cmd.Parameters.AddWithValue("idCategoria", obj.oCategoria.idCategoria);
                cmd.Parameters.AddWithValue("precio", obj.precio);
                cmd.Parameters.AddWithValue("stock", obj.stock);
                cmd.Parameters.AddWithValue("activo", obj.activo);
                cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                oConexion.Open();

                cmd.ExecuteNonQuery();

                idAutogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }
            catch (Exception ex)
            {
                idAutogenerado = 0;
                mensaje = "Error al registrar el Producto: " + ex.Message;
            }

            return idAutogenerado;
        }

        public bool EditarProducto(Producto obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using SqlConnection oConexion = Conexion.GetConnection();
                SqlCommand cmd = new("sp_EditarProducto", oConexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("idProducto", obj.idProducto);
                cmd.Parameters.AddWithValue("description", obj.description);
                cmd.Parameters.AddWithValue("idMarca", obj.oMarca.idMarca);
                cmd.Parameters.AddWithValue("idCategoria", obj.oCategoria.idCategoria);
                cmd.Parameters.AddWithValue("precio", obj.precio);
                cmd.Parameters.AddWithValue("stock", obj.stock);
                cmd.Parameters.AddWithValue("activo", obj.activo);
                cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                oConexion.Open();
                cmd.ExecuteNonQuery();

                resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error al editar el Producto: " + ex.Message;
            }

            return resultado;
        }

        public bool GuardarDatosImagen(Producto obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                string query = "update PRODUCTO set nombreImagen = @nombreImagen, rutaImagen = @rutaImagen where idProducto = @idProducto";

                using SqlConnection oConexion = Conexion.GetConnection();
                SqlCommand cmd = new(query, oConexion)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.AddWithValue("@nombreImagen", obj.nombreImagen);
                cmd.Parameters.AddWithValue("@rutaImagen", obj.rutaImagen);
                cmd.Parameters.AddWithValue("@idProducto", obj.idProducto);

                oConexion.Open();

                if (cmd.ExecuteNonQuery() > 0) resultado = true;
                else mensaje = "No se pudo actualizar la imagen del producto.";
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error al registrar el Producto: " + ex.Message;
            }

            return resultado;
        }
        
        public bool EliminarProducto(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using SqlConnection oConexion = Conexion.GetConnection();
                SqlCommand cmd = new("sp_EliminarProducto", oConexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("idProducto", id);
                cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                oConexion.Open();

                cmd.ExecuteNonQuery();

                resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error al eliminar el Producto: " + ex.Message;
            }

            return resultado;
        }
    }
}
