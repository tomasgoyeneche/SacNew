using Core.Interfaces;

namespace Servicios.Views.Mantenimientos
{
    public interface IListadoMantenimientosView : IViewConMensajes
    {
        void MostrarMantenimientos(List<Shared.Models.Mantenimiento> movimientos);
    }
}