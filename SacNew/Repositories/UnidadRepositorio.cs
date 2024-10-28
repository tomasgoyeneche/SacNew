using Dapper;
using SacNew.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public class UnidadRepositorio : BaseRepositorio, IUnidadRepositorio
    {
        public UnidadRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

        public async Task<int?> ObtenerIdTractorPorPatenteAsync(string patente)
        {
            var query = @"
            SELECT IdTractor 
            FROM Tractor 
            WHERE Patente = @Patente AND Activo = 1";

            return await ConectarAsync(async conn =>
                await conn.QuerySingleOrDefaultAsync<int?>(query, new { Patente = patente }));
        }

        public async Task<int?> ObtenerIdUnidadPorTractorAsync(int idTractor)
        {
            var query = @"
            SELECT IdUnidad 
            FROM Unidad 
            WHERE IdTractor = @IdTractor AND Activo = 1";

            return await ConectarAsync(async conn =>
                await conn.QuerySingleOrDefaultAsync<int?>(query, new { IdTractor = idTractor }));
        }
    }
}
