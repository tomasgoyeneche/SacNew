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
    public class OrdenTrabajoMantenimientoRepositorio : BaseRepositorio, IOrdenTrabajoMantenimientoRepositorio
    {
        private const string Tabla = "OrdenTrabajoMantenimiento";

        public OrdenTrabajoMantenimientoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> AgregarAsync(OrdenTrabajoMantenimiento entidad)
            => await AgregarGenéricoAsync(Tabla, entidad);

        public async Task<int> ActualizarAsync(OrdenTrabajoMantenimiento entidad)
            => await ActualizarGenéricoAsync(Tabla, entidad);

        public async Task<int> EliminarAsync(int id)
            => await EliminarGenéricoAsync<OrdenTrabajoMantenimiento>(Tabla, id);

        public async Task<OrdenTrabajoMantenimiento?> ObtenerPorIdAsync(int id)
            => await ObtenerPorIdGenericoAsync<OrdenTrabajoMantenimiento>(Tabla, "IdOrdenTrabajoMantenimiento", id);

        public async Task<List<OrdenTrabajoMantenimiento>> ObtenerPorOrdenTrabajoAsync(int idOrdenTrabajo)
        {
            const string query = @"
        SELECT * FROM OrdenTrabajoMantenimiento
        WHERE IdOrdenTrabajo = @IdOrdenTrabajo AND Activo = 1
        ORDER BY IdOrdenTrabajoMantenimiento DESC;";
            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<OrdenTrabajoMantenimiento>(query, new { IdOrdenTrabajo = idOrdenTrabajo });
                return result.ToList();
            });
        }
    }
}
