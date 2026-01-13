using Core.Interfaces;
using Shared.Models;

namespace Servicios.Views.Mantenimiento
{
    public interface IAgregarEditarArticuloProveedorView : IViewConMensajes
    {
        int? IdArticuloProveedor { get; }
        string RazonSocial { get; }
        string NombreFantasia { get; }

        string CUIT { get; }
        string? Direccion { get; }
        string? Telefono { get; }
        string? Email { get; }

        void MostrarDatosProveedor(ArticuloProveedor proveedor);

        void Cerrar();
    }
}