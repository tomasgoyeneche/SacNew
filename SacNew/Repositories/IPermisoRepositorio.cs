namespace SacNew.Repositories
{
    public interface IPermisoRepositorio
    {
        List<int> ObtenerPermisosPorUsuario(int idUsuario);
    }
}