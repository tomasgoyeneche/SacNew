using Dapper;
using SacNew.Services;

namespace SacNew.Repositories
{
    public class BaseRepositorioGenerico<T> : BaseRepositorio where T : class
    {
        private readonly string _nombreTabla;

        public BaseRepositorioGenerico(string connectionString, ISesionService sesionService, string nombreTabla)
            : base(connectionString, sesionService)
        {
            _nombreTabla = nombreTabla;
        }

        public async Task<List<T>> ObtenerTodosAsync()
        {
            var query = $"SELECT * FROM {_nombreTabla} WHERE Activo = 1";
            return (await ConectarAsync(conn => conn.QueryAsync<T>(query))).ToList();
        }

        public async Task<T?> ObtenerPorIdAsync(int id)
        {
            var query = $"SELECT * FROM {_nombreTabla} WHERE Id = @Id";
            return await ConectarAsync(conn =>
                conn.QuerySingleOrDefaultAsync<T>(query, new { Id = id }));
        }

        public async Task AgregarAsync(T entidad)
        {
            var query = GenerarInsertQuery(entidad);
            await EjecutarConAuditoriaAsync(
                async conn => await conn.ExecuteAsync(query, entidad),
                _nombreTabla,
                "INSERT",
                null,
                entidad
            );
        }

        public async Task ActualizarAsync(T entidad, object valoresAnteriores)
        {
            var query = GenerarUpdateQuery(entidad);
            await EjecutarConAuditoriaAsync(
                async conn => await conn.ExecuteAsync(query, entidad),
                _nombreTabla,
                "UPDATE",
                valoresAnteriores,
                entidad
            );
        }

        public async Task EliminarAsync(int id, T valoresAnteriores)
        {
            var query = $"UPDATE {_nombreTabla} SET Activo = 0 WHERE Id = @Id";
            await EjecutarConAuditoriaAsync(
                async conn => await conn.ExecuteAsync(query, new { Id = id }),
                _nombreTabla,
                "DELETE",
                valoresAnteriores,
                null
            );
        }

        private string GenerarInsertQuery(T entidad)
        {
            var columnas = string.Join(", ", typeof(T).GetProperties().Select(p => p.Name));
            var valores = string.Join(", ", typeof(T).GetProperties().Select(p => $"@{p.Name}"));
            return $"INSERT INTO {_nombreTabla} ({columnas}) VALUES ({valores})";
        }

        private string GenerarUpdateQuery(T entidad)
        {
            var setClause = string.Join(", ", typeof(T).GetProperties().Select(p => $"{p.Name} = @{p.Name}"));
            return $"UPDATE {_nombreTabla} SET {setClause} WHERE Id = @Id";
        }
    }
}