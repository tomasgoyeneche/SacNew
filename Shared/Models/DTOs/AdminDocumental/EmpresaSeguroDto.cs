namespace Shared.Models
{
    public class EmpresaSeguroDto
    {
        public int idEmpresaSeguro { get; set; }
        public int idEmpresa { get; set; }
        public string? entidad { get; set; }
        public string? cia { get; set; }
        public string? TipoCobertura { get; set; }
        public string? numeroPoliza { get; set; }
        public DateTime certificadoMensual { get; set; }
        public DateTime vigenciaAnual { get; set; }
        public int idEmpresaSeguroEntidad { get; set; }
    }
}