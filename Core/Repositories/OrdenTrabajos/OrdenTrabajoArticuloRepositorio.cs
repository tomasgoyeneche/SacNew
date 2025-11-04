using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class OrdenTrabajoArticuloRepositorio : BaseRepositorio, IOrdenTrabajoArticuloRepositorio
    {
        private const string Tabla = "OrdenTrabajoArticulo";

        public OrdenTrabajoArticuloRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> AgregarAsync(OrdenTrabajoArticulo entidad)
            => await AgregarGenéricoAsync(Tabla, entidad);

        public async Task<int> ActualizarAsync(OrdenTrabajoArticulo entidad)
            => await ActualizarGenéricoAsync(Tabla, entidad);

        public async Task<int> EliminarAsync(int id)
            => await EliminarGenéricoAsync<OrdenTrabajoArticulo>(Tabla, id);

        public async Task<OrdenTrabajoArticulo?> ObtenerPorIdAsync(int id)
            => await ObtenerPorIdGenericoAsync<OrdenTrabajoArticulo>(Tabla, "IdOrdenTrabajoArticulo", id);

        public async Task<List<OrdenTrabajoArticulo>> ObtenerPorTareaAsync(int idOrdenTrabajoTarea)
        {
            const string query = @"
        SELECT * FROM OrdenTrabajoArticulo
        WHERE IdOrdenTrabajoTarea = @IdOrdenTrabajoTarea AND Activo = 1;";
            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<OrdenTrabajoArticulo>(query, new { IdOrdenTrabajoTarea = idOrdenTrabajoTarea });
                return result.ToList();
            });
        }

        public async Task<OrdenTrabajoArticulo?> ObtenerPorTareaYArticuloAsync(int idTarea, int idArticulo)
        {
            const string query = @"
        SELECT *
        FROM OrdenTrabajoArticulo
        WHERE IdOrdenTrabajoTarea = @IdTarea AND IdArticulo = @IdArticulo AND Activo = 1;";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<OrdenTrabajoArticulo>(query, new { IdTarea = idTarea, IdArticulo = idArticulo });
            });
        }
    }
}