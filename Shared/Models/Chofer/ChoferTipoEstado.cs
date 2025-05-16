namespace Shared.Models
{
    public class ChoferTipoEstado
    {
        public int IdEstado { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public string? Abreviado { get; set; } = string.Empty;
    }
}