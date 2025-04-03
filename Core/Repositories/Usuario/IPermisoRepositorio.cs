namespace Core.Repositories
{
    public interface IPermisoRepositorio
    {
        Task<List<string>> ObtenerPermisosPorUsuarioAsync(int idUsuario);
    }
}