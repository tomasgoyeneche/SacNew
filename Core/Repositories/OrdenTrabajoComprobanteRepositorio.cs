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
    internal class OrdenTrabajoComprobanteRepositorio : BaseRepositorio, IOrdenTrabajoComprobanteRepositorio
    {
        public OrdenTrabajoComprobanteRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<OrdenTrabajoComprobante>> ObtenerPorMovimientoAsync(int idOrdenTrabajo)
        {
            const string query = @"
            SELECT * From OrdenTrabajoComprobante c
            WHERE c.IdOrdenTrabajo = @IdOrdenTrabajo AND c.Activo = 1";

            return await ConectarAsync(conn =>
                conn.QueryAsync<OrdenTrabajoComprobante>(query, new { IdOrdenTrabajo = idOrdenTrabajo })
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
        public async Task<int> AgregarAsync(OrdenTrabajoComprobante comprobante)
        {
            const string query = @"
            INSERT INTO OrdenTrabajoComprobante (IdOrdenTrabajo, Nombre, IdTipoComprobante, NroComprobante, Activo, RutaComprobante)
            VALUES (@IdOrdenTrabajo, @Nombre, @IdTipoComprobante, @NroComprobante, 1, @RutaComprobante);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            return await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteScalarAsync<int>(query, comprobante),
                "OrdenTrabajoComprobante",
                "INSERT",
                null,
                comprobante
            );
        }

        public async Task<OrdenTrabajoComprobante?> ObtenerPorIdAsync(int idOrdenTrabajoComprobante)
        {
            const string query = @"
            SELECT *
            FROM OrdenTrabajoComprobante
            WHERE OrdenTrabajoComprobante = @OrdenTrabajoComprobante";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<OrdenTrabajoComprobante>(
                    query,
                    new { IdOrdenTrabajoComprobante = idOrdenTrabajoComprobante }
                );
            });
        }
        public async Task ActualizarAsync(OrdenTrabajoComprobante comprobante)
        {
            const string query = @"
            UPDATE OrdenTrabajoComprobante
            SET Nombre = @Nombre,
                IdTipoComprobante = @IdTipoComprobante,
                NroComprobante = @NroComprobante,
                RutaComprobante = @RutaComprobante
            WHERE IdOrdenTrabajoComprobante = @IdOrdenTrabajoComprobante";

            await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, comprobante),
                "OrdenTrabajoComprobante",
                "UPDATE",
                null,
                comprobante
            );
        }

        public async Task EliminarAsync(int idOrdenTrabajoComprobante)
        {
            const string query = "UPDATE OrdenTrabajoComprobante SET Activo = 0 WHERE IdOrdenTrabajoComprobante = @IdOrdenTrabajoComprobante";

            await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, new { IdOrdenTrabajoComprobante = idOrdenTrabajoComprobante }),
                "OrdenTrabajoComprobante",
                "DELETE",
                null,
                null
            );
        }
    }
}
