using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Views.Mantenimiento
{
    public interface IMenuArticuloView : IViewConMensajes
    {
        void MostrarArticulos(List<Articulo> articulos);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}
