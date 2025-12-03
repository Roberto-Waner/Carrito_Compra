namespace Capa_Entidad
{
    public class Detalle_Venta
    {
        public int idDetalleVenta { get; set; }
        public int idVenta { get; set; }
        public Producto oProducto { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public string idTransaccion { get; set; }
    }
}
