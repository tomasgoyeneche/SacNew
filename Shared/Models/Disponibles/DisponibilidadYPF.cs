namespace Shared.Models
{
    public class DisponibilidadYPF
    {
        // public int IdNomina { get; set; }
        public DateTime Fecha { get; set; }

        public string Cuit { get; set; } = string.Empty;

        public int CodTransporte { get; set; }
        public string Transportista { get; set; } = string.Empty;

        public string Unidad { get; set; } = string.Empty;

        public string Chofer { get; set; } = string.Empty;
        public string? Dni { get; set; }
        public string Tractor { get; set; } = string.Empty;
        public string Semi { get; set; } = string.Empty;
        public int Cap { get; set; }
        public string CapCisternas { get; set; } = string.Empty;
        public string Hora { get; set; } = string.Empty;
        public string Etanol { get; set; } = string.Empty;
        public string Producto { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Origen { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public string ObsYPF { get; set; } = string.Empty;
        public string TipoCarga { get; set; } = string.Empty;
    }
}