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
    public class TareaRepositorio : BaseRepositorio, ITareaRepositorio
    {
        public TareaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> AgregarAsync(Tarea tarea)
        {
            const string query = @"
        INSERT INTO Tarea (Nombre, Descripcion, ManoObra, Horas, Activo)
        VALUES (@Nombre, @Descripcion, @ManoObra, @Horas, @Activo);
        SELECT CAST(SCOPE_IDENTITY() as int);";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteScalarAsync<int>(query, tarea);
            });
        }

        public async Task<int> ActualizarAsync(Tarea tarea)
        {
            const string query = @"
        UPDATE Tarea
        SET Nombre = @Nombre,
            Descripcion = @Descripcion,
            ManoObra = @ManoObra,
            Horas = @Horas,
            Activo = @Activo
        WHERE IdTarea = @IdTarea;";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteAsync(query, tarea);
            });
        }

        public async Task<int> EliminarAsync(int idTarea)
        {
            const string query = "UPDATE Tarea SET Activo = 0 WHERE IdTarea = @IdTarea;";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteAsync(query, new { IdTarea = idTarea });
            });
        }

        public async Task<Tarea?> ObtenerPorIdAsync(int idTarea)
        {
            const string query = "SELECT * FROM Tarea WHERE IdTarea = @IdTarea;";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<Tarea>(query, new { IdTarea = idTarea });
            });
        }

        public async Task<List<Tarea>> ObtenerTodosAsync()
        {
            const string query = "SELECT * FROM Tarea WHERE Activo = 1 ORDER BY Nombre;";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<Tarea>(query);
                return result.ToList();
            });
        }
    }
}
