using Core.Interfaces;
using Shared.Models;

namespace GestionOperativa.Views
{
    public interface IEliminarEventoView : IViewConMensajes
    {
        void MostrarHistorial(List<GuardiaHistorialDto> historial);

        void Close();
    }
}