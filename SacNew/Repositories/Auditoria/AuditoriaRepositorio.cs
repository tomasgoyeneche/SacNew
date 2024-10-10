using SacNew.Models;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    public class AuditoriaRepositorio : IAuditoriaRepositorio
    {
        private readonly string _connectionString;

        public AuditoriaRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AgregarAuditoriaAsync(Auditoria auditoria)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                INSERT INTO Auditoria (IdUsuario, TablaModificada, Accion, FechaHora, RegistroModificadoId, ValoresAnteriores, ValoresNuevos)
                VALUES (@IdUsuario, @TablaModificada, @Accion, @FechaHora, @RegistroModificadoId, @ValoresAnteriores, @ValoresNuevos)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", auditoria.IdUsuario);
                    command.Parameters.AddWithValue("@TablaModificada", auditoria.TablaModificada);
                    command.Parameters.AddWithValue("@Accion", auditoria.Accion);
                    command.Parameters.AddWithValue("@FechaHora", auditoria.FechaHora);
                    command.Parameters.AddWithValue("@RegistroModificadoId", auditoria.RegistroModificadoId);
                    command.Parameters.AddWithValue("@ValoresAnteriores", (object)auditoria.ValoresAnteriores ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ValoresNuevos", (object)auditoria.ValoresNuevos ?? DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}