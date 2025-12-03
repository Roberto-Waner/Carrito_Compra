namespace Capa_Entidad
{
    public class Carrito
    {
        public int idCarrito { get; set; }
        public Cliente oCliente { get; set; }
        public Producto oProducto { get; set; }
        public int cantidad { get; set; }
    }
}
