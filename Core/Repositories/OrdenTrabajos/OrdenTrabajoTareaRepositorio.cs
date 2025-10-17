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
    public class OrdenTrabajoTareaRepositorio : BaseRepositorio, IOrdenTrabajoTareaRepositorio
    {
        private const string Tabla = "OrdenTrabajoTarea";

        public OrdenTrabajoTareaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> AgregarAsync(OrdenTrabajoTarea entidad)
            => await AgregarGenéricoAsync(Tabla, entidad);

        public async Task<int> ActualizarAsync(OrdenTrabajoTarea entidad)
            => await ActualizarGenéricoAsync(Tabla, entidad);

        public async Task<int> EliminarAsync(int id)
            => await EliminarGenéricoAsync<OrdenTrabajoTarea>(Tabla, id);

        public async Task<OrdenTrabajoTarea?> ObtenerPorIdAsync(int id)
            => await ObtenerPorIdGenericoAsync<OrdenTrabajoTarea>(Tabla, "IdOrdenTrabajoTarea", id);

        public async Task<List<OrdenTrabajoTarea>> ObtenerPorMantenimientoAsync(int idOrdenTrabajoMantenimiento)
        {
            const string query = @"
        SELECT * FROM OrdenTrabajoTarea
        WHERE IdOrdenTrabajoMantenimiento = @IdOrdenTrabajoMantenimiento AND Activo = 1;";
            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<OrdenTrabajoTarea>(query, new { IdOrdenTrabajoMantenimiento = idOrdenTrabajoMantenimiento });
                return result.ToList();
            });
        }
    }
}
