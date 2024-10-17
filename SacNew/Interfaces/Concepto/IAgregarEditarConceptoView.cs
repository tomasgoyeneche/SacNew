using SacNew.Models;

namespace SacNew.Interfaces
{
    public interface IAgregarEditarConceptoView : IViewConMensajes
    {
        int Id { get; set; }
        string Codigo { get; }
        string Descripcion { get; }
        int IdTipoConsumo { get; }
        decimal PrecioActual { get; }
        decimal PrecioAnterior { get; }
        DateTime Vigencia { get; }

        int IdProveedorBahiaBlanca { get; }
        int IdProveedorPlazaHuincul { get; }

        void CargarTiposDeConsumo(List<ConceptoTipo> tiposDeConsumo);

        void CargarProveedoresBahiaBlanca(List<Proveedor> proveedores);

        void CargarProveedoresPlazaHuincul(List<Proveedor> proveedores);

        void MostrarDatosConcepto(Concepto concepto);
    }
}