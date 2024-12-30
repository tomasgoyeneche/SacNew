using Core.Interfaces;
using Shared.Models;

namespace SacNew.Views.Configuraciones.AbmLocaciones
{
    public interface IAgregarEditarLocacionPoolView : IViewConMensajes
    {
        void CargarLocacionesDisponibles(IEnumerable<Locacion> locaciones);

        void CargarLocacionesAsignadas(IEnumerable<Locacion> locaciones);

        Locacion LocacionSeleccionadaDisponible { get; }
        Locacion LocacionSeleccionadaAsignada { get; }
    }
}