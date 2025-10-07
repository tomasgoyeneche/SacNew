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
    public class MantenimientoTareaArticuloRepositorio : BaseRepositorio, IMantenimientoTareaArticuloRepositorio
    {
        public MantenimientoTareaArticuloRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> AgregarAsync(MantenimientoTareaArticulo mantenimientoTareaArticulo)
        {
            const string query = @"
        INSERT INTO MantenimientoTareaArticulo (IdTarea, IdArticulo, Cantidad, Activo)
        VALUES (@IdTarea, @IdArticulo, @Cantidad, @Activo);
        SELECT CAST(SCOPE_IDENTITY() as int);";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteScalarAsync<int>(query, mantenimientoTareaArticulo);
            });
        }

        public async Task<int> EliminarAsync(int idMantenimientoTareaArticulo)
        {
            const string query = "UPDATE MantenimientoTareaArticulo SET Activo = 0 WHERE IdMantenimientoTareaArticulo = @IdMantenimientoTareaArticulo;";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteAsync(query, new { IdMantenimientoTareaArticulo = idMantenimientoTareaArticulo });
            });
        }

        public async Task<List<MantenimientoTareaArticulo>> ObtenerPorTareaAsync(int idTarea)
        {
            const string query = "SELECT * FROM MantenimientoTareaArticulo WHERE IdTarea = @IdTarea AND Activo = 1;";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<MantenimientoTareaArticulo>(query, new { IdTarea = idTarea });
                return result.ToList();
            });
        }
    }
}
