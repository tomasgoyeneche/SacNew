namespace Shared.Models
{
    public class ProductoSinonimo
    {
        public int IdSinonimo { get; set; }
        public int IdProducto { get; set; }
        public string Sinonimo { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public int IdUsuario { get; set; } = 0; // Usuario que creó el sinónimo
        public DateTime FechaModificacion { get; set; } = DateTime.Now; // Fecha de creación del sinónimo
    }
}