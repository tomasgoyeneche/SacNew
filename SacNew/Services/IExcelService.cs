using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Services
{
    public interface IExcelService
    {
        Task<List<T>> LeerExcelAsync<T>(string filePath, Func<ExcelWorksheet, int, Task<T>> mapFunc);
        Task ExportarAExcelAsync<T>(IEnumerable<T> datos, string filePath, string nombreHoja = "Datos");
    }
}
