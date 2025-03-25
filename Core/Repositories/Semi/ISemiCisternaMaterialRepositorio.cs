using Shared.Models;

namespace Core.Repositories
{
    public interface ISemiCisternaMaterialRepositorio
    {
        Task<List<SemiCisternaMaterial>> ObtenerMaterialesAsync();
    }
}