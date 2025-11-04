using Core.Interfaces;
using Shared.Models;

namespace Servicios.Views
{
    public interface IMovimientoStockView : IViewConMensajes
    {
        void MostrarMovimientos(List<MovimientoStockDto> movimientos);
    }
}