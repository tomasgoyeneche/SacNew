using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IConceptoRepositorio
    {
        Task<List<Concepto>> ObtenerTodosLosConceptosAsync();

        Task<List<Concepto>> ObtenerPorTipoAsync(int idTipoConsumo);

        Task<List<Concepto>> BuscarConceptosAsync(string textoBusqueda);

        Task<Concepto?> ObtenerPorIdAsync(int idConsumo);

        Task AgregarConceptoAsync(Concepto concepto);

        Task ActualizarConceptoAsync(Concepto concepto);

        Task EliminarConceptoAsync(int idConsumo);
    }
}