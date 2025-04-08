using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo
{
    public interface IOtrosConsumosView : IViewConMensajes
    {
        int TipoConsumoSeleccionado { get; }
        string RemitoExterno { get; }
        DateTime FechaRemito { get; }
        decimal? Cantidad { get; }

        decimal? PrecioManual { get; }
        string Aclaraciones { get; }

        bool Dolar { get; }

        void CargarTiposConsumo(List<Concepto> tiposConsumo, string poc);

        void MostrarTotalCalculado(decimal total);

        void InicializarParaEdicion(ConsumoOtros consumo);

        void Cerrar();
    }
}