using Dapper;
using SacNew.Models;
using SacNew.Services;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    public class LocacionKilometrosEntreRepositorio : BaseRepositorio, ILocacionKilometrosEntreRepositorio
    {
        public LocacionKilometrosEntreRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService)
        {
        }

        public async Task<List<LocacionKilometrosEntre>> ObtenerPorLocacionIdAsync(int idLocacion)
        {
            var query = @"
    SELECT lk.IdKilometros, lk.IdLocacionOrigen, lk.IdLocacionDestino, lk.Kilometros, l.IdLocacion AS LocacionDestinoId, l.Nombre 
    FROM LocacionKilometrosEntre lk
    INNER JOIN Locacion l ON lk.IdLocacionDestino = l.IdLocacion
    WHERE lk.IdLocacionOrigen = @IdLocacion";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<LocacionKilometrosEntre, Locacion, LocacionKilometrosEntre>(
                    query,
                    (locacionKilometrosEntre, locacionDestino) =>
                    {
                        locacionKilometrosEntre.LocacionDestino = locacionDestino;  // Asignar la locación destino
                        return locacionKilometrosEntre;
                    },
                    new { IdLocacion = idLocacion },  // Parámetro para la consulta
                    splitOn: "LocacionDestinoId"  // Indica dónde empieza el segundo objeto (LocacionDestino)
                ).ContinueWith(task => task.Result.ToList());
            });
        }

        public Task AgregarAsync(LocacionKilometrosEntre locacionKilometrosEntre)
        {
            var query = @"
        INSERT INTO LocacionKilometrosEntre (IdLocacionOrigen, IdLocacionDestino, Kilometros)
        VALUES (@IdLocacionOrigen, @IdLocacionDestino, @Kilometros)";

            return EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, locacionKilometrosEntre),
                "LocacionKilometrosEntre",
                "INSERT",
                null,
                locacionKilometrosEntre);
        }

        public async Task EliminarAsync(int idKilometros)
        {
            var locacionKilometrosAnterior = await ObtenerPorIdAsync(idKilometros);  // Obtener antes de eliminar
            var query = "DELETE FROM LocacionKilometrosEntre WHERE IdKilometros = @IdKilometros";

            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, new { IdKilometros = idKilometros }),
                "LocacionKilometrosEntre",
                "DELETE",
                locacionKilometrosAnterior,
                null);
        }

        private async Task<LocacionKilometrosEntre?> ObtenerPorIdAsync(int idKilometros)
        {
            var query = "SELECT * FROM LocacionKilometrosEntre WHERE IdKilometros = @IdKilometros";

            return await ConectarAsync(connection =>
                connection.QueryFirstOrDefaultAsync<LocacionKilometrosEntre>(query, new { IdKilometros = idKilometros }));
        }
    }
}