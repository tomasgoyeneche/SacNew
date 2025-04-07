using Shared.Models;

namespace GestionFlota.Views.Postas.Informes.ConsultarConsumos
{
    public interface IResultadosConsumoView
    {
        void MostrarResultados(List<InformeConsumoPocDto> resultados);

        void MostrarTotales(List<TotalConsumoDto> totales);

        void MostrarMensaje(string mensaje);

        List<InformeConsumoPocDto> ObtenerResultados();
    }
}