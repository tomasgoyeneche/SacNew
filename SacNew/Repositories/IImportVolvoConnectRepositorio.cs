using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IImportVolvoConnectRepositorio
    {
        Task AgregarImportacionAsync(ImportVolvoConnect importacion);

        Task<bool> ExistenDatosParaPeriodoAsync(int idPeriodo);

        Task<List<ImportVolvoConnect>> ObtenerPorPeriodoAsync(int idPeriodo);
    }
}