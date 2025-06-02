namespace Shared.Models
{
    public class VaporizadoMotivo
    {
        public int IdVaporizadoMotivo { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;
    }
}