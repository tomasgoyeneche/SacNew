namespace Shared.Models
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        public string Codigo { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal? ManoObra { get; set; }
        public decimal? Horas { get; set; }
        public bool Activo { get; set; } = true;
    }
}