using Dapper;
using SacNew.Models;
using SacNew.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public class PeriodoRepositorio : BaseRepositorio, IPeriodoRepositorio
    {
        public PeriodoRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

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
