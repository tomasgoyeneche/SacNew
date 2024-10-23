using SacNew.Interfaces;
using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Views.Configuraciones.AbmLocaciones
{
    public interface IAgregarEditarLocacionPoolView : IViewConMensajes
    {
        void CargarLocacionesDisponibles(IEnumerable<Locacion> locaciones);
        void CargarLocacionesAsignadas(IEnumerable<Locacion> locaciones);
        Locacion LocacionSeleccionadaDisponible { get; }
        Locacion LocacionSeleccionadaAsignada { get; }
    }
}
