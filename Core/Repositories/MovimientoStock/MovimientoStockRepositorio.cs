using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class MovimientoStockRepositorio : BaseRepositorio, IMovimientoStockRepositorio
    {
        public MovimientoStockRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<MovimientoStockDto>> ObtenerMovimientosAsync()
        {
            const string query = @"
        SELECT 
            m.IdMovimientoStock,
            t.Nombre AS Tipo,
            m.FechaEmision,
            m.FechaIngreso,
            CASE WHEN m.Autorizado = 1 THEN 'Sí' ELSE 'No' END AS Autorizado,
            m.Observaciones
        FROM MovimientoStock m
        INNER JOIN TipoMovimientoStock t ON m.IdTipoMovimiento = t.IdTipoMovimiento
        WHERE m.Activo = 1
        ORDER BY m.FechaEmision DESC";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<MovimientoStockDto>(query);
                return result.ToList();
            });
        }

        public async Task<List<TipoMovimientoStock>> ObtenerTipoMovimientosAsync()
        {
            const string query = @"
        SELECT * FROM TipoMovimientoStock ";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<TipoMovimientoStock>(query);
                return result.ToList();
            });
        }

        public async Task<MovimientoStock?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
            SELECT *
            FROM MovimientoStock
            WHERE IdMovimientoStock = @IdMovimientoStock AND Activo = 1";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<MovimientoStock>(
                    query,
                    new { IdMovimientoStock = id }
                );
            });
        }

        public async Task<int> AgregarAsync(MovimientoStock movimiento)
        {
            const string query = @"
            INSERT INTO MovimientoStock (IdTipoMovimiento, FechaEmision, FechaIngreso, Autorizado, Observaciones, Activo)
            VALUES (@IdTipoMovimiento, @FechaEmision, @FechaIngreso, @Autorizado, @Observaciones, 1);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            return await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteScalarAsync<int>(query, movimiento),
                "MovimientoStock",
                "INSERT",
                null,
                movimiento
            );
        }

        public async Task ActualizarAsync(MovimientoStock movimiento)
        {
            const string query = @"
            UPDATE MovimientoStock
            SET IdTipoMovimiento = @IdTipoMovimiento,
                FechaEmision = @FechaEmision,
                FechaIngreso = @FechaIngreso,
                Autorizado = @Autorizado,
                Observaciones = @Observaciones
            WHERE IdMovimientoStock = @IdMovimientoStock";

            await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, movimiento),
                "MovimientoStock",
                "UPDATE",
                null,
                movimiento
            );
        }
    }
}
