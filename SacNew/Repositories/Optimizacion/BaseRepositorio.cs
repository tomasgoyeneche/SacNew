using Dapper;
using SacNew.Models;
using SacNew.Services;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    public abstract class BaseRepositorio
    {
        private readonly string _connectionString;
        private readonly ISesionService _sesionService;

        protected BaseRepositorio(string connectionString, ISesionService sesionService)
        {
            _connectionString = connectionString;
            _sesionService = sesionService;
        }

        protected T Conectar<T>(Func<SqlConnection, T> consulta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return consulta(connection);
            }
        }

        // Método auxiliar para manejar la conexión con retorno
        protected async Task<T> ConectarAsync<T>(Func<SqlConnection, Task<T>> consulta)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return await consulta(connection);
        }

        // Método auxiliar para manejar la conexión sin retorno

        protected async Task ConectarAsync(Func<SqlConnection, Task> consulta)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await consulta(connection);
        }

        protected async Task EjecutarConAuditoriaAsync<T>(
           Func<SqlConnection, Task<T>> consulta,
           string tabla,
           string accion,
           object? valoresAnteriores = null,
           object? valoresNuevos = null)
        {
            await ConectarAsync(async conn =>
            {
                var resultado = await consulta(conn);
                await InsertarAuditoria(conn, tabla, accion, valoresAnteriores, valoresNuevos);
                return resultado;
            });
        }


        private async Task InsertarAuditoria(
               SqlConnection connection,
               string tabla,
               string accion,
               object? valoresAnteriores,
               object? valoresNuevos)
        {
            var auditoria = new Auditoria
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
    }
}