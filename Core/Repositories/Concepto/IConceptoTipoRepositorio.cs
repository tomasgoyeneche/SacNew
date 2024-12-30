using Shared.Models;

namespace Core.Repositories
{
    public interface IConceptoTipoRepositorio
    {
        Task<string> ObtenerDescripcionPorIdAsync(int idConsumoTipo);

        Task<List<ConceptoTipo>> ObtenerTodosLosTiposAsync();
    }
}