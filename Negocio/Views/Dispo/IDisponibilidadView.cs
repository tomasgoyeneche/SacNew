using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views
{
    public interface IDisponibilidadView : IViewConMensajes
    {
        DateTime FechaSeleccionada { get; }

        void CargarDisponibilidades(List<Disponibilidad> disponibilidades);

        void ConfigurarControles();

        //void MostrarResumen(List<DispoResumen> resumen);

        void MostrarHistorial(List<HistorialGeneralDto> historial);

        void MostrarVencimientos(List<VencimientosDto> vencimientos);

        void MostrarAlertas(List<AlertaDto> alertas);

        void Close();
    }
}