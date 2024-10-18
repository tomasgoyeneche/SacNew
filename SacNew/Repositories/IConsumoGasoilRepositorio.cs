using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IConsumoGasoilRepositorio
    {
        Task<List<ConsumoGasoil>> ObtenerPorPOCAsync(int idPoc);

        Task AgregarConsumoAsync(ConsumoGasoil consumoGasoil);

        Task EliminarConsumoAsync(int idConsumoGasoil);

        Task<ConsumoGasoil> ObtenerPorIdAsync(int idConsumoGasoil);
    }
}