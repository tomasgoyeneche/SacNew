using Shared.Models;
using Shared.Models.DTOs;

namespace Core.Repositories
{
    public interface IConsumoOtrosRepositorio
    {
        Task AgregarConsumoAsync(ConsumoOtros consumoOtros);

        Task EliminarConsumoAsync(int idConsumoOtros);

        Task ActualizarConsumoAsync(ConsumoOtros consumo);

        Task<ConsumoOtros> ObtenerPorIdAsync(int idConsumoOtros);

        Task<List<ConsumosUnificadosDto>> ObtenerPorPocAsync(int idPoc);

        Task<List<InformeConsumoPocDto>> BuscarConsumosAsync(
                int? idConcepto,
                int? idPosta,
                int? idEmpresa,
                int? idUnidad,
                int? idChofer,
                string numeroPOC,
                string estado,
                DateTime? fechaCreacionDesde,
                DateTime? fechaCreacionHasta,
                DateTime? fechaCierreDesde,
                DateTime? fechaCierreHasta);
    }
}