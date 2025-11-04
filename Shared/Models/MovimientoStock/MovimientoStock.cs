namespace Shared.Models
{
    public class MovimientoStock
    {
        public int IdMovimientoStock { get; set; }
        public int IdTipoMovimiento { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public bool Autorizado { get; set; }
        public string? Observaciones { get; set; }
        public bool Activo { get; set; }
    }
}