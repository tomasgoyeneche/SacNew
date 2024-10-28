namespace SacNew.Repositories
{
    public interface IUnidadRepositorio
    {
        Task<int?> ObtenerIdTractorPorPatenteAsync(string patente);

        Task<int?> ObtenerIdUnidadPorTractorAsync(int idTractor);
    }
}