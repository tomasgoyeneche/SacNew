using CsvHelper.Configuration;

namespace Core.Services
{
    public interface ICsvService
    {
        Task ExportarACsvAsync<T>(IEnumerable<T> datos, string filePath, bool incluirEncabezados = true);

        Task ExportarFilasSeparadasAsync(IEnumerable<string[]> filas, string filePath);

        Task<List<T>> LeerCsvAsync<T>(string filePath, bool tieneEncabezados = true);

        Task<List<T>> LeerCsvConMapeoAsync<T, TMap>(string filePath) where TMap : ClassMap<T>;

        Task ProcesarCsvLineaPorLineaAsync(string filePath, Func<dynamic, Task> procesarLinea, bool tieneEncabezados = true);
    }
}