namespace Shared.Models
{
    public class OrdenTrabajoTarea
    {
        public int IdOrdenTrabajoTarea { get; set; }
        public int IdOrdenTrabajoMantenimiento { get; set; }

        public int? IdTarea { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public decimal? ManoObra { get; set; }
        public decimal? Horas { get; set; }
        public decimal? TotalEstimado { get; set; }
        public decimal? TotalEstimadoUsd { get; set; }

        public bool Dolar { get; set; } = false;
        public bool Activo { get; set; } = true;
    }
}