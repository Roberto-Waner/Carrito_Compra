using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Marcas
    {
        private CD_Marcas objCDMarcas = new CD_Marcas();

        public List<Marca> Listar()
        {
            return objCDMarcas.ListarMarcas();
        }

        public int Registrar(Marca obj, out string mensaje)
        {
            mensaje = string.Empty;
            if(string.IsNullOrEmpty(obj.description) || string.IsNullOrWhiteSpace(obj.description)) mensaje = "La descripción de la marca no puede estar vacía.";
            if(string.IsNullOrEmpty(mensaje)) return objCDMarcas.RegistrarMarca(obj, out mensaje);
            else return 0;
        }

        public bool Editar(Marca obj, out string mensaje)
        {
            mensaje = string.Empty;
            if(string.IsNullOrEmpty(obj.description) || string.IsNullOrWhiteSpace(obj.description)) mensaje = "La descripción de la marca no puede estar vacía.";
            if(string.IsNullOrEmpty(mensaje)) return objCDMarcas.EditarMarca(obj, out mensaje);
            else return false;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objCDMarcas.EliminarMarca(id, out mensaje);
        }
    }
}
