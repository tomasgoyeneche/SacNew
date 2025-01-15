using Shared.Models;
using Shared.Models.DTOs;

namespace Core.Repositories
{
    public interface IConsumoOtrosRepositorio
    {
        Task AgregarConsumoAsync(ConsumoOtros consumoOtros);

        Task EliminarConsumoAsync(int idConsumoOtros);

        Task<ConsumoOtros> ObtenerPorIdAsync(int idConsumoOtros);

        Task<List<ConsumosUnificadosDto>> ObtenerPorPocAsync(int idPoc);
    }
}