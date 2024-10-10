using SacNew.Models;

namespace SacNew.Interfaces
{
    public interface IAgregarProductoView
    {
        int ProductoSeleccionado { get; }

        void CargarProductos(List<Producto> productos);

        void MostrarMensaje(string mensaje);
    }
}