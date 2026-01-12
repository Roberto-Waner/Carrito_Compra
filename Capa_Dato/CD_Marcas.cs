using Capa_Entidad;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_Datos
{
    public class CD_Marcas
    {
        public List<Marca> ListarMarcas()
        {
            // Simplificación de la inicialización de la colección y uso de 'new()' para el tipo conocido
            List<Marca> listarMarcas = [];

            try
            {
                using SqlConnection oConexion = Conexion.GetConnection();
                oConexion.Open();

                string query = "select idMarca, description, activo from MARCA";
                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.CommandType = CommandType.Text;

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Reemplaza la asignación potencialmente nula usando el operador null-coalescing para evitar CS8601
                    listarMarcas.Add(new Marca()
                    {
                        idMarca = Convert.ToInt32(reader["idMarca"]),
                        description = reader["description"]?.ToString() ?? string.Empty,
                        activo = Convert.ToBoolean(reader["activo"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar marcas: " + ex.Message);
            }

            return listarMarcas;
        }

        public int RegistrarMarca(Marca obj, out string mensaje)
        {
            int idAutogenerado = 0;
            mensaje = string.Empty;

            try
            {
                using SqlConnection oConexion = Conexion.GetConnection();
                SqlCommand cmd = new("sp_RegistrarMarca", oConexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("description", obj.description);
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
                mensaje = "Error al registrar la marca: " + ex.Message;
            }

            return idAutogenerado;
        }

        public bool EditarMarca(Marca obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using SqlConnection oConexion = Conexion.GetConnection();
                SqlCommand cmd = new("sp_EditarMarca", oConexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("idMarca", obj.idMarca);
                cmd.Parameters.AddWithValue("description", obj.description);
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
                mensaje = "Error al editar la marca: " + ex.Message;
            }

            return resultado;
        }

        public bool EliminarMarca(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using SqlConnection oConexion = Conexion.GetConnection();
                SqlCommand cmd = new("sp_EliminarMarca", oConexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("idMarca", id);
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
                mensaje = "Error al eliminar la marca: " + ex.Message;
            }

            return resultado;
        }
    }
}
