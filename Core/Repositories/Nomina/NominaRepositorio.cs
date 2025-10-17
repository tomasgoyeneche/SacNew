using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using System.Data;

namespace Core.Repositories
{
    public class NominaRepositorio : BaseRepositorio, INominaRepositorio
    {
        public NominaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<Nomina?> ObtenerNominaActivaPorUnidadAsync(int idUnidad, DateTime fechaReferencia)
        {
            var query = @"
            SELECT TOP 1 *
            FROM Nomina
            WHERE IdUnidad = @idUnidad
              AND FechaAlta <= @fecha
              AND (FechaBaja IS NULL OR FechaBaja >= @fecha)";

            return await ConectarAsync(conn =>
                conn.QueryFirstOrDefaultAsync<Nomina>(query, new
                {
                    idUnidad,
                    fecha = fechaReferencia
                })
            );
        }

        public async Task<decimal?> ObtenerOdometerPorNomina(int idNomina)
        {
            // Busca el odometer más reciente ANTES o IGUAL a la fecha indicada
            var sql = @"
        SELECT TOP 1 odometer
        FROM wsSitrackNomina
        WHERE idNomina = @idNomina";
            return await ConectarAsync(async conn =>
                await conn.ExecuteScalarAsync<decimal?>(sql, new { idNomina })
            );
        }

        public async Task<Nomina?> ObtenerNominaActivaPorChoferAsync(int idChofer, DateTime fechaReferencia)
        {
            var query = @"
            SELECT TOP 1 *
            FROM Nomina
            WHERE idChofer = @idChofer
              AND FechaAlta <= @fecha
              AND (FechaBaja IS NULL OR FechaBaja >= @fecha)";

            return await ConectarAsync(conn =>
                conn.QueryFirstOrDefaultAsync<Nomina>(query, new
                {
                    idChofer,
                    fecha = fechaReferencia
                })
            );
        }

        public async Task<List<HistorialGeneralDto>> ObtenerHistorialPorNomina(int idNomina)
        {
            const string query = "SELECT * FROM vw_HistorialGeneral WHERE IdNomina = @idNomina  order by Fecha desc";

            return await ConectarAsync(conn =>
                conn.QueryAsync<HistorialGeneralDto>(query, new { idNomina }))
                .ContinueWith(t => t.Result.ToList());
        }

        public async Task<Nomina?> ObtenerPorIdAsync(int idNomina)
        {
            var query = "SELECT * FROM Nomina WHERE idNomina = @idNomina";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<Nomina>(query, new { IdNomina = idNomina }));
        }

        public async Task<Nomina?> ObtenerNominaMasNuevaPorChofer(int idChofer)
        {
            var query = "SELECT * FROM Nomina WHERE IdChofer = @IdChofer order by fechaalta desc";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<Nomina>(query, new { IdChofer = idChofer }));
        }

        public async Task<Nomina?> ObtenerNominaMasNuevaPorUnidad(int idUnidad)
        {
            var query = "SELECT * FROM Nomina WHERE IdUnidad = @IdUnidad order by fechaalta desc";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<Nomina>(query, new { IdUnidad = idUnidad }));
        }



        public async Task<List<VencimientosDto>> ObtenerVencimientosPorNominaAsync(Nomina nomina)
        {
            var vencimientos = new List<VencimientosDto>();

            const string queryUnidad = "SELECT * FROM vw_vencimientosunidad WHERE Entidad = @idUnidad";
            const string queryChofer = "SELECT * FROM vw_vencimientoschofer WHERE Entidad = @idChofer";

            var vencUnidad = await ConectarAsync(conn =>
                conn.QueryAsync<VencimientosDto>(queryUnidad, new { idUnidad = nomina.IdUnidad }));

            vencimientos.AddRange(vencUnidad);

            if (nomina.IdChofer != 0)
            {
                var vencChofer = await ConectarAsync(conn =>
                    conn.QueryAsync<VencimientosDto>(queryChofer, new { idChofer = nomina.IdChofer }));

                vencimientos.AddRange(vencChofer);
            }

            return vencimientos;
        }

        public async Task RegistrarNominaAsync(int idNomina, string evento, string descripcion, int idUsuario)
        {
            var query = @"INSERT INTO NominaRegistro (idNomina, Evento, Descripcion, idUsuario, Fecha)
                  VALUES (@idNomina, @Evento, @Descripcion, @idUsuario, GETDATE())";

            await ConectarAsync(async conn =>
            {
                await conn.ExecuteAsync(query, new
                {
                    idNomina,
                    Evento = evento,
                    Descripcion = descripcion,
                    idUsuario
                });
            });
        }

        public async Task CambiarChoferUnidadAsync(int? idChofer, int idUnidad, DateTime fecha, string Observaciones)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@idChofer", idChofer);
            parameters.Add("@idUnidad", idUnidad);
            parameters.Add("@fecha", fecha);
            parameters.Add("@Observacion", Observaciones);

            await ConectarAsync(async conn =>
            {
                await conn.ExecuteAsync("sp_CambiarChoferUnidad", parameters, commandType: CommandType.StoredProcedure);
            });
        }
    }
}