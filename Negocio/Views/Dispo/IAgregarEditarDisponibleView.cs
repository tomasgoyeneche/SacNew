using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views
{
    public interface IAgregarEditarDisponibleView : IViewConMensajes
    {
        void CargarOrigenes(List<Locacion> origenes);
        void CargarDestinos(List<Locacion> destinos);
        void CargarCupos(List<int> cupos);
        void MostrarDisponible(Disponible disponible);
        void MostrarAusenciasChofer(string texto);
        void MostrarMantenimientosUnidad(string texto);

        void CargarProductos(List<Producto> productos);
        Disponible ObtenerDisponible();
        void Cerrar();
    }
}
