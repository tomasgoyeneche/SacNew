using Shared.Models;

namespace Core.Repositories
{
    public interface IConsumoGasoilRepositorio
    {
        Task<List<ConsumoGasoil>> ObtenerPorPOCAsync(int idPoc);

        Task AgregarConsumoAsync(ConsumoGasoil consumoGasoil);

        Task EliminarConsumoAsync(int idConsumoGasoil);

        Task<ConsumoGasoil?> ObtenerPorIdAsync(int idConsumoGasoil);

        Task<(int IdPrograma, decimal Kilometros)?> ObtenerProgramaPorPatenteAsync(string patenteTractor, DateTime FechaCreacion);

        Task<decimal> ObtenerLitrosCargadosPorProgramaAsync(int idPrograma);

        Task<List<ConsumoGasoilAutorizadoDto>> ObtenerConsumosPorProgramaAsync(int idPrograma, string patente);

        Task<List<ConsumoGasoilAutorizadoDto>> ObtenerConsumosPorProgramaEditableAsync(int idConsumo, int idPrograma, string patente);

        Task<int?> ObtenerIdProgramaAnteriorAsync(string patente, int idProgramaActual);

        Task<ConsumoGasoilAutorizadoDto?> ObtenerUltimoConsumoPorPatenteAsync(string patente);

        Task ActualizarConsumoAsync(ConsumoGasoil consumo);

        Task<List<ConsumoGasoilAutorizadoDto>> ObtenerConsumosUltimosDosMesesAsync(string patente, int idProgramaActual);
    }
}