namespace SacNew.Repositories
{
    public interface IPermisoRepositorio
    {
        Task<List<int>> ObtenerPermisosPorUsuarioAsync(int idUsuario);
    }
}