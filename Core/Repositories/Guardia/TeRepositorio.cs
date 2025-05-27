using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class TeRepositorio : BaseRepositorio, ITeRepositorio
    {
        public TeRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<TransitoEspecial?> ObtenerPorIdAsync(int id)
        {
            const string query = "SELECT * FROM TE WHERE IdTe = @id";
            return await ConectarAsync(conn =>
                conn.QueryFirstOrDefaultAsync<TransitoEspecial>(query, new { id }));
        }
    }
}