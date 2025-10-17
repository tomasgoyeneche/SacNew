using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IOrdenTrabajoTareaRepositorio
    {
        Task<int> AgregarAsync(OrdenTrabajoTarea entidad);
        Task<int> ActualizarAsync(OrdenTrabajoTarea entidad);
        Task<int> EliminarAsync(int id);
        Task<OrdenTrabajoTarea?> ObtenerPorIdAsync(int id);
        Task<List<OrdenTrabajoTarea>> ObtenerPorMantenimientoAsync(int idOrdenTrabajoMantenimiento);
    }
}
