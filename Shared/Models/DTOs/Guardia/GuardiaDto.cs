namespace Shared.Models
{
    public class GuardiaDto
    {
        public int IdGuardiaIngreso { get; set; }

        public int TipoIngreso { get; set; }
        public int IdPosta { get; set; }
        public int IdEntidad { get; set; }

        public string Empresa { get; set; } = string.Empty;
        public string Tractor { get; set; } = string.Empty;
        public string Semi { get; set; } = string.Empty;
        public string Chofer { get; set; } = string.Empty;

        public DateTime Ingreso { get; set; } = DateTime.Now;

        public int IdEstadoEvento { get; set; }
        public string Evento { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; } = string.Empty;
    }
}