using Capa_Dato;
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
                string clave = "test123";
                obj.clave = CN_Recursos.ConvertirSha256(clave);

                return objCapaDato.Registrar(obj, out mensaje);
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
