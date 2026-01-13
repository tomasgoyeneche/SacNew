using Core.Interfaces;
using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.ImportarConsumos
{
    public interface IImportarConsumosYpfView : IViewConMensajes
    {
        List<ImportConsumoYpfEnRutaDto> ObtenerConsumos();

        DateTime PeriodoSeleccionado { get; }
        int QuincenaSeleccionada { get; }

        void MostrarConsumos(List<ImportConsumoYpfEnRutaDto> consumos);
    }
}