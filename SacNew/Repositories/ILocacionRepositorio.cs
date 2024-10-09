using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public interface ILocacionRepositorio
    {
        Task<List<Locacion>> ObtenerTodasAsync();
        Task<List<Locacion>> BuscarPorCriterioAsync(string criterio);
        Task EliminarAsync(int idLocacion);
        Task<Locacion> ObtenerPorIdAsync(int idLocacion);

        Task AgregarAsync(Locacion locacion);
        Task ActualizarAsync(Locacion locacion);
      
    }
}
