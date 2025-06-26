using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views
{
    public interface IAsignarCargaView : IViewConMensajes
    {
        void CargarOrigenes(List<Locacion> origenes, int? idOrigenSeleccionado);

        void CargarDestinos(List<Locacion> destinos, int? idDestinoSeleccionado);

        void CargarProductos(List<Producto> productos, int? idProductoSeleccionado);

        int? IdOrigenSeleccionado { get; }
        int? IdDestinoSeleccionado { get; }
        int? IdProductoSeleccionado { get; }

        void Cerrar();
    }
}