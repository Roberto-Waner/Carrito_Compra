namespace Capa_Entidad
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string? nombre { get; set; }
        public string? description { get; set; }
        public Marca? oMarca { get; set; }
        public Categoria? oCategoria { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public string? rutaImagen { get; set; }
        public string? nombreImagen { get; set; }
        public bool activo { get; set; }

        public string? PrecioText { get; set; }
        public string? Base64 { get; set; }
        public string? Extension { get; set; }
    }
}
