using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class CoberturaRepositorio : BaseRepositorio, ICoberturaRepositorio
    {
        public CoberturaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<Cobertura>> ObtenerTodasAsync()
        {
            var query = "SELECT IdCobertura, TipoCobertura FROM Cobertura";
            return await ConectarAsync(async connection =>
            {
                return (await connection.QueryAsync<Cobertura>(query)).ToList();
            });
        }
    }
}