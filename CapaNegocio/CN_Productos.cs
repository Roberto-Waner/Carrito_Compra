using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Productos
    {
        private CD_Productos objCDProductos = new CD_Productos();

        public List<Producto> Listar()
        {
            return objCDProductos.ListarProductos();
        }

        public int Registrar(Producto obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre)) mensaje = "El nombre del producto no puede estar vacío.";
            else if (string.IsNullOrEmpty(obj.description) || string.IsNullOrWhiteSpace(obj.description)) mensaje = "La descripción del producto no puede estar vacía.";
            else if (obj.oMarca == null || obj.oMarca.idMarca <= 0) mensaje = "Debe seleccionar una marca válida.";
            else if (obj.oCategoria == null || obj.oCategoria.idCategoria <= 0) mensaje = "Debe seleccionar una categoría válida.";
            else if (obj.precio <= 0) mensaje = "El precio del producto no puede ser menor a cero y debe ingresar el precio.";
            else if (obj.stock == 0) mensaje = "Debe ingresar el stock del producto.";

            if (string.IsNullOrEmpty(mensaje)) return objCDProductos.RegistrarProducto(obj, out mensaje);
            else return 0;
        }

        public bool Editar(Producto obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre)) mensaje = "El nombre del producto no puede estar vacío.";
            else if (string.IsNullOrEmpty(obj.description) || string.IsNullOrWhiteSpace(obj.description)) mensaje = "La descripción del producto no puede estar vacía.";
            else if (obj.oMarca == null || obj.oMarca.idMarca <= 0) mensaje = "Debe seleccionar una marca válida.";
            else if (obj.oCategoria == null || obj.oCategoria.idCategoria <= 0) mensaje = "Debe seleccionar una categoría válida.";
            else if (obj.precio <= 0) mensaje = "El precio del producto no puede ser menor a cero y debe ingresar el precio.";
            else if (obj.stock == 0) mensaje = "Debe ingresar el stock del producto.";

            if (string.IsNullOrEmpty(mensaje)) return objCDProductos.EditarProducto(obj, out mensaje);
            else return false;
        }

        public bool GuardarImagen(Producto obj, out string mensaje)
        {
            return objCDProductos.GuardarDatosImagen(obj, out mensaje);
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objCDProductos.EliminarProducto(id, out mensaje);
        }
    }
}
