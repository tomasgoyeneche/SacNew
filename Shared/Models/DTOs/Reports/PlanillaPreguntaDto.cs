namespace Shared.Models.DTOs
{
    public class PlanillaPreguntaDto
    {
        public int IdPlanilla { get; set; }

        public int IdPregunta { get; set; }

        public int IdUnidadTipo { get; set; }

        public string? DescripcionUnidadTipo { get; set; }

        public string? Texto { get; set; }

        public int IdPreguntaRespuestaTipo { get; set; }

        public string? Orden { get; set; }

        public bool Conforme { get; set; }

        public bool NoConforme { get; set; }

        public string? Observaciones { get; set; }

        public bool EsEncabezado { get; set; }
    }
}