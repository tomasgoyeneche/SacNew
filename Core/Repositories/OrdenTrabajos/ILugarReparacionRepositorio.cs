using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ILugarReparacionRepositorio
    {
        Task<int> AgregarAsync(LugarReparacion entidad);
        Task<int> ActualizarAsync(LugarReparacion entidad);
        Task<int> EliminarAsync(int id);
        Task<LugarReparacion?> ObtenerPorIdAsync(int id);
        Task<List<LugarReparacion>> ObtenerTodosAsync();
    }
}
