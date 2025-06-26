using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class MedidaRepositorio : BaseRepositorio, IMedidaRepositorio
    {
        public MedidaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<List<Medida>> ObtenerTodosAsync()
        {
            var query = "SELECT * FROM Medida";

            return await ConectarAsync(async connection =>
            {
                var medidas = await connection.QueryAsync<Medida>(query);
                return medidas.ToList();
            });
        }
    }
}