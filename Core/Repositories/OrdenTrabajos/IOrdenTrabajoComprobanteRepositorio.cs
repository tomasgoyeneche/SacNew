using Shared.Models;

namespace Core.Repositories
{
    public interface IOrdenTrabajoComprobanteRepositorio
    {
        Task<List<OrdenTrabajoComprobante>> ObtenerPorMovimientoAsync(int idOrdenTrabajo);

        Task<int> AgregarAsync(OrdenTrabajoComprobante comprobante);

        Task ActualizarAsync(OrdenTrabajoComprobante comprobante);

        Task<TipoComprobante?> ObtenerTiposComprobantesPorId(int idTipoComprobante);

        Task<List<TipoComprobante>> ObtenerTiposComprobantes();

        Task<OrdenTrabajoComprobante?> ObtenerPorIdAsync(int idOrdenTrabajoComprobante);

        Task EliminarAsync(int idOrdenTrabajoComprobante);
    }
}