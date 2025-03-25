using Shared.Models;

namespace Core.Repositories
{
    public interface ISemiCisternaTipoCargaRepositorio
    {
        Task<List<SemiCisternaTipoCarga>> ObtenerTiposCargaAsync();
    }
}