using Shared.Models;

namespace Core.Repositories
{
    public interface IUnidadMantenimientoRepositorio
    {
        Task<List<UnidadMantenimientoDto>> ObtenerNovedadesDto();

        Task<List<UnidadMantenimientoDto>> ObtenerTodasLasNovedadesDto();

        Task<List<UnidadMantenimientoEstado>> ObtenerEstados();

        Task AltaNovedadAsync(UnidadMantenimiento unidadMantenimiento, int idUsuario);

        Task EditarNovedadAsync(UnidadMantenimiento unidadMantenimiento, int idUsuario);

        Task EliminarNovedadAsync(UnidadMantenimientoDto unidadMantenimiento, int idUsuario);

        Task<List<UnidadMantenimientoDto>> ObtenerPorMesYAnioAsync(int mes, int anio);

        Task<List<UnidadMantenimientoDto>> ObtenerPorAnioAsync(int anio);

        Task<List<UnidadMantenimientoDto>> ObtenerPorUnidadAsync(int idUnidad);
    }
}