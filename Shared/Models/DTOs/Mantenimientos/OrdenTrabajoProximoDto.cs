namespace Shared.Models
{
    public class OrdenTrabajoProximoDto
    {
        public int IdUnidad { get; set; }
        public string PatenteTractor { get; set; } = string.Empty;
        public string PatenteSemi { get; set; } = string.Empty;
        public int IdMantenimiento { get; set; }
        public string NombreMantenimiento { get; set; } = string.Empty;
        public string TipoIntervalo { get; set; } = string.Empty;
        public DateTime? UltimaFecha { get; set; }
        public decimal? UltimoOdometro { get; set; }
        public int? DiasIntervalo { get; set; }
        public int? KilometrosIntervalo { get; set; }
        public int? DiasRestantes { get; set; }
        public decimal? KmRestantes { get; set; }
        public DateTime? FechaProximoMantenimiento { get; set; }
        public decimal? OdometroProximoMantenimiento { get; set; }
        public decimal? OdometerActual { get; set; }
    }
}