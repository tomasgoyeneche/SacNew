using Core.Interfaces;
using Shared.Models;

namespace Servicios.Views.Mantenimiento
{
    public interface IMenuArticuloView : IViewConMensajes
    {
        void MostrarArticulos(List<Articulo> articulos);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}