using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Processor
{
    public interface IImportacionYpfProcessor
    {
        Task<List<ImportConsumoYpfEnRuta>> ImportarConsumosDesdeExcelAsync(string filePath, int idPeriodo);
        Task ExportarConsumosAExcelAsync(IEnumerable<ImportConsumoYpfEnRuta> consumos, string filePath);
    }
}
