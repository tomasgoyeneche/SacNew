using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IVaporizadoMotivoRepositorio
    {
        Task<List<VaporizadoMotivo>> ObtenerTodosAsync();
    }
}
