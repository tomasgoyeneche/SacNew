namespace Shared.Models
{
    public class TransitoEspecialEmpresaDto
    {
        public string RazonSocial { get; set; } = string.Empty;
        public string Cuit { get; set; } = string.Empty;

        public string Display => $"{RazonSocial} - {Cuit}";
    }
}