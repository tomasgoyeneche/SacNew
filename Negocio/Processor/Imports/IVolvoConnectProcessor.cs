using Shared.Models;

namespace GestionFlota.Processor
{
    public interface IVolvoConnectProcessor
    {
        Task<List<ImportVolvoConnect>> ImportarDesdeExcelAsync(string filePath, int idPeriodo);

        Task ExportarAExcelAsync(List<ImportVolvoConnect> datos, string filePath);
    }
}