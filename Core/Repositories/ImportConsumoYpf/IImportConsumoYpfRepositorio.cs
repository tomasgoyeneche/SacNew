using Shared.Models;

namespace Core.Repositories
{
    public interface IImportConsumoYpfRepositorio
    {
        Task<int> AgregarConsumoAsync(ImportConsumoYpfEnRuta consumo);

        Task<IEnumerable<ImportConsumoYpfEnRuta>> ObtenerPorPeriodoAsync(int idPeriodo);

        Task EliminarConsumosPorPeriodoAsync(int idPeriodo);

        Task<int> ActualizarConsumoAsync(ImportConsumoYpfEnRuta consumo);
        Task<bool> ExistenConsumosParaPeriodoAsync(int idPeriodo);
    }
}