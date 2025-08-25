using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class DisponibilidadRepositorio : BaseRepositorio, IDisponibilidadRepositorio
    {
        public DisponibilidadRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<List<Disponibilidad>> BuscarDisponiblesPorFechaAsync(DateTime fecha)
        {
            var query = @"
        SELECT *
        FROM vw_Disponibilidad
        WHERE DispoFecha = @Fecha
          AND Tipo <> 'X'
        ORDER BY Tipo
        OPTION (RECOMPILE)"; // <<--- esto soluciona el problema de los tiempos variables

            var disponibles = await ConectarAsync(conn =>
                conn.QueryAsync<Disponibilidad>(query, new { Fecha = fecha.Date }, commandTimeout: 120)
            );

            return disponibles.ToList();
        }

        public async Task<Disponible?> ObtenerDisponiblePorNominaYFechaAsync(int idNomina, DateTime fechaDisponible)
        {
            var query = "SELECT * FROM Disponible WHERE IdNomina = @idNomina AND FechaDisponible = @fecha AND IdDisponibleEstado = 1";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<Disponible>(query, new { idNomina, fecha = fechaDisponible }));
        }

        public Task<Disponible?> ObtenerPorIdAsync(int idDisponible)
        {
            return ObtenerPorIdGenericoAsync<Disponible>("Disponible", "IdDisponible", idDisponible);
        }

        public async Task<List<int>> ObtenerCuposUsadosAsync(int idOrigen, DateTime fechaDisponible)
        {
            var query = "SELECT Cupo FROM Disponible WHERE IdOrigen = @idOrigen AND FechaDisponible = @fecha AND IdDisponibleEstado = 1";
            return (await ConectarAsync(conn => conn.QueryAsync<int>(query, new { idOrigen, fecha = fechaDisponible }))).ToList();
        }

        public async Task<List<DisponibleEstado>> ObtenerEstadosDeBajaAsync()
        {
            var query = "SELECT * FROM DisponibleEstado WHERE IdDisponibleEstado >= 5 and Activo = 1";
            return (await ConectarAsync(conn => conn.QueryAsync<DisponibleEstado>(query))).ToList();
        }

        public Task AgregarDisponibleAsync(Disponible disp)
        {
            return AgregarGenéricoAsync("Disponible", disp);
        }

        public Task ActualizarDisponibleAsync(Disponible disp)
        {
            return ActualizarGenéricoAsync("Disponible", disp);
        }

        public async Task<List<DisponibleFecha>> ObtenerProximasFechasDisponiblesAsync(DateTime desde, int cantidad)
        {
            var query = @"
                SELECT TOP (@cantidad) IdDisponibleFecha, dispoFecha
                FROM DisponibleFecha
                WHERE dispoFecha >= @desde
                ORDER BY dispoFecha
            ";
            return (await ConectarAsync(conn =>
                conn.QueryAsync<DisponibleFecha>(query, new { desde, cantidad }))).ToList();
        }

        public async Task<List<DisponibilidadYPF>> ObtenerDisponibilidadYPFPorFechaAsync(DateTime dispoFecha)
        {
            var query = "SELECT * FROM vw_DisponibilidadYPF WHERE Fecha = @dispoFecha Order by Estado, Origen, obsYPF";
            return (await ConectarAsync(conn => conn.QueryAsync<DisponibilidadYPF>(query, new { dispoFecha }))).ToList();
        }

        public async Task<DisponibleEstado?> ObtenerEstadoDeBajaPorIdAsync(int idMotivo)
        {
            var query = "SELECT * FROM DisponibleEstado WHERE IdDisponibleEstado = @idMotivo";
            return await ConectarAsync(conn =>
                conn.QueryFirstOrDefaultAsync<DisponibleEstado>(query, new { idMotivo }));
        }
    }
}