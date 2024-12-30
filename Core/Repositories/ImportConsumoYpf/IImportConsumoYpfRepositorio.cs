using Shared.Models;

namespace Core.Repositories
{
    public interface IImportConsumoYpfRepositorio
    {
        Task AgregarConsumoAsync(ImportConsumoYpfEnRuta consumo);

        Task<IEnumerable<ImportConsumoYpfEnRuta>> ObtenerPorPeriodoAsync(int idPeriodo);

        Task EliminarConsumosPorPeriodoAsync(int idPeriodo);

        Task<bool> ExistenConsumosParaPeriodoAsync(int idPeriodo);
    }
}