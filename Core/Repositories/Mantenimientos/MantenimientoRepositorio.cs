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
    public class MantenimientoRepositorio : BaseRepositorio, IMantenimientoRepositorio
    {
        public MantenimientoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> AgregarAsync(Mantenimiento mantenimiento)
        {
            const string query = @"
        INSERT INTO Mantenimiento
        (Nombre, IdTipoMantenimiento, AplicaA, Descripcion, Activo, KilometrosIntervalo, DiasIntervalo)
        VALUES (@Nombre, @IdTipoMantenimiento, @AplicaA, @Descripcion, @Activo, @KilometrosIntervalo, @DiasIntervalo);
        SELECT CAST(SCOPE_IDENTITY() as int);";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteScalarAsync<int>(query, mantenimiento);
            });
        }

        public async Task<int> ActualizarAsync(Mantenimiento mantenimiento)
        {
            const string query = @"
        UPDATE Mantenimiento
        SET Nombre = @Nombre,
            IdTipoMantenimiento = @IdTipoMantenimiento,
            AplicaA = @AplicaA,
            Descripcion = @Descripcion,
            Activo = @Activo,
            KilometrosIntervalo = @KilometrosIntervalo,
            DiasIntervalo = @DiasIntervalo
        WHERE IdMantenimiento = @IdMantenimiento;";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteAsync(query, mantenimiento);
            });
        }

        public async Task<int> EliminarAsync(int idMantenimiento)
        {
            const string query = "Update Mantenimiento set Activo = 0 WHERE IdMantenimiento = @IdMantenimiento";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteAsync(query, new { IdMantenimiento = idMantenimiento });
            });
        }

        public async Task<Mantenimiento?> ObtenerPorIdAsync(int idMantenimiento)
        {
            const string query = "SELECT * FROM Mantenimiento WHERE IdMantenimiento = @IdMantenimiento";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<Mantenimiento>(query, new { IdMantenimiento = idMantenimiento });
            });
        }

        public async Task<List<Mantenimiento>> ObtenerTodosAsync()
        {
            const string query = "SELECT * FROM Mantenimiento Where Activo = 1 ORDER BY Nombre";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<Mantenimiento>(query);
                return result.ToList();
            });
        }
    }
}
