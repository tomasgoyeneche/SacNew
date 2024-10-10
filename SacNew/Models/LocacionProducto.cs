namespace SacNew.Models
{
    public class LocacionProducto
    {
        public int IdLocacionProducto { get; set; }
        public int IdLocacion { get; set; }
        public int IdProducto { get; set; }

        // Relación con el producto
        public Producto Producto { get; set; }
    }
}