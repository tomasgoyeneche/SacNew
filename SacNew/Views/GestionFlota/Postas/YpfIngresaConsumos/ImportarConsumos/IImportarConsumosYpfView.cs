using SacNew.Interfaces;
using SacNew.Models;

namespace SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.ImportarConsumos
{
    public interface IImportarConsumosYpfView : IViewConMensajes
    {
        void CargarPeriodos(List<Periodo> periodos);

        Periodo? PeriodoSeleccionado { get; }

        List<ImportConsumoYpfEnRuta> ObtenerConsumos();

        void MostrarConsumos(List<ImportConsumoYpfEnRuta> consumos);
    }
}