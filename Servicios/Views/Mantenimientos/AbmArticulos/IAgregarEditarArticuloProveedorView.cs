using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Views.Mantenimiento
{
    public interface IAgregarEditarArticuloProveedorView : IViewConMensajes
    {
        int? IdArticuloProveedor { get; }
        string RazonSocial { get; }
        string CUIT { get; }
        string? Direccion { get; }
        string? Telefono { get; }
        string? Email { get; }

        void MostrarDatosProveedor(ArticuloProveedor proveedor);
        void Cerrar();
    }
}
