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
    public class MovimientoStockDetalleRepositorio : BaseRepositorio, IMovimientoStockDetalleRepositorio
    {
        public MovimientoStockDetalleRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<MovimientoStockDetalle>> ObtenerPorMovimientoAsync(int idMovimientoStock)
        {
            const string query = @"
            SELECT *
            FROM MovimientoStockDetalle
            WHERE IdMovimientoStock = @IdMovimientoStock AND Activo = 1";

            return await ConectarAsync(conn =>
                conn.QueryAsync<MovimientoStockDetalle>(query, new { IdMovimientoStock = idMovimientoStock })
                    .ContinueWith(t => t.Result.ToList())
            );
        }

        public async Task<int> AgregarAsync(MovimientoStockDetalle detalle)
        {
            const string query = @"
            INSERT INTO MovimientoStockDetalle (IdMovimientoStock, IdArticulo, IdPosta, Cantidad, PrecioUnitario, PrecioTotal, Activo)
            VALUES (@IdMovimientoStock, @IdArticulo, @IdPosta, @Cantidad, @PrecioUnitario, @PrecioTotal, 1);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            return await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteScalarAsync<int>(query, detalle),
                "MovimientoStockDetalle",
                "INSERT",
                null,
                detalle
            );
        }

        public async Task ActualizarAsync(MovimientoStockDetalle detalle)
        {
            const string query = @"
            UPDATE MovimientoStockDetalle
            SET IdArticulo = @IdArticulo,
                IdPosta = @IdPosta,
                Cantidad = @Cantidad,
                PrecioUnitario = @PrecioUnitario,
                PrecioTotal = @PrecioTotal
            WHERE IdMovimientoDetalle = @IdMovimientoDetalle";

            await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, detalle),
                "MovimientoStockDetalle",
                "UPDATE",
                null,
                detalle
            );
        }

        public async Task<MovimientoStockDetalle?> ObtenerPorIdAsync(int idMovimientoDetalle)
        {
            const string query = @"
            SELECT *
            FROM MovimientoStockDetalle
            WHERE IdMovimientoDetalle = @IdMovimientoDetalle";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<MovimientoStockDetalle>(
                    query,
                    new { IdMovimientoDetalle = idMovimientoDetalle }
                );
            });
        }
        public async Task EliminarAsync(int idMovimientoDetalle)
        {
            const string query = "UPDATE MovimientoStockDetalle SET Activo = 0 WHERE IdMovimientoDetalle = @IdMovimientoDetalle";

            await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, new { IdMovimientoDetalle = idMovimientoDetalle }),
                "MovimientoStockDetalle",
                "DELETE",
                null,
                null
            );
        }
    }
}
