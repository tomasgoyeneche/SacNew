using Shared.Models;
using Shared.Models.DTOs;

namespace Core.Repositories
{
    public interface IUnidadRepositorio
    {
        // Obtener Unidad Por Id
        Task<List<UnidadPatenteDto>> ObtenerUnidadesPatenteDtoAsync();

        Task<UnidadDto> ObtenerPorIdDtoAsync(int idUnidad);

        Task<UnidadPatenteDto?> ObtenerPorIdAsync(int idUnidad);

        Task<List<UnidadDto>> ObtenerUnidadesDtoAsync();

        // Obtener Por Otras Opciones
        Task<int?> ObtenerIdTractorPorPatenteAsync(string patente);

        Task<int?> ObtenerIdUnidadPorTractorAsync(int idTractor);

        Task<List<NominaMetanolActivaDto>> ObtenerNominaMetanolActiva();

        // Actualizar, Editar, Eliminar
        Task ActualizarVencimientoUnidadAsync(int idUnidad, int idTipoVencimiento, DateTime fechaActualizacion, int idUsuario);

        Task EliminarUnidadAsync(int idUnidad);

        Task AgregarUnidadAsync(Unidad unidad);
    }
}