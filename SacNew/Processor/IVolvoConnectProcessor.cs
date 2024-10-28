using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Processor
{
    public interface IVolvoConnectProcessor
    {
        Task<List<ImportVolvoConnect>> ImportarDesdeExcelAsync(string filePath, int idPeriodo);
        Task ExportarAExcelAsync(List<ImportVolvoConnect> datos, string filePath);
    }
}
