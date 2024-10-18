using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IConceptoRepositorio
    {
        List<Concepto> ObtenerTodosLosConceptos();

        Task<IEnumerable<Concepto>> ObtenerPorTipoAsync(int idTipoConsumo);

        List<Concepto> BuscarConceptos(string textoBusqueda);

        Concepto ObtenerPorId(int idConsumo);

        void AgregarConcepto(Concepto concepto);

        void ActualizarConcepto(Concepto concepto);

        void EliminarConcepto(int idConsumo);
    }
}