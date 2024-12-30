using Shared.Models;

namespace GestionFlota.Processor
{
    public interface IImportacionYpfProcessor
    {
        Task<List<ImportConsumoYpfEnRuta>> ImportarConsumosDesdeExcelAsync(string filePath, int idPeriodo);

        Task ExportarConsumosAExcelAsync(IEnumerable<ImportConsumoYpfEnRuta> consumos, string filePath);
    }
}