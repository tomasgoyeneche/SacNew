using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class ChoferRepositorio : BaseRepositorio, IChoferRepositorio
    {
        public ChoferRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<Chofer>> ObtenerTodosLosChoferes()
        {
            var query = "SELECT * FROM Chofer WHERE Activo = 1";

            return await ConectarAsync(async connection =>
            {
                var chofers = await connection.QueryAsync<Chofer>(query);
                return chofers.ToList();
            });
        }

        public async Task<int?> ObtenerIdPorDocumentoAsync(string documento)
        {
            var query = "SELECT IdChofer FROM Chofer WHERE Documento = @Documento AND Activo = 1";

            return await ConectarAsync(async conn =>
                await conn.QuerySingleOrDefaultAsync<int?>(query, new { Documento = documento }));
        }
    }
}