namespace Shared.Models
{
    public class GuardiaRegistro
    {
        public int IdGuardiaRegistro { get; set; }
        public int IdGuardiaIngreso { get; set; }
        public int IdGuardiaEstado { get; set; }
        public string Observaciones { get; set; } = string.Empty;

        public int IdUsuario { get; set; }
        public DateTime FechaGuardia { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;
    }
}