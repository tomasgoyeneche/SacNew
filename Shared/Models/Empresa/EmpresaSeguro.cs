namespace Shared.Models
{
    public class EmpresaSeguro
    {
        public int IdSeguroEmpresa { get; set; }
        public int IdEmpresa { get; set; }
        public int IdCia { get; set; }
        public int IdCobertura { get; set; }
        public string NumeroPoliza { get; set; } = string.Empty;
        public DateTime VigenciaHasta { get; set; }
        public DateTime PagoDesde { get; set; }
        public DateTime PagoHasta { get; set; }
    }
}