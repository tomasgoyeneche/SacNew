using Shared.Models;

namespace Core.Repositories
{
    public interface ITractorRepositorio
    {
        // Obtener Tractor Por Id
        Task<List<TractorDto>> ObtenerTodosLosTractoresDto();

        Task<List<Tractor>> ObtenerTodosLosTractores();

        Task<List<TractorDto>> BuscarTractoresAsync(string textoBusqueda);

        Task<TractorDto> ObtenerPorIdDtoAsync(int idTractor);

        Task<Tractor> ObtenerTractorPorIdAsync(int idTractor);

        // Obtener Por Otras Opciones

        Task<List<Tractor>> ObtenerTractoresLibresAsync();

        // Actualizar, Editar, Eliminar

        Task AltaTractorAsync(string patente, int idUsuario);

        Task ActualizarTractorAsync(Tractor tractor);

        Task EliminarTractorAsync(int idTractor);

        Task ActualizarEmpresaTractorAsync(int idTractor, int idEmpresa);

        Task ActualizarVencimientoTractorAsync(int idTractor, int idVencimiento, DateTime fechaActualizacion, int idUsuario);
    }
}