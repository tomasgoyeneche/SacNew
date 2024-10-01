using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IUsuarioRepositorio
    {
        Usuario ObtenerPorNombreUsuario(string nombreUsuario);
    }
}