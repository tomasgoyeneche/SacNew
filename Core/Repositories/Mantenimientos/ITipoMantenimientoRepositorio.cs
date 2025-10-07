using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITipoMantenimientoRepositorio
    {
        Task<List<TipoMantenimiento>> ObtenerTodosAsync();
    }
}
