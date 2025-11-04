namespace Shared.Models
{
    public class ArticuloMarca
    {
        public int IdArticuloMarca { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;
    }
}