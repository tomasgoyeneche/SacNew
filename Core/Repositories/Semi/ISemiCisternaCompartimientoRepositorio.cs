using Shared.Models;

namespace Core.Repositories.Semi
{
    public interface ISemiCisternaCompartimientoRepositorio
    {
        Task EliminarCompartimientoAsync(int idCompartimiento);

        Task AgregarCompartimientoAsync(SemiCisternaCompartimiento compartimiento);

        Task<List<SemiCisternaCompartimiento>> ObtenerCompartimientosActivosAsync(int idSemi);
    }
}