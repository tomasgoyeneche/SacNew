using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IConceptoRepositorio
    {
        List<Concepto> ObtenerTodosLosConceptos();

        List<Concepto> BuscarConceptos(string textoBusqueda);

        Concepto ObtenerPorId(int idConsumo);

        void AgregarConcepto(Concepto concepto);

        void ActualizarConcepto(Concepto concepto);

        void EliminarConcepto(int idConsumo);
    }
}