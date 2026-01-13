namespace Shared.Models.RequerimientoCompra
{
    public class RcDetalleRcc
    {
        public int IdRcDetalle { get; set; } // solo para BD
        public int IdRc { get; set; }         // se setea al guardar
        public string Descripcion { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public bool Activo { get; set; } = true;
    }
}