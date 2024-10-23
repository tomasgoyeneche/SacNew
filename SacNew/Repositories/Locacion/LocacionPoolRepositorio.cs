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
    internal class LocacionPoolRepositorio : BaseRepositorio, ILocacionPoolRepositorio
    {
        public LocacionPoolRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService)
        {
        }

        public async Task<IEnumerable<Locacion>> ObtenerPorIdPoolAsync(int idPool)
        {
            var query = @"
            SELECT l.*
            FROM Locacion l
            INNER JOIN LocacionPool lp ON l.IdLocacion = lp.IdLocacion
            WHERE lp.IdPool = @idPool AND lp.Activo = 1";

            return await ConectarAsync(conn =>
                conn.QueryAsync<Locacion>(query, new { idPool }));
        }

        public async Task<LocacionPool?> ObtenerRelacionAsync(int idPool, int idLocacion)
        {
            var query = @"
            SELECT * 
            FROM LocacionPool
            WHERE IdPool = @IdPool AND IdLocacion = @IdLocacion AND Activo = 1";

            return await ConectarAsync(conn =>
                conn.QuerySingleOrDefaultAsync<LocacionPool>(query, new { IdPool = idPool, IdLocacion = idLocacion }));
        }

        public async Task<IEnumerable<Locacion>> ObtenerLocacionesDisponiblesAsync()
        {
            var query = @"
            SELECT * 
            FROM Locacion 
            WHERE Activo = 1";

            return await ConectarAsync(conn => conn.QueryAsync<Locacion>(query));
        }

        public async Task AgregarLocacionAlPoolAsync(LocacionPool locacionPool)
        {
            var query = @"
            INSERT INTO LocacionPool (IdPool, IdLocacion, Activo)
            VALUES (@IdPool, @IdLocacion, @Activo)";

            await EjecutarConAuditoriaAsync(
                async conn => await conn.ExecuteAsync(query, locacionPool),
                "LocacionPool",
                "INSERT",
                null,
                locacionPool
            );
        }

        public async Task EliminarLocacionDelPoolAsync(int idLocacionPool)
        {
            var query = "UPDATE LocacionPool SET Activo = 0 WHERE IdLocacionPool = @IdLocacionPool";

            var valoresAnteriores = await ObtenerPorIdAsync(idLocacionPool);
            await EjecutarConAuditoriaAsync(
                async conn => await conn.ExecuteAsync(query, new { IdLocacionPool = idLocacionPool }),
                "LocacionPool",
                "DELETE",
                valoresAnteriores,
                null
            );
        }

        private async Task<LocacionPool?> ObtenerPorIdAsync(int idLocacionPool)
        {
            var query = "SELECT * FROM LocacionPool WHERE IdLocacionPool = @IdLocacionPool";
            return await ConectarAsync(conn =>
                conn.QuerySingleOrDefaultAsync<LocacionPool>(query, new { IdLocacionPool = idLocacionPool }));
        }
    }
}
