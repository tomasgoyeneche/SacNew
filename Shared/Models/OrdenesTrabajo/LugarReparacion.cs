namespace Shared.Models
{
    public class LugarReparacion
    {
        public int IdLugarReparacion { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}