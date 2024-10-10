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
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            try
            {
                // Ejecutar la consulta
                var resultado = await consulta(connection);

                // Serializar los valores para la auditoría
                var valoresAnterioresJson = valoresAnteriores != null ? Newtonsoft.Json.JsonConvert.SerializeObject(valoresAnteriores) : null;
                var valoresNuevosJson = valoresNuevos != null ? Newtonsoft.Json.JsonConvert.SerializeObject(valoresNuevos) : null;

                // Crear la entrada de auditoría
                var auditoria = new Auditoria
                {
                    IdUsuario = _sesionService.IdUsuario,
                    TablaModificada = tabla,
                    Accion = accion,
                    ValoresAnteriores = valoresAnterioresJson,
                    ValoresNuevos = valoresNuevosJson,
                    FechaHora = DateTime.Now
                };

                var queryAuditoria = @"
            INSERT INTO Auditoria (IdUsuario, TablaModificada, Accion, ValoresAnteriores, ValoresNuevos, FechaHora)
            VALUES (@IdUsuario, @TablaModificada, @Accion, @ValoresAnteriores, @ValoresNuevos, @FechaHora)";

                // Insertar la auditoría
                await connection.ExecuteAsync(queryAuditoria, auditoria);
            }
            catch (Exception ex)
            {
                // Manejar el error, si es necesario
                throw;
            }
        }
    }
}