using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.Informes
{
    public interface IConsumoUnidadView
    {
        DateTime PeriodoDesde { get; }
        int QuincenaDesde { get; }

        DateTime PeriodoHasta { get; }
        int QuincenaHasta { get; }

        void MostrarConsumos(List<InformeConsumoUnidad> consumos);

        List<InformeConsumoUnidad> ObtenerConsumos();

        void MostrarMensaje(string mensaje);
    }
}