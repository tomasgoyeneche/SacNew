using Dapper;
using SacNew.Models;
using SacNew.Services;
using static SacNew.Services.Startup;

namespace SacNew.Repositories
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