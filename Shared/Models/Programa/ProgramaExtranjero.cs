namespace Shared.Models
{
    public class ProgramaExtranjero
    {
        public int IdProgramaExtranjero { get; set; }
        public int IdPrograma { get; set; }
        public int IdProgramaTipoPunto { get; set; }
        public int IdProgramaTipoEvento { get; set; }
        public decimal Odometro { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
    }
}