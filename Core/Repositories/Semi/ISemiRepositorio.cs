using Shared.Models;

namespace Core.Repositories
{
    public interface ISemiRepositorio
    {
        Task<List<SemiDto>> ObtenerTodosLosSemisDto();

        Task<SemiDto> ObtenerPorIdDtoAsync(int idSemi);

        Task EliminarSemiAsync(int idSemi);

        Task<List<SemiDto>> BuscarSemisAsync(string textoBusqueda);


        //Actualizar Documental
        Task<ModificarSemiDto> ObtenerSemiPorIdAsync(int idSemi);
        Task ActualizarSemiAsync(ModificarSemiDto semi);
    }
}