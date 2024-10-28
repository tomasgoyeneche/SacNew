using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public interface IUnidadRepositorio
    {
        Task<int?> ObtenerIdTractorPorPatenteAsync(string patente);
        Task<int?> ObtenerIdUnidadPorTractorAsync(int idTractor);
    }
}
