namespace Shared.Models
{
    public class MovimientoStockDto
    {
        public int IdMovimientoStock { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string Autorizado { get; set; } = string.Empty; // "Sí" o "No"
        public string? Observaciones { get; set; }
    }
}