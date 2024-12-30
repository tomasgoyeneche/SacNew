namespace Core.Repositories
{
    public interface IPermisoRepositorio
    {
        Task<List<int>> ObtenerPermisosPorUsuarioAsync(int idUsuario);
    }
}