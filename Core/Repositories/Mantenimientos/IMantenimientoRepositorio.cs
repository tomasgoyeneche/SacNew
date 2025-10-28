using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IMantenimientoRepositorio
    {
        Task<int> AgregarAsync(Mantenimiento mantenimiento);
        Task<int> ActualizarAsync(Mantenimiento mantenimiento);
        Task<int> EliminarAsync(int idMantenimiento);
        Task<Mantenimiento?> ObtenerPorIdAsync(int idMantenimiento);
        Task<List<Mantenimiento>> ObtenerTodosAsync();

    }
}
