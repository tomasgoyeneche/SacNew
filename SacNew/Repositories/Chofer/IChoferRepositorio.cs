namespace SacNew.Repositories.Chofer
{
    public interface IChoferRepositorio
    {
        Task<int?> ObtenerIdPorDocumentoAsync(string documento);
    }
}