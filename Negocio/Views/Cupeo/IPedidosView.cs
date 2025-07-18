using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views
{
    public interface IPedidosView : IViewConMensajes
    {
        void CargarPedidos(List<PedidoDto> pedidos);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}