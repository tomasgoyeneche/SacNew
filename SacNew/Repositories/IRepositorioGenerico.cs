namespace SacNew.Repositories
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<List<T>> ObtenerTodosAsync();

        Task<T?> ObtenerPorIdAsync(int id);

        Task AgregarAsync(T entidad);

        Task ActualizarAsync(T entidad, object valoresAnteriores);

        Task EliminarAsync(int id, T valoresAnteriores);
    }
}