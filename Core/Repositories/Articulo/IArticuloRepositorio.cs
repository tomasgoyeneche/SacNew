using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IArticuloRepositorio
    {
        Task<List<Articulo>> ObtenerArticulosActivosAsync();

        Task<Articulo?> ObtenerPorIdAsync(int idArticulo);

        Task<int> AgregarArticuloAsync(Articulo nuevoArticulo);

        Task<int> ActualizarArticuloAsync(Articulo articuloActualizado);

        Task<int> EliminarArticuloAsync(int idArticulo);
    }
}
