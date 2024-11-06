using SacNew.Models;
using SacNew.Models.DTOs;

namespace SacNew.Repositories.Chofer
{
    public interface IChoferRepositorio
    {
        Task<int?> ObtenerIdPorDocumentoAsync(string documento);

        Task<List<chofer>> ObtenerTodosLosChoferes();
    }
}