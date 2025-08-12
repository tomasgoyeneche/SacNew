namespace Shared.Models
{
    public class UnidadMantenimientoEstado
    {
        public int IdMantenimientoEstado { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public string? Abreviado { get; set; } = string.Empty;
        public string? Detalle { get; set; } = string.Empty;


        public bool Activo { get; set; } = true;
    }
}