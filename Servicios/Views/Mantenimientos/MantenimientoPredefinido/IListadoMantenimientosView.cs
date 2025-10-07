using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Views.Mantenimientos
{
    public interface IListadoMantenimientosView : IViewConMensajes
    {
        void MostrarMantenimientos(List<Shared.Models.Mantenimiento> movimientos);
    }
}
