namespace Shared.Models
{
    public class EmpresaSeguro
    {
        public int idEmpresaSeguro { get; set; }
        public int idEmpresa { get; set; }
        public int idEmpresaSeguroEntidad { get; set; }
        public int idCia { get; set; }
        public int idCobertura { get; set; }
        public string numeroPoliza { get; set; }
        public DateTime certificadoMensual { get; set; }
        public DateTime vigenciaAnual { get; set; }
    }
}