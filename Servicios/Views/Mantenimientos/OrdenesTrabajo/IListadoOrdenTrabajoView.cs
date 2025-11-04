using Core.Interfaces;
using Shared.Models;

namespace Servicios.Views.Mantenimientos
{
    public interface IListadoOrdenTrabajoView : IViewConMensajes
    {
        void MostrarOrdenesDeTrabajo(List<OrdenTrabajoDto> ordenes);
    }
}