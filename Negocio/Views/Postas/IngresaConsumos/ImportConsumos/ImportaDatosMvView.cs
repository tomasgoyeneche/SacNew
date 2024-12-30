using Core.Interfaces;
using Shared.Models;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.ImportConsumos
{
    public interface ImportaDatosMvView : IViewConMensajes
    {
        void CargarPeriodos(List<Periodo> periodos);

        Periodo? PeriodoSeleccionado { get; }

        List<ImportMercadoVictoria> ObtenerConsumos();

        void MostrarConsumos(List<ImportMercadoVictoria> consumos);
    }
}