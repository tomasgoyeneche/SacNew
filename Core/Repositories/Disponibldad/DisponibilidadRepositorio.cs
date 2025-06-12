using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ORDER BY Tipo";

            var disponibles = await ConectarAsync(conn =>
                conn.QueryAsync<Disponibilidad>(query, new { Fecha = fecha.Date })
            );

            return disponibles.ToList();
        }

        public async Task<Disponible?> ObtenerDisponiblePorNominaYFechaAsync(int idNomina, DateTime fechaDisponible)
        {
            var query = "SELECT * FROM Disponible WHERE IdNomina = @idNomina AND FechaDisponible = @fecha AND IdDisponibleEstado = 1";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<Disponible>(query, new { idNomina, fecha = fechaDisponible }));
        }

        public async Task<List<int>> ObtenerCuposUsadosAsync(int idOrigen, DateTime fechaDisponible)
        {
            var query = "SELECT Cupo FROM Disponible WHERE IdOrigen = @idOrigen AND FechaDisponible = @fecha AND IdDisponibleEstado = 1";
            return (await ConectarAsync(conn => conn.QueryAsync<int>(query, new { idOrigen, fecha = fechaDisponible }))).ToList();
        }

        public async Task<List<DisponibleEstado>> ObtenerEstadosDeBajaAsync()
        {
            var query = "SELECT * FROM DisponibleEstado WHERE IdDisponibleEstado >= 5";
            return (await ConectarAsync(conn => conn.QueryAsync<DisponibleEstado>(query))).ToList();
        }

        public async Task AgregarDisponibleAsync(Disponible disp)
        {
            var query = @"INSERT INTO Disponible
            (IdNomina, FechaDisponible, IdOrigen, IdDestino, Cupo, Observaciones, IdDisponibleEstado, IdUsuario, Fecha)
            VALUES
            (@IdNomina, @FechaDisponible, @IdOrigen, @IdDestino, @Cupo, @Observaciones, @IdDisponibleEstado, @IdUsuario, @Fecha)";

            await ConectarAsync(async conn =>
            {
                await conn.ExecuteAsync(query, disp);
            });
        }

        public async Task ActualizarDisponibleAsync(Disponible disp)
        {
            var query = @"UPDATE Disponible SET
                IdOrigen = @IdOrigen,
                IdDestino = @IdDestino,
                Cupo = @Cupo,
                Observaciones = @Observaciones,
                IdDisponibleEstado = @IdDisponibleEstado,
                IdUsuario = @IdUsuario,
                Fecha = @Fecha
            WHERE IdDisponible = @IdDisponible";

            await ConectarAsync(async conn =>
            {
                await conn.ExecuteAsync(query, disp);
            });
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
            var query = "SELECT * FROM vw_DisponibilidadYPF WHERE dispoFecha = @dispoFecha";
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

