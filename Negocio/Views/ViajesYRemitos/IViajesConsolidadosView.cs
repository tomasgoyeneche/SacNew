using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views
{
    public interface IViajesConsolidadosView : IViewConMensajes
    {
        void CargarProgramas(List<VistaProgramaGridDto> programas);

        void MostrarMensaje(string mensaje);

        void MostrarRemitosFaltantes(int faltanCarga, int faltanEntrega);
    }
}