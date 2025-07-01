using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views
{
    public interface IPedidosView : IViewConMensajes
    {
        void CargarPedidos(List<PedidoDto> pedidos);
        DialogResult ConfirmarEliminacion(string mensaje);
    }
}
