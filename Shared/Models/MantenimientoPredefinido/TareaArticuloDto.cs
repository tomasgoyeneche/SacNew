namespace Shared.Models
{
    public class TareaArticuloDto
    {
        public int? IdOrdenTrabajoArticulo { get; set; }
        public int IdArticulo { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal => PrecioUnitario * Cantidad;

        public bool Dolar { get; set; }
        public string? Estado { get; set; } = string.Empty;
    }
}