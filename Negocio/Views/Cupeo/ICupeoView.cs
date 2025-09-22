using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views
{
    public interface ICupeoView : IViewConMensajes
    {
        void MostrarCupeoDisp(List<Shared.Models.Cupeo> cargados);

        void MostrarCupeoAsignados(List<Shared.Models.Cupeo> vacios);

        //void MostrarResumen(List<CupeoResumen> resumen);

        void MostrarHistorial(List<HistorialGeneralDto> historial);

        void MostrarVencimientos(List<VencimientosDto> vencimientos);

        void MostrarAlertas(List<AlertaDto> alertas);


        void SeleccionarCupeoAsignadosPorId(int idNomina);
        void SeleccionarCupeoDispPorNomina(int idNomina);
    }
}