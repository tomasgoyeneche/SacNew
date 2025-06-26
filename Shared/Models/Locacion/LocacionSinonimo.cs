namespace Shared.Models
{
    public class LocacionSinonimo
    {
        public int IdSinonimo { get; set; }
        public int IdLocacion { get; set; }
        public string Sinonimo { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}