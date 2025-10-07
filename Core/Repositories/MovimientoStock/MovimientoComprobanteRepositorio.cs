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
    public class MovimientoComprobanteRepositorio : BaseRepositorio, IMovimientoComprobanteRepositorio
    {
        public MovimientoComprobanteRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<MovimientoComprobante>> ObtenerPorMovimientoAsync(int idMovimientoStock)
        {
            const string query = @"
            SELECT * From MovimientoComprobante c
            WHERE c.IdMovimientoStock = @IdMovimientoStock AND c.Activo = 1";

            return await ConectarAsync(conn =>
                conn.QueryAsync<MovimientoComprobante>(query, new { IdMovimientoStock = idMovimientoStock })
                    .ContinueWith(t => t.Result.ToList())
            );
        }


        public async Task<TipoComprobante?> ObtenerTiposComprobantesPorId(int idTipoComprobante)
        {
            const string query = @"
            SELECT * From TipoComprobante c
            WHERE c.IdTipoComprobante = @IdTipoComprobante";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<TipoComprobante>(query, new { IdTipoComprobante = idTipoComprobante });
            });
        }

        public async Task<List<TipoComprobante>> ObtenerTiposComprobantes()
        {
            const string query = "SELECT * FROM [TipoComprobante] ORDER BY Nombre";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<TipoComprobante>(query);
                return result.ToList();
            });
        }
        public async Task<int> AgregarAsync(MovimientoComprobante comprobante)
        {
            const string query = @"
            INSERT INTO MovimientoComprobante (IdMovimientoStock, IdTipoComprobante, NroComprobante, IdProveedor, Activo, RutaComprobante)
            VALUES (@IdMovimientoStock, @IdTipoComprobante, @NroComprobante, @IdProveedor, 1, @RutaComprobante);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            return await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteScalarAsync<int>(query, comprobante),
                "MovimientoComprobante",
                "INSERT",
                null,
                comprobante
            );
        }

        public async Task<MovimientoComprobante?> ObtenerPorIdAsync(int idMovimientoComprobante)
        {
            const string query = @"
            SELECT *
            FROM MovimientoComprobante
            WHERE IdMovimientoComprobante = @IdMovimientoComprobante";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<MovimientoComprobante>(
                    query,
                    new { IdMovimientoComprobante = idMovimientoComprobante }
                );
            });
        }
        public async Task ActualizarAsync(MovimientoComprobante comprobante)
        {
            const string query = @"
            UPDATE MovimientoComprobante
            SET IdTipoComprobante = @IdTipoComprobante,
                NroComprobante = @NroComprobante,
                IdProveedor = @IdProveedor,
                RutaComprobante = @RutaComprobante
            WHERE IdMovimientoComprobante = @IdMovimientoComprobante";

            await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, comprobante),
                "MovimientoComprobante",
                "UPDATE",
                null,
                comprobante
            );
        }

        public async Task EliminarAsync(int idMovimientoComprobante)
        {
            const string query = "UPDATE MovimientoComprobante SET Activo = 0 WHERE IdMovimientoComprobante = @IdMovimientoComprobante";

            await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, new { IdMovimientoComprobante = idMovimientoComprobante }),
                "MovimientoComprobante",
                "DELETE",
                null,
                null
            );
        }
    }
}
