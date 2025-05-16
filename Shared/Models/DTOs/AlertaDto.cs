namespace Shared.Models
{
    public class AlertaDto
    {
        public int IdAlerta { get; set; }

        public string? PatenteTractor { get; set; }

        public string? PatenteSemi { get; set; }

        public string? NombreCompletoChofer { get; set; }

        public string? Descripcion { get; set; }

        public string? Usuario { get; set; }

        public int IdNomina { get; set; }

        public DateTime Fecha { get; set; }

        public bool Activo { get; set; }
    }
}