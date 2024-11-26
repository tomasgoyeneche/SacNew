using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IConceptoTipoRepositorio
    {
        Task<string> ObtenerDescripcionPorIdAsync(int idConsumoTipo);

        Task<List<ConceptoTipo>> ObtenerTodosLosTiposAsync();
    }
}