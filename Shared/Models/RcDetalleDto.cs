namespace Shared.Models
{
    public class RcDetalleDto
    {
        public int IdRc { get; set; }              // Identificador del requerimiento
        public string? Proveedor { get; set; }      // Nombre del proveedor
        public DateTime Fecha { get; set; }        // Fecha del requerimiento
        public string? EmitidoPor { get; set; }     // Usuario que emitió el requerimiento
        public string? FuncionEmitido { get; set; } // Función del usuario que emitió
        public string? AprobadoPor { get; set; }    // Usuario que aprobó el requerimiento
        public string? FuncionAprobado { get; set; } // Función del usuario que aprobó
        public string? EntregaLugar { get; set; }   // Lugar de entrega
        public string? EntregaFecha { get; set; }   // Fecha de entrega
        public string? Importe { get; set; }        // Importe asociado
        public string? CondicionPago { get; set; }  // Condiciones de pago
        public string? Observaciones { get; set; }  // Observaciones generales
    }
}