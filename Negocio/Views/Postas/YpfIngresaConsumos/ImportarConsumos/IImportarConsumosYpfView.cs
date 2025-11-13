using Core.Interfaces;
using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.ImportarConsumos
{
    public interface IImportarConsumosYpfView : IViewConMensajes
    {
        List<ImportConsumoYpfEnRuta> ObtenerConsumos();

        DateTime PeriodoSeleccionado { get; }

        void MostrarConsumos(List<ImportConsumoYpfEnRuta> consumos);
    }
}