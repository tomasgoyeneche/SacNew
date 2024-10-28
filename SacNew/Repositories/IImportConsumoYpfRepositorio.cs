using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public interface IImportConsumoYpfRepositorio
    {
        Task AgregarConsumoAsync(ImportConsumoYpfEnRuta consumo);
        Task<IEnumerable<ImportConsumoYpfEnRuta>> ObtenerPorPeriodoAsync(int idPeriodo);
        Task EliminarConsumosPorPeriodoAsync(int idPeriodo);

        Task<bool> ExistenConsumosParaPeriodoAsync(int idPeriodo);

    }
}
