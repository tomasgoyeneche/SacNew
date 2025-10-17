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
    public class MantenimientoTareaRepositorio : BaseRepositorio, IMantenimientoTareaRepositorio
    {
        public MantenimientoTareaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> AgregarAsync(MantenimientoTarea mantenimientoTarea)
        {
            const string query = @"
        INSERT INTO MantenimientoTarea (IdMantenimiento, IdTarea, Activo)
        VALUES (@IdMantenimiento, @IdTarea, @Activo);
        SELECT CAST(SCOPE_IDENTITY() as int);";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteScalarAsync<int>(query, mantenimientoTarea);
            });
        }

        public async Task<int> EliminarAsync(int idMantenimientoTarea)
        {
            const string query = "UPDATE MantenimientoTarea SET Activo = 0 WHERE IdMantenimientoTarea = @IdMantenimientoTarea;";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteAsync(query, new { IdMantenimientoTarea = idMantenimientoTarea });
            });
        }

        public async Task<List<MantenimientoTarea>> ObtenerPorMantenimientoAsync(int idMantenimiento)
        {
            const string query = "SELECT * FROM MantenimientoTarea WHERE IdMantenimiento = @IdMantenimiento AND Activo = 1;";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<MantenimientoTarea>(query, new { IdMantenimiento = idMantenimiento });
                return result.ToList();
            });
        }
    }

}
