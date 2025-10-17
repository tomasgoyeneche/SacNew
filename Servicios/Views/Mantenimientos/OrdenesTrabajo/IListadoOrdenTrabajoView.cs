using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Views.Mantenimientos
{
    public interface IListadoOrdenTrabajoView : IViewConMensajes
    {
        void MostrarOrdenesDeTrabajo(List<OrdenTrabajoDto> ordenes);
    }
}
