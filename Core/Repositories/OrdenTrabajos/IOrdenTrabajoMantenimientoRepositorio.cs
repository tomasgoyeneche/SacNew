using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IOrdenTrabajoMantenimientoRepositorio
    {
        Task<int> AgregarAsync(OrdenTrabajoMantenimiento entidad);
        Task<int> ActualizarAsync(OrdenTrabajoMantenimiento entidad);
        Task<int> EliminarAsync(int id);
        Task<OrdenTrabajoMantenimiento?> ObtenerPorIdAsync(int id);
        Task<List<OrdenTrabajoMantenimiento>> ObtenerPorOrdenTrabajoAsync(int idOrdenTrabajo);
    }
}
