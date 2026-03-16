using Shared.Models;

namespace GestionFlota.Views.Postas.Informes.ConsultarConsumos
{
    public interface IResultadosConsumoView
    {
        void MostrarResultados(List<InformeConsumoPocDto> resultados, bool exportaTransoft);

        void MostrarTotales(List<TotalConsumoDto> totales);

        void MostrarMensaje(string mensaje);

        List<InformeConsumoPocDto> ObtenerResultados();
        void MarcarRegistrosValorizados(List<string> claves);
        bool ConfirmarValorizacion(string mensaje);
    }
}