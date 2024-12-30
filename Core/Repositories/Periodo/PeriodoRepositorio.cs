using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class PeriodoRepositorio : BaseRepositorio, IPeriodoRepositorio
    {
        public PeriodoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public Task<List<Periodo>> ObtenerPeriodosActivosAsync()
        {
            var query = "SELECT * FROM Periodo WHERE Activo = 1";

            return ConectarAsync(connection =>
            {
                return connection.QueryAsync<Periodo>(query)
                                 .ContinueWith(task => task.Result.ToList());
            });
        }
    }
}