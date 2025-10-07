using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Views.Mantenimiento
{
    public interface IMenuArticuloProveedorView : IViewConMensajes
    {
        void MostrarProveedores(List<ArticuloProveedor> proveedores);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}
