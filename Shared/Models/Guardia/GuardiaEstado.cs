namespace Shared.Models
{
    public class GuardiaEstado
    {
        public int IdGuardiaEstado { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;
    }
}