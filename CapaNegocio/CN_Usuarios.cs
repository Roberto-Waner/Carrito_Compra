using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            if(string.IsNullOrEmpty(obj.nombres) || string.IsNullOrWhiteSpace(obj.nombres)) mensaje = "El nombre del usuario no puede estar vacío.";
            else if(string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos)) mensaje = "El apellido del usuario no puede estar vacío.";
            else if(string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo)) mensaje = "El correo del usuario no puede estar vacío.";

            if(string.IsNullOrEmpty(mensaje)) 
            {
                string clave = CN_Recursos.GenerarClave();

                string code = $"{clave}";

                string asunto = "Creación de Cuenta";

                string menssaje_correo = $@"
                    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px; background-color: #f9f9f9; color: #333;'>
                        <h2 style='text-align: center; color: #667db6;'>✨ ¡Recupera tu Contraseña! ✨</h2>
                        <p style='font-size: 16px; line-height: 1.6;'>
                            Hola, hemos recibido una solicitud para restablecer tu contraseña. Si no has solicitado esto, puedes ignorar este correo. 
                            De lo contrario, sigue las instrucciones a continuación. 
                        </p>
                        <hr>
                        <p style='text-align: center; font-size: 16px;'>
                            <strong>No responder a este correo.</strong>
                        </p>
                        <hr>
                        <p style='font-size: 14px; text-align: center; color: #888;'>
                            Copia y pega el siguiente enlace en tu navegador:
                        </p>
                        <div style='background-color: #f1f1f1; padding: 10px; border: 1px dashed #ccc; border-radius: 5px; text-align: center;'>
                            <span id='resetLink' style='word-break: break-all; font-size: 14px; color: #555;'>{code}</span>
                        </div>
                        <p style='font-size: 12px; text-align: center; margin-top: 20px; color: #888;'>
                            ⚠ Este enlace es válido por 1 hora. Si expira, solicita un nuevo enlace desde la App en la pestaña de recuperación de contraseña.
                        </p>
                        <p style='text-align: center;'>
                            <strong>💡 Consejo:</strong> Mantén presionado el enlace y selecciona <i>Copiar</i> en tu móvil o haz clic derecho en tu computadora.
                        </p>
                    </div>
                ";

                bool resp = CN_Recursos.EnviarCorreo(obj.correo, asunto, menssaje_correo);

                if(resp)
                {
                    obj.clave = CN_Recursos.ConvertirSha256(clave);

                    return objCapaDato.Registrar(obj, out mensaje);
                }
                else
                {
                    mensaje = "No se pudo enviar el correo electrónico.";
                    return 0;
                }

            }
            else return 0;
        }

        public bool Editar(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombres) || string.IsNullOrWhiteSpace(obj.nombres)) mensaje = "El nombre del usuario no puede estar vacío.";
            else if (string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos)) mensaje = "El apellido del usuario no puede estar vacío.";
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo)) mensaje = "El correo del usuario no puede estar vacío.";

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDato.Editar(obj, out mensaje);
            }
            else return false;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objCapaDato.Eliminar(id, out mensaje);
        }
    }
}
