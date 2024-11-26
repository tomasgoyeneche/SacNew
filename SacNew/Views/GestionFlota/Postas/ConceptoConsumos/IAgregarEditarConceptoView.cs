using SacNew.Interfaces;
using SacNew.Models;

namespace SacNew.Views.GestionFlota.Postas.ConceptoConsumos
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

        Task CargarTiposDeConsumoAsync(List<ConceptoTipo> tiposDeConsumo);

        Task CargarProveedoresBahiaBlancaAsync(List<Proveedor> proveedores);

        Task CargarProveedoresPlazaHuinculAsync(List<Proveedor> proveedores);

        void MostrarDatosConcepto(Concepto concepto);
    }
}