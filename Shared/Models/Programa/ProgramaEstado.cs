namespace Shared.Models
{
    public class ProgramaEstado
    {
        public int IdProgramaEstado { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}