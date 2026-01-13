namespace Shared.Models
{
    public class RcImputacionDetalleDto
    {
        public int IdRc { get; set; }              // Identificador del requerimiento
        public string? Imputacion { get; set; }     // Detalles de la imputación

        public string? Porcentaje { get; set; }
    }
}