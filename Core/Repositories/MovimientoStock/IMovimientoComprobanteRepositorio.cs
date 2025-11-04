using Shared.Models;

namespace Core.Repositories
{
    public interface IMovimientoComprobanteRepositorio
    {
        Task<List<MovimientoComprobante>> ObtenerPorMovimientoAsync(int idMovimientoStock);

        Task<int> AgregarAsync(MovimientoComprobante comprobante);

        Task ActualizarAsync(MovimientoComprobante comprobante);

        Task<TipoComprobante?> ObtenerTiposComprobantesPorId(int idTipoComprobante);

        Task<List<TipoComprobante>> ObtenerTiposComprobantes();

        Task<MovimientoComprobante?> ObtenerPorIdAsync(int idMovimientoComprobante);

        Task EliminarAsync(int idMovimientoComprobante);
    }
}