using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IArticuloMarcaRepositorio
    {
        Task<List<ArticuloMarca>> ObtenerTodasAsync();
        Task<ArticuloMarca?> ObtenerPorIdAsync(int id);
        Task<int> AgregarAsync(ArticuloMarca marca);
        Task<int> ActualizarAsync(ArticuloMarca marca);
        Task<int> EliminarAsync(int id);
    }
}
