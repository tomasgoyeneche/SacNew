using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public interface ILocacionKilometrosEntreRepositorio
    {
        Task<List<LocacionKilometrosEntre>> ObtenerPorLocacionIdAsync(int idLocacion);
        Task AgregarAsync(LocacionKilometrosEntre locacionKilometrosEntre);
        Task EliminarAsync(int idKilometros);
    }
}
