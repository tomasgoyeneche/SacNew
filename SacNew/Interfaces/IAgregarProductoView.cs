using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Interfaces
{
    public interface IAgregarProductoView
    {
        int ProductoSeleccionado { get; }
        void CargarProductos(List<Producto> productos);
        void MostrarMensaje(string mensaje);
    }
}
