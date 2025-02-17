using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class CiaRepositorio : BaseRepositorio, ICiaRepositorio
    {
        public CiaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<Cia>> ObtenerTodasAsync()
        {
            var query = "SELECT IdCia, NombreFantasia FROM Cia WHERE Activo = 1";
            return await ConectarAsync(async connection =>
            {
                return (await connection.QueryAsync<Cia>(query)).ToList();
            });
        }
    }
}