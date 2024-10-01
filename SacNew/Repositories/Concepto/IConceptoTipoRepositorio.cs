using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IConceptoTipoRepositorio
    {
        string ObtenerDescripcionPorId(int idConsumoTipo);

        List<ConceptoTipo> ObtenerTodosLosTipos();
    }
}