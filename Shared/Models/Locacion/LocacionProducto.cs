namespace Shared.Models
{
    public class LocacionProducto
    {
        public int IdLocacionProducto { get; set; }
        public int IdLocacion { get; set; }
        public int IdProducto { get; set; }
        public Producto? Producto { get; set; }
    }
}