using Dapper;
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
            try
            {
                await using var connection = new SqlConnection(_connectionString);
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
        protected async Task ConectarAsync(Func<SqlConnection, Task> consulta)
        {
            await ConectarAsync(async conn =>
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
                    FechaHora = DateTime.UtcNow
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
    }
}