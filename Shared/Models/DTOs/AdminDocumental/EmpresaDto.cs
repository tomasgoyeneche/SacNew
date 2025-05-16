namespace Shared.Models
{
    public class EmpresaDto
    {
        public int IdEmpresa { get; set; }                  // Identificador único de la empresa
        public string? EmpresaTipo { get; set; }             // Tipo de empresa (e.g., proveedor, cliente)
        public string? RazonSocial { get; set; }             // Razón social de la empresa
        public string? NombreFantasia { get; set; }          // Nombre comercial de la empresa
        public string? Cuit { get; set; }                    // Número de CUIT de la empresa
        public string? Ubicacion { get; set; }               // Ubicación general de la empresa
        public string? Domicilio { get; set; }               // Dirección específica de la empresa
        public string? Telefono { get; set; }                // Teléfono de contacto
        public string? Email { get; set; }                   // Correo electrónico de contacto
    }
}