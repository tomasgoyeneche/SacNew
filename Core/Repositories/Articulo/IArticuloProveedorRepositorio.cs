using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IArticuloProveedorRepositorio
    {
        Task<int> AgregarAsync(ArticuloProveedor proveedor);
        Task<int> ActualizarAsync(ArticuloProveedor proveedor);
        Task<int> EliminarAsync(int idProveedor);
        Task<ArticuloProveedor?> ObtenerPorIdAsync(int idProveedor);
        Task<List<ArticuloProveedor>> ObtenerTodosAsync();
    }
}
