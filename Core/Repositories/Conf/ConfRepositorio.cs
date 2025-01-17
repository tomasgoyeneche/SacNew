using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class ConfRepositorio : BaseRepositorio, IConfRepositorio
    {
        public ConfRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<Conf?> ObtenerRutaPorIdAsync(int idConf)
        {
            return await ConectarAsync(async conn =>
            {
                var query = "SELECT * FROM Conf WHERE IdConf = @IdConf AND Activo = 1";
                return await conn.QueryFirstOrDefaultAsync<Conf>(query, new { IdConf = idConf });
            });
        }
    }
}