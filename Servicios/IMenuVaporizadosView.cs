using Core.Interfaces;
using Shared.Models;

namespace Servicios
{
    public interface IMenuVaporizadosView : IViewConMensajes
    {
        void MostrarVaporizados(List<VaporizadoDto> vaporizados);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}