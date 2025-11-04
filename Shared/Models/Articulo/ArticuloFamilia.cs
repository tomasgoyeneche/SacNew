namespace Shared.Models
{
    public class ArticuloFamilia
    {
        public int IdArticuloFamilia { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public bool Activo { get; set; } = true;
    }
}