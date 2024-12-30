using Shared.Models;

namespace GestionFlota.Processor
{
    public interface IImportacionMercadoVictoriaProcessor
    {
        Task<List<ImportMercadoVictoria>> ImportarDesdeExcelAsync(string filePath, int idPeriodo);

        Task ExportarConsumosAExcelAsync(List<ImportMercadoVictoria> consumos, string filePath);
    }
}