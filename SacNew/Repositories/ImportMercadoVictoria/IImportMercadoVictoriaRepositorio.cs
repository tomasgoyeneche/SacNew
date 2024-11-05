using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IImportMercadoVictoriaRepositorio
    {
        Task<List<ImportMercadoVictoria>> ObtenerPorPeriodoAsync(int idPeriodo);

        Task AgregarConsumoAsync(ImportMercadoVictoria consumo);

        Task<bool> ExistenDatosParaPeriodoAsync(int idPeriodo);
    }
}