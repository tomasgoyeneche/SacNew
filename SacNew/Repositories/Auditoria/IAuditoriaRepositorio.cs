using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IAuditoriaRepositorio
    {
        Task AgregarAuditoriaAsync(Auditoria auditoria);
    }
}