using Shared.Models;
using Shared.Models.DTOs;

namespace Core.Repositories
{
    public interface IPOCRepositorio
    {
        Task<List<POCDto>> ObtenerTodosAsync();

        Task<List<POCDto>> BuscarPOCAsync(string criterio, int idPosta);

        Task EliminarPOCAsync(int id);

        Task<POC> ObtenerPorIdAsync(int idPoc);

        Task AgregarPOCAsync(POC poc);

        Task ActualizarPOCAsync(POC poc);

        Task ActualizarFechaCierreYEstadoAsync(int idPoc, DateTime fechaCierre, string estado);

        Task<List<POCDto>> ObtenerTodosPorPostaAsync(int idPosta);

        Task<(string PatenteTractor, decimal CapacidadTanque)> ObtenerUnidadPorPocAsync(int idPoc);

        Task<POC?> ObtenerPorNumeroAsync(string numeroPoc);
        Task<POC?> ObtenerAbiertaPorUnidadAsync(int idUnidad);
        Task<POC?> ObtenerAbiertaPorChoferAsync(int idChofer);
    }
}