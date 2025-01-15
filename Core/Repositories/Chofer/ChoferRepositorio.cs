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

        public async Task<List<Chofer>> BuscarAsync(string textoBusqueda)
        {
            var query = "SELECT * FROM Chofer WHERE activo = 1 and (Nombre LIKE @TextoBusqueda OR Apellido LIKE @TextoBusqueda OR Documento LIKE @TextoBusqueda)";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<Chofer>(query, new { TextoBusqueda = $"%{textoBusqueda}%" })
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task<int?> ObtenerIdPorDocumentoAsync(string documento)
        {
            var query = "SELECT IdChofer FROM Chofer WHERE Documento = @Documento AND Activo = 1";

            return await ConectarAsync(async conn =>
                await conn.QuerySingleOrDefaultAsync<int?>(query, new { Documento = documento }));
        }

        public async Task EliminarChoferAsync(int idChofer)
        {
            var query = "Update Chofer set Activo = 0 WHERE IdChofer = @IdChofer";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { IdChofer = idChofer });
            });
        }
    }
}