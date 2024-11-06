using Dapper;
using SacNew.Models;
using SacNew.Services;

namespace SacNew.Repositories.Chofer
{
    public class ChoferRepositorio : BaseRepositorio, IChoferRepositorio
    {
        public ChoferRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

        public async Task<List<chofer>> ObtenerTodosLosChoferes()
        {
            var query = "SELECT * FROM Chofer WHERE Activo = 1";

            return await ConectarAsync(async connection =>
            {
                var chofers = await connection.QueryAsync<chofer>(query);
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