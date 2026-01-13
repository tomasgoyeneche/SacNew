using Shared.Models.RequerimientoCompra;

namespace Core.Repositories.RequerimientoCompra
{
    public interface IUsuarioRccRepositorio
    {
        Task<List<UsuarioRcc>> ObtenerActivosAsync();

        Task<UsuarioRcc?> ObtenerPorIdAsync(int idUsuario);
    }
}