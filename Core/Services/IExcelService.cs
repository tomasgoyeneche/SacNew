using OfficeOpenXml;

namespace Core.Services
{
    public interface IExcelService
    {
        Task<List<T>> LeerExcelAsync<T>(
         string filePath,
         Func<ExcelWorksheet, int, Task<T>> mapFunc,
         string? sheetName = null);

        Task ExportarAExcelAsync<T>(IEnumerable<T> datos, string filePath, string nombreHoja = "Datos");
    }
}