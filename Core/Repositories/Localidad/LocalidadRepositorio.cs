using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class LocalidadRepositorio : BaseRepositorio, ILocalidadRepositorio
    {
        public LocalidadRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<Localidad>> ObtenerPorProvinciaAsync(int idProvincia)
        {
            var query = "SELECT idLocalidad, nombreLocalidad FROM Localidad WHERE idProvincia = @IdProvincia";
            return await ConectarAsync(async connection =>
            {
                return (await connection.QueryAsync<Localidad>(query, new { IdProvincia = idProvincia })).ToList();
            });
        }
    }
}