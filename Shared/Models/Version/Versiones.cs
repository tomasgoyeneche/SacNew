namespace Shared.Models
{
    public class Versiones
    {
        public int Id { get; set; }
        public string NumeroVersion { get; set; } = string.Empty;
        public DateTime FechaPublicacion { get; set; }
        public string? Notas { get; set; }
        public bool Activo { get; set; }
    }
}