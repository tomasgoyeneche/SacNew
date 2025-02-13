using Shared.Models;

namespace Core.Repositories
{
    public interface IEmpresaTipoRepositorio
    {
        Task<List<EmpresaTipo>> ObtenerTodosAsync();
    }
}