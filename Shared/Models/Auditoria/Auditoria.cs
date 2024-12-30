namespace Shared.Models
{
    public class Auditoria
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string? TablaModificada { get; set; }
        public string? Accion { get; set; }
        public DateTime FechaHora { get; set; }
        public int RegistroModificadoId { get; set; }
        public string? ValoresAnteriores { get; set; }
        public string? ValoresNuevos { get; set; }
    }
}