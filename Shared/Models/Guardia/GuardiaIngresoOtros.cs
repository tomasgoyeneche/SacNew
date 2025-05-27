namespace Shared.Models
{
    public class GuardiaIngresoOtros
    {
        public int IdGuardiaIngresoOtros { get; set; }

        public string Documento { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public DateTime? Licencia { get; set; } = DateTime.Now;
        public DateTime? Art { get; set; } = DateTime.Now;

        public string Tipo { get; set; } = string.Empty;

        public string Patente { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}