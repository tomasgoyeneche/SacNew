using Shared.Models;

namespace Core.Repositories
{
    public interface IConsumoUnidadRepositorio
    {
        Task<List<InformeConsumoUnidad>> ObtenerConsumosPorPeriodoAsync(int idPeriodo);

        Task GuardarConsumoAsync(InformeConsumoUnidad consumo);
    }
}