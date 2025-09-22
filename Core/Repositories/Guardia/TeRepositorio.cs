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

        public async Task<List<GuardiaTransitoEspecialDto>> ObtenerControlTransitoEspecial(DateTime desde, DateTime hasta)
        {
            string query = @"SELECT * FROM vw_GuardiaTransitoEspecial WHERE Ingreso BETWEEN @desde AND @hasta Order by Ingreso";
            return (await ConectarAsync(conn => conn.QueryAsync<GuardiaTransitoEspecialDto>(query, new { desde, hasta }))).ToList();
        }
    }
}