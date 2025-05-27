namespace Shared.Models
{
    public class GuardiaHistorialDto
    {
        public int IdGuardiaIngreso { get; set; }

        public int IdGuardiaRegistro { get; set; }

        public int IdGuardiaEstado { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;

        public DateTime FechaGuardia { get; set; } = DateTime.Now;
    }
}