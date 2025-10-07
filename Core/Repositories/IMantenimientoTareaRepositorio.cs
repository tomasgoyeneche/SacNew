using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IMantenimientoTareaRepositorio
    {
        Task<int> AgregarAsync(MantenimientoTarea mantenimientoTarea);
        Task<int> EliminarAsync(int idMantenimientoTarea);
        Task<List<MantenimientoTarea>> ObtenerPorMantenimientoAsync(int idMantenimiento);
    }
}
