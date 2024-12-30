namespace Shared.Models
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Codigo { get; set; }
        public string RazonSocial { get; set; }
        public int NumeroFicha { get; set; }
        public bool Activo { get; set; }
    }
}