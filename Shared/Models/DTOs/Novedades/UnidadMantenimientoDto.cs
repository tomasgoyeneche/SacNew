namespace Shared.Models
{
    public class UnidadMantenimientoDto
    {
        public int idUnidadMantenimiento { get; set; }

        public int idUnidad { get; set; }

        public string Tractor { get; set; } = string.Empty;
        public string Semi { get; set; } = string.Empty;

        public int idMantenimientoEstado { get; set; }

        public string Descripcion { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;

        public string Abreviado { get; set; } = string.Empty;

        public int Odometro { get; set; } = 0;

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int Dias { get; set; } = 0;
    }
}