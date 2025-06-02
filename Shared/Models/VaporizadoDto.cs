namespace Shared.Models
{
    public class VaporizadoDto
    {
        public int IdVaporizado { get; set; }

        public string Empresa { get; set; } = string.Empty;
        public string Tractor { get; set; } = string.Empty;

        public string Semi { get; set; } = string.Empty;

        public string Motivo { get; set; } = string.Empty;

        public int Cisternas { get; set; } = 0;
        public int NumeroCertificado { get; set; } = 0;
        public int RemitoDanes { get; set; } = 0;
        public DateTime? Inicio { get; set; }
        public DateTime? Fin { get; set; }

        public string? Tiempo { get; set; } = string.Empty;
        public string? Zona { get; set; } = string.Empty;

        public string? Externo { get; set; } = string.Empty;
        public string? Estado { get; set; } = string.Empty;
        public int TipoIngreso { get; set; } = 0;
        public int IdPosta { get; set; } = 0;
        public int IdGuardiaIngreso { get; set; } = 0;
    }
}