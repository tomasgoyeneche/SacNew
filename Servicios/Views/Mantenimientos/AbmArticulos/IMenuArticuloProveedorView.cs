using Core.Interfaces;
using Shared.Models;

namespace Servicios.Views.Mantenimiento
{
    public interface IMenuArticuloProveedorView : IViewConMensajes
    {
        void MostrarProveedores(List<ArticuloProveedor> proveedores);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}