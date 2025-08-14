using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views
{
    public interface IRuteoView : IViewConMensajes
    {
        void MostrarRuteoCargados(List<Shared.Models.Ruteo> cargados);

        void MostrarRuteoVacios(List<Shared.Models.Ruteo> vacios);

        void MostrarResumen(List<RuteoResumen> resumen);

        void MostrarHistorial(List<HistorialGeneralDto> historial);

        void MostrarVencimientos(List<VencimientosDto> vencimientos);

        void MostrarChoferesLibres(List<ChoferesLibresDto> choferesLibres);

        void MostrarAlertas(List<AlertaDto> alertas);

        void SetEstadoCargaDisponibles(bool cargando);
        void SeleccionarRuteoCargadoPorId(int idPrograma);

        void SeleccionarRuteoPorNomina(int idNomina);
    }
}