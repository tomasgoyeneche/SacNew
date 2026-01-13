using Shared.Models;
using Shared.Models.RequerimientoCompra;

namespace Core.Repositories.RequerimientoCompra
{
    public interface IRcRepositorio
    {
        Task<int> ObtenerProximoNumeroAsync();

        Task<RcRcc?> ObtenerPorIdAsync(int idRc);

        Task<int> AgregarAsync(RcRcc rc);

        Task ActualizarAsync(RcRcc rc);

        Task<List<Dependencia>> ObtenerDependenciasAsync();

        Task<List<Imputacion>> ObtenerImputacionesPorDependenciaAsync(int idDependencia);

        Task InsertarRcImputacionAsync(int idRc, int idImputacion, int porcentaje);

        Task InsertarRcDetalleAsync(int idRc, string descripcion, int cantidad);

        Task<List<RcDetalleRcc>> ObtenerDescripcionesRcAsync(int idRc);

        Task<List<RcImputacionDetalleDto>> ObtenerImputacionesRcAsync(int idRc);

        Task<RcDetalleDto?> ObtenerDetalleRcAsync(int idRc);
    }
}