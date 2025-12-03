namespace Capa_Entidad
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public bool reestablecer { get; set; }
        public bool activo { get; set; }
    }
}
