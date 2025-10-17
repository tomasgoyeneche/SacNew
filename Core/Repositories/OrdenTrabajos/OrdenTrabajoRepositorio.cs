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
    public class OrdenTrabajoRepositorio : BaseRepositorio, IOrdenTrabajoRepositorio
    {
        private const string Tabla = "OrdenTrabajo";

        public OrdenTrabajoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> AgregarAsync(OrdenTrabajo orden)
        {
            // Usamos el método genérico con auditoría automática
            return await AgregarGenéricoAsync(Tabla, orden);
        }

        public async Task<int> ActualizarAsync(OrdenTrabajo orden)
        {
            // También usa el genérico con auditoría y comparación de valores previos
            return await ActualizarGenéricoAsync(Tabla, orden);
        }

        public async Task<int> EliminarAsync(int idOrdenTrabajo)
        {
            // Elimina lógicamente (ACTIVO = 0)
            return await EliminarGenéricoAsync<OrdenTrabajo>(Tabla, idOrdenTrabajo);
        }

        public async Task<OrdenTrabajo?> ObtenerPorIdAsync(int idOrdenTrabajo)
        {
            return await ObtenerPorIdGenericoAsync<OrdenTrabajo>(Tabla, "IdOrdenTrabajo", idOrdenTrabajo);
        }

        public async Task<List<OrdenTrabajo>> ObtenerTodosAsync()
        {
            const string query = @"
        SELECT *
        FROM OrdenTrabajo
        WHERE Activo = 1
        ORDER BY FechaEmision DESC";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<OrdenTrabajo>(query);
                return result.ToList();
            });
        }



        public async Task<List<OrdenTrabajoDto>> ObtenerTodosDtoAsync()
        {
            const string query = @"
        SELECT *
        FROM vw_OrdenTrabajoDetalle
        WHERE Activo = 1
        ORDER BY FechaEmision DESC";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<OrdenTrabajoDto>(query);
                return result.ToList();
            });
        }

        public async Task<List<OrdenTrabajoDto>> ObtenerPorFaseAsync(string fase)
        {
            const string query = @"
        SELECT *
        FROM vw_OrdenTrabajoDetalle
        WHERE FaseNombre <> @Fase AND Activo = 1
        ORDER BY FechaEmision DESC;";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<OrdenTrabajoDto>(query, new { Fase = fase });
                return result.ToList();
            });
        }


        public async Task<List<OrdenTrabajo>> ObtenerPorFaseAsync(byte fase)
        {
            const string query = @"
        SELECT *
        FROM OrdenTrabajo
        WHERE Fase = @Fase AND Activo = 1
        ORDER BY FechaEmision DESC;";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<OrdenTrabajo>(query, new { Fase = fase });
                return result.ToList();
            });
        }
        public async Task<List<OrdenTrabajo>> ObtenerPorNominaAsync(int idNomina)
        {
            const string query = @"
        SELECT *
        FROM OrdenTrabajo
        WHERE IdNomina = @IdNomina AND Activo = 1
        ORDER BY FechaEmision DESC;";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<OrdenTrabajo>(query, new { IdNomina = idNomina });
                return result.ToList();
            });
        }
    }
}
