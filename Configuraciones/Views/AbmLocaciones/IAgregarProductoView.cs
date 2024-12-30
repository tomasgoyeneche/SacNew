using Core.Interfaces;
using Shared.Models;

namespace SacNew.Interfaces
{
    public interface IAgregarProductoView : IViewConMensajes
    {
        int ProductoSeleccionado { get; }

        void CargarProductos(List<Producto> productos);
    }
}