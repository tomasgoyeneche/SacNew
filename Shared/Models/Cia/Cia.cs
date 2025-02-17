namespace Shared.Models
{
    public class Cia
    {
        public int IdCia { get; set; }
        public string NombreFantasia { get; set; } = string.Empty;
        public int IdTipoCia { get; set; }
        public bool Activo { get; set; }
    }
}