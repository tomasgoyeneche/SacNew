using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IOrdenTrabajoRepositorio
    {
        Task<int> AgregarAsync(OrdenTrabajo orden);
        Task<int> ActualizarAsync(OrdenTrabajo orden);
        Task<int> EliminarAsync(int idOrdenTrabajo);
        Task<OrdenTrabajo?> ObtenerPorIdAsync(int idOrdenTrabajo);
        Task<List<OrdenTrabajo>> ObtenerTodosAsync();



        Task<List<OrdenTrabajoDto>> ObtenerTodosDtoAsync();
        Task<List<OrdenTrabajoDto>> ObtenerPorFaseAsync(string fase);
        Task<List<OrdenTrabajo>> ObtenerPorNominaAsync(int idNomina);
    }
}
