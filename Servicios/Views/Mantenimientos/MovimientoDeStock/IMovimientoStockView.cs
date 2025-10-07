using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Views
{
    public interface IMovimientoStockView : IViewConMensajes
    {
        void MostrarMovimientos(List<MovimientoStockDto> movimientos);
    }
}
