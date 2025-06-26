namespace Shared.Models
{
    public class DisponibleEstado
    {
        public int IdDisponibleEstado { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}