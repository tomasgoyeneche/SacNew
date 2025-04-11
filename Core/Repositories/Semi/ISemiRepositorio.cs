using Shared.Models;

namespace Core.Repositories
{
    public interface ISemiRepositorio
    {
        // Obtener Semi Por Id
        Task<List<SemiDto>> BuscarSemisAsync(string textoBusqueda);

        Task<List<SemiDto>> ObtenerTodosLosSemisDto();

        Task<SemiDto> ObtenerPorIdDtoAsync(int idSemi);

        Task<Shared.Models.Semi> ObtenerSemiPorIdAsync(int idSemi);

        //Actualizar, Editar, Eliminar

        Task AltaSemiAsync(string patente, int idUsuario);

        Task ActualizarSemiAsync(Shared.Models.Semi semi);

        Task EliminarSemiAsync(int idSemi);

        Task ActualizarEmpresaSemiAsync(int idSemi, int idEmpresa);

        Task ActualizarVencimientoSemiAsync(int idSemi, int idVencimiento, DateTime fechaActualizacion, int idUsuario);
    }
}