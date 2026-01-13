namespace Shared.Models.RequerimientoCompra
{
    public class RcRcc
    {
        public int IdRc { get; set; }
        public DateTime Fecha { get; set; }

        public int? IdProveedor { get; set; }
        public int? Emitido { get; set; }
        public int? Aprobado { get; set; }

        public string? EntregaLugar { get; set; }
        public string? EntregaFecha { get; set; }
        public string? Importe { get; set; }
        public string? CondicionPago { get; set; }
        public string? Observaciones { get; set; }

        public int IdEstado { get; set; }
    }
}