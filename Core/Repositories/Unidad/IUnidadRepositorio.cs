using Shared.Models;
using Shared.Models.DTOs;

namespace Core.Repositories
{
    public interface IUnidadRepositorio
    {






        Task<UnidadDto> ObtenerPorIdDtoAsync(int idUnidad);

        Task<Unidad> ObtenerPorUnidadIdAsync(int idUnidad);

     

        Task<List<UnidadDto>> ObtenerUnidadesDtoAsync();

        Task<List<Unidad>> ObtenerUnidadesAsync();

        // Obtener Por Otras Opciones
        Task<int?> ObtenerIdTractorPorPatenteAsync(string patente);

        Task<int?> ObtenerIdUnidadPorTractorAsync(int idTractor);

        Task<List<NominaMetanolActivaDto>> ObtenerNominaMetanolActiva();

        // Actualizar, Editar, Eliminar
        Task ActualizarVencimientoUnidadAsync(int idUnidad, int idTipoVencimiento, DateTime fechaActualizacion, int idUsuario);

        Task EliminarUnidadAsync(int idUnidad);

        Task AgregarUnidadAsync(Unidad unidad, int idUsuario);
    }
}