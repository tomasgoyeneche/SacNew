using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
    }
}