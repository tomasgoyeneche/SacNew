namespace Shared.Models.RequerimientoCompra
{
    public class ProveedorRcc
    {
        public int IdProveedor { get; set; }
        public string Cuit { get; set; } = string.Empty;
        public string NombreFantasia { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}