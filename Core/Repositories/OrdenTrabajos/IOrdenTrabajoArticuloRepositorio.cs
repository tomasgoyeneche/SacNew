using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IOrdenTrabajoArticuloRepositorio
    {
        Task<int> AgregarAsync(OrdenTrabajoArticulo entidad);
        Task<int> ActualizarAsync(OrdenTrabajoArticulo entidad);
        Task<int> EliminarAsync(int id);
        Task<OrdenTrabajoArticulo?> ObtenerPorIdAsync(int id);
        Task<List<OrdenTrabajoArticulo>> ObtenerPorTareaAsync(int idOrdenTrabajoTarea);
    }
}
