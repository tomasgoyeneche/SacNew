using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class AlertaRepositorio : BaseRepositorio, IAlertaRepositorio
    {
        public AlertaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
           : base(connectionStrings, sesionService) { }

        public async Task<List<AlertaDto?>> ObtenerTodasLasAlertasDtoAsync()
        {
            var query = "SELECT * FROM vw_AlertaDetalle";

            return await ConectarAsync(async connection =>
            {
                var result = await connection.QueryAsync<AlertaDto?>(query);
                return result.ToList();
            });
        }

        public async Task<Alerta?> ObtenerPorIdAsync(int idAlerta)
        {
            return await ObtenerPorIdGenericoAsync<Alerta>("Alerta", "idAlerta", idAlerta);
        }

        public async Task AgregarAlertaAsync(Alerta nuevaAlerta, int IdUsuario)
        {
            var query = @"
            INSERT INTO Alerta (idChofer, idtractor, idSemi, descripcion, idUsuario)
            VALUES (@idChofer, @idtractor, @idSemi, @descripcion, @IdUsuario)";

            await ConectarAsync(connection =>
            {
                var parameters = new DynamicParameters(nuevaAlerta);
                parameters.Add("IdUsuario", IdUsuario);
                return connection.ExecuteAsync(query, parameters);
            });
        }

        public async Task ActualizarAlertaAsync(Alerta alertaActualizada, int IdUsuario)
        {
            var query = @"
            UPDATE Alerta
            SET idChofer = @idChofer, idtractor = @idtractor, idSemi = @idSemi, descripcion = @descripcion, idUsuario = @idUsuario
            WHERE IdAlerta = @IdAlerta";

            await ConectarAsync(connection =>
            {
                var parameters = new DynamicParameters(alertaActualizada);
                parameters.Add("IdUsuario", IdUsuario);
                return connection.ExecuteAsync(query, parameters);
            });
        }

        public async Task EliminarAlertaAsync(int idAlerta)
        {
            var query = "UPDATE Alerta SET Activo = 0 WHERE IdAlerta = @idAlerta";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { IdAlerta = idAlerta });
            });
        }

        public async Task<List<AlertaDto>> ObtenerAlertasPorIdNominaAsync(int idNomina)
        {
            const string query = "SELECT * FROM vw_AlertaDetalle WHERE IdNomina = @idNomina";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<AlertaDto>(query, new { idNomina });
                return result.ToList();
            });
        }
    }
}