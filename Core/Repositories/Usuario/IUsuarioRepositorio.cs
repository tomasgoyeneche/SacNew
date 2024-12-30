using Shared.Models;

namespace Core.Repositories
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);

        Task<List<Usuario>> ObtenerTodosAsync();

        Task<List<Usuario>> BuscarPorCriterioAsync(string criterio);

        Task EliminarAsync(int idUsuario);

        Task<Usuario?> ObtenerPorIdAsync(int idUsuario);

        Task ActualizarAsync(Usuario usuario);

        Task AgregarAsync(Usuario usuario);
    }
}