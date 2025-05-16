namespace Shared.Models
{
    public class Alerta
    {
        public int IdAlerta { get; set; }

        public int? IdChofer { get; set; }

        public int? IdTractor { get; set; }

        public int? IdSemi { get; set; }

        public string? Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int IdUsuario { get; set; }

        public bool Activo { get; set; }
    }
}