using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class ProvinciaRepositorio : BaseRepositorio, IProvinciaRepositorio
    {
        public ProvinciaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<Provincia>> ObtenerProvinciasAsync()
        {
            var query = "SELECT idProvincia, nombreProvincia FROM Provincia";

            return await ConectarAsync(async connection =>
            {
                var provincias = await connection.QueryAsync<Provincia>(query);
                return provincias.ToList(); // Convertimos el IEnumerable a List
            });
        }
    }
}