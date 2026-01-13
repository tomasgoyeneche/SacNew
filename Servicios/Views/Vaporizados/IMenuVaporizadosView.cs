using Core.Interfaces;
using Shared.Models;

namespace Servicios
{
    public interface IMenuVaporizadosView : IViewConMensajes
    {
        void MostrarVaporizados(List<VaporizadoDto> vaporizados);

        void CargarGuardiasPasadas(List<GuardiaDto> empresas);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}