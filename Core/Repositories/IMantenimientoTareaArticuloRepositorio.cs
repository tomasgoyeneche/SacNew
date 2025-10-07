using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IMantenimientoTareaArticuloRepositorio
    {
        Task<int> AgregarAsync(MantenimientoTareaArticulo mantenimientoTareaArticulo);
        Task<int> EliminarAsync(int idMantenimientoTareaArticulo);
        Task<List<MantenimientoTareaArticulo>> ObtenerPorTareaAsync(int idTarea);
    }
}
