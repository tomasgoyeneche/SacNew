using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo
{
    public interface IOtrosConsumosView : IViewConMensajes
    {
        Concepto TipoConsumoSeleccionado { get; }
        string RemitoExterno { get; }
        DateTime FechaRemito { get; }
        decimal? Cantidad { get; }
        string Aclaraciones { get; }

        void CargarTiposConsumo(List<Concepto> tiposConsumo);

        void MostrarTotalCalculado(decimal total);

        void InicializarParaEdicion(ConsumoOtros consumo);

        void Cerrar();
    }
}