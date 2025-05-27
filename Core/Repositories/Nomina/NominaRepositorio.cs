using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

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

        public async Task<Nomina?> ObtenerPorIdAsync(int idNomina)
        {
            var query = "SELECT * FROM Nomina WHERE idNomina = @idNomina";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<Nomina>(query, new { IdNomina = idNomina }));
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
    }
}