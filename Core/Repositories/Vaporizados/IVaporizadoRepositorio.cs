using Shared.Models;

namespace Core.Repositories
{
    public interface IVaporizadoRepositorio
    {
        Task AgregarAsync(Vaporizado vaporizado, int idUsuario);

        Task EditarAsync(Vaporizado vaporizado, int idUsuario);

        Task<Vaporizado?> ObtenerPorIdAsync(int idVaporizado);

        Task<List<VaporizadoDto>> ObtenerTodosLosVaporizadosDto();

        Task<List<VaporizadoDto>> ObtenerVaporizadosDtoPorPosta(int idPosta);

        Task<Vaporizado?> ObtenerPorNominaAsync(int idNomina);

        Task<Vaporizado?> ObtenerPorTeAsync(int idTe);

        Task EliminarAsync(int idVaporizado);
    }
}