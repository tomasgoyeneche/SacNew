using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.Informes
{
    public interface IConsumoUnidadView
    {
        int IdPeriodoSeleccionado { get; }

        void CargarPeriodos(List<Periodo> periodos);

        void MostrarConsumos(List<InformeConsumoUnidad> consumos);

        List<InformeConsumoUnidad> ObtenerConsumos();

        void MostrarMensaje(string mensaje);
    }
}