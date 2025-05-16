namespace Shared.Models
{
    public class UnidadMantenimiento
    {
        public int IdUnidadMantenimiento { get; set; }

        public int IdUnidad { get; set; }

        public int IdMantenimientoEstado { get; set; }

        public string Observaciones { get; set; } = string.Empty;

        public int Odometro { get; set; } = 0;

        public DateTime FechaInicio { get; set; } = DateTime.Now;

        public DateTime FechaFin { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;
    }
}