namespace Shared.Models
{
    public class ChoferEstado
    {
        public int IdEstadoChofer { get; set; }

        public int IdChofer { get; set; }

        public int IdEstado { get; set; }

        public string? Observaciones { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public bool Disponible { get; set; }
        public bool Activo { get; set; }
    }
}