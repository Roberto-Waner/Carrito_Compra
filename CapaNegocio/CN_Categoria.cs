using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Categoria
    {
        private CD_Categoria objCDCategoria = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return objCDCategoria.ListarCategorias();
        }

        public int Registrar(Categoria obj, out string mensaje)
        {
            mensaje = string.Empty;

            if(string.IsNullOrEmpty(obj.description) || string.IsNullOrWhiteSpace(obj.description)) mensaje = "La descripción de la categoría no puede estar vacía.";

            if(string.IsNullOrEmpty(mensaje)) return objCDCategoria.RegistrarCategoria(obj, out mensaje);
            else return 0;
        }

        public bool Editar(Categoria obj, out string mensaje)
        {
            mensaje = string.Empty;

            if(string.IsNullOrEmpty(obj.description) || string.IsNullOrWhiteSpace(obj.description)) mensaje = "La descripción de la categoría no puede estar vacía.";

            if(string.IsNullOrEmpty(mensaje)) return objCDCategoria.EditarCategoria(obj, out mensaje);
            else return false;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objCDCategoria.EliminarCategoria(id, out mensaje);
        }
    }
}
