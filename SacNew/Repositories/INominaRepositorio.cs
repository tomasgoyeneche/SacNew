using SacNew.Models;

namespace SacNew.Repositories
{
    public interface INominaRepositorio
    {
        List<Nomina> ObtenerTodasLasNominas();

        Task<Nomina?> ObtenerPorIdAsync(int idNomina);
    }
}