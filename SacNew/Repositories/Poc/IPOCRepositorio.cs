using SacNew.Models;
using SacNew.Models.DTOs;

namespace SacNew.Repositories
{
    public interface IPOCRepositorio
    {
        Task<List<POCDto>> ObtenerTodosAsync();

        Task<List<POCDto>> BuscarPOCAsync(string criterio);

        Task EliminarPOCAsync(int id);

        Task<POC> ObtenerPorIdAsync(int idPoc);

        Task AgregarPOCAsync(POC poc);

        Task ActualizarPOCAsync(POC poc);
    }
}