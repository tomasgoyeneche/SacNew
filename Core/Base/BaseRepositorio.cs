using Core.Services;
using Dapper;

//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Shared.Models;

namespace Core.Base
{
    public abstract class BaseRepositorio
    {
        private readonly string _connectionString;
        private readonly string _connectionStringFo;
        private readonly ISesionService _sesionService;

        protected BaseRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
        {
            _connectionString = connectionStrings.MyDBConnectionString;
            _sesionService = sesionService;
            _connectionStringFo = connectionStrings.FOConnectionString;
        }

        // Método auxiliar para manejar la conexión con retorno
        protected async Task<T> ConectarAsync<T>(Func<SqlConnection, Task<T>> consulta)
        {
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                return await consulta(connection);
            }
            catch (SqlException ex)
            {
                // Re-lanzar con un mensaje claro
                throw new ApplicationException($"SQL Error {ex.Number}: {ex.Message}", ex);
            }
        }

        // Método auxiliar para manejar la conexión sin retorno
        protected async Task ConectarAsync(Func<SqlConnection, Task> consulta)
        {
            await ConectarAsync(async conn =>
            {
                await consulta(conn);
                return true; // Dummy value para cumplir con la firma genérica.
            });
        }

        protected async Task<T> ConectarAsyncFo<T>(Func<SqlConnection, Task<T>> consulta)
        {
            try
            {
                await using var connection = new SqlConnection(_connectionStringFo);
                await connection.OpenAsync();
                return await consulta(connection);
            }
            catch (SqlException ex)
            {
                // Re-lanzar con un mensaje claro
                throw new ApplicationException("Ocurrió un error al acceder a la base de datos.", ex);
            }
        }

        // Método auxiliar para manejar la conexión sin retorno
        protected async Task ConectarAsyncFo(Func<SqlConnection, Task> consulta)
        {
            await ConectarAsyncFo(async conn =>
            {
                await consulta(conn);
                return true; // Dummy value para cumplir con la firma genérica.
            });
        }

        protected async Task<T> EjecutarConAuditoriaAsync<T>(
          Func<SqlConnection, Task<T>> consulta,
          string tabla,
          string accion,
          object? valoresAnteriores = null,
          object? valoresNuevos = null)
        {
            return await ConectarAsync(async conn =>
            {
                var resultado = await consulta(conn);
                await InsertarAuditoriaAsync(conn, tabla, accion, valoresAnteriores, valoresNuevos);
                return resultado;
            });
        }

        // Método para insertar registros de auditoría
        private async Task InsertarAuditoriaAsync(
            SqlConnection connection,
            string tabla,
            string accion,
            object? valoresAnteriores,
            object? valoresNuevos)
        {
            try
            {
                var auditoria = new
                {
                    IdUsuario = _sesionService.IdUsuario,
                    TablaModificada = tabla,
                    Accion = accion,
                    ValoresAnteriores = valoresAnteriores != null
                        ? Newtonsoft.Json.JsonConvert.SerializeObject(valoresAnteriores)
                        : string.Empty,
                    ValoresNuevos = valoresNuevos != null
                        ? Newtonsoft.Json.JsonConvert.SerializeObject(valoresNuevos)
                        : string.Empty,
                    FechaHora = DateTime.Now
                };

                var query = @"
            INSERT INTO Auditoria
            (IdUsuario, TablaModificada, Accion, ValoresAnteriores, ValoresNuevos, FechaHora)
            VALUES (@IdUsuario, @TablaModificada, @Accion, @ValoresAnteriores, @ValoresNuevos, @FechaHora)";

                await connection.ExecuteAsync(query, auditoria);
            }
            catch (SqlException ex)
            {
                // Re-lanzar con un mensaje claro
                throw new ApplicationException("Error al registrar auditoría.", ex);
            }
        }



        protected (string whereClause, DynamicParameters parameters) ConstruirFiltroDinamico(Dictionary<string, object?> filtros)
        {
            var condiciones = new List<string>();
            var parametros = new DynamicParameters();

            foreach (var filtro in filtros)
            {
                if (filtro.Value == null) continue;

                if (filtro.Key.EndsWith("_like", StringComparison.OrdinalIgnoreCase))
                {
                    var key = filtro.Key.Replace("_like", "");
                    condiciones.Add($"{key} LIKE @{key}");
                    parametros.Add(key, $"%{filtro.Value}%");
                }
                else if (filtro.Key.EndsWith("_desde", StringComparison.OrdinalIgnoreCase))
                {
                    var key = filtro.Key.Replace("_desde", "");
                    condiciones.Add($"{key} >= @{filtro.Key}");
                    parametros.Add(filtro.Key, filtro.Value);
                }
                else if (filtro.Key.EndsWith("_hasta", StringComparison.OrdinalIgnoreCase))
                {
                    var key = filtro.Key.Replace("_hasta", "");
                    condiciones.Add($"{key} <= @{filtro.Key}");
                    parametros.Add(filtro.Key, filtro.Value);
                }
                else
                {
                    condiciones.Add($"{filtro.Key} = @{filtro.Key}");
                    parametros.Add(filtro.Key, filtro.Value);
                }
            }

            var whereClause = condiciones.Count > 0 ? "WHERE " + string.Join(" AND ", condiciones) : "";
            return (whereClause, parametros);
        }
    }
}