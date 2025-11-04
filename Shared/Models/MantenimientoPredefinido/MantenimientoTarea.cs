namespace Shared.Models
{
    public class MantenimientoTarea
    {
        public int IdMantenimientoTarea { get; set; }
        public int IdMantenimiento { get; set; }
        public int IdTarea { get; set; }
        public bool Activo { get; set; } = true;
    }
}