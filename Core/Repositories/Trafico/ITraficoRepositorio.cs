using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITraficoRepositorio
    {
        Task<Trafico?> ObtenerPorIdAsync(int idTrafico);
        Task<List<Trafico>> ObtenerTodosAsync();
        Task<List<Trafico>> BuscarAsync(string textoBusqueda);

        Task AgregarAsync(Trafico trafico);
        Task ActualizarAsync(Trafico trafico);
        Task EliminarAsync(int idTrafico);
    }
}
