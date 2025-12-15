using Capa_Entidad;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_Datos
{
    public class CD_Usuarios
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection oConexion = Conexion.GetConnection())
                {
                    oConexion.Open();

                    string query = "SELECT idUsuario, nombres, apellidos, correo, clave, reestablecer, activo FROM USUARIO";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(
                                    new Usuario()
                                    {
                                        idUsuario = Convert.ToInt32(reader["idUsuario"]),
                                        nombres = reader["nombres"].ToString(),
                                        apellidos = reader["apellidos"].ToString(),
                                        correo = reader["correo"].ToString(),
                                        clave = reader["clave"].ToString(),
                                        reestablecer = Convert.ToBoolean(reader["reestablecer"]),
                                        activo = Convert.ToBoolean(reader["activo"])
                                    }
                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios: " + ex.Message);
            }

            return lista;
        }

        public int Registrar(Usuario obj, out string mensaje)
        {
            int idAutogenerado = 0;
            mensaje = string.Empty;

            try
            {
                using(SqlConnection oConexion = Conexion.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("correo", obj.correo);
                    cmd.Parameters.AddWithValue("clave", obj.clave);
                    cmd.Parameters.AddWithValue("activo", obj.activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    idAutogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idAutogenerado = 0;
                mensaje = "Error al registrar usuario: " + ex.Message;
            }

            return idAutogenerado;
        }

        public bool Editar(Usuario obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using(SqlConnection oConexion = Conexion.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("idUsuario", obj.idUsuario);
                    cmd.Parameters.AddWithValue("nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("correo", obj.correo);
                    cmd.Parameters.AddWithValue("activo", obj.activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error al editar usuario: " + ex.Message;
            }

            return resultado;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using(SqlConnection oConexion = Conexion.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from USUARIO where idUsuario = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error al eliminar usuario: " + ex.Message;
            }

            return resultado;
        }
    }
}