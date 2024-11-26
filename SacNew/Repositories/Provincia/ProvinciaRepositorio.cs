using Dapper;
using SacNew.Models;
using SacNew.Services;

namespace SacNew.Repositories
{
    public class ProvinciaRepositorio : BaseRepositorio, IProvinciaRepositorio
    {
        public ProvinciaRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

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