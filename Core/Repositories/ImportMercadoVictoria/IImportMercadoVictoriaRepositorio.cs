using Shared.Models;

namespace Core.Repositories
{
    public interface IImportMercadoVictoriaRepositorio
    {
        Task<List<ImportMercadoVictoria>> ObtenerPorPeriodoAsync(int idPeriodo);

        Task AgregarConsumoAsync(ImportMercadoVictoria consumo);

        Task<bool> ExistenDatosParaPeriodoAsync(int idPeriodo);
    }
}