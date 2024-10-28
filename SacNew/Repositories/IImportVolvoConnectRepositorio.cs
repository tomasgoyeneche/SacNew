using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public interface IImportVolvoConnectRepositorio
    {
        Task AgregarImportacionAsync(ImportVolvoConnect importacion);
        Task<bool> ExistenDatosParaPeriodoAsync(int idPeriodo);
        Task<List<ImportVolvoConnect>> ObtenerPorPeriodoAsync(int idPeriodo);
    }
}
