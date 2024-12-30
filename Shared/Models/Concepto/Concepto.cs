namespace Shared.Models
{
    public class Concepto
    {
        public int IdConsumo { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int IdConsumoTipo { get; set; }  // FK a la tabla de tipo de consumo
        public decimal PrecioActual { get; set; }
        public DateTime Vigencia { get; set; }
        public decimal PrecioAnterior { get; set; }
        public bool Activo { get; set; }
        public int IdUsuario { get; set; }  // FK al usuario que realizó la modificación
        public DateTime FechaModificacion { get; set; }
    }
}