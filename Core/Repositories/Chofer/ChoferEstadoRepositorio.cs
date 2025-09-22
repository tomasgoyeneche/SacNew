using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class ChoferEstadoRepositorio : BaseRepositorio, IChoferEstadoRepositorio
    {
        public ChoferEstadoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<List<NovedadesChoferesDto>> ObtenerNovedadesDto()
        {
            var query = @"
        SELECT *
        FROM vw_NovedadesChoferes
        WHERE FechaFin >= CAST(GETDATE() AS DATE) Order by FechaInicio desc"; // Comparación sin hora

            return await ConectarAsync(async connection =>
            {
                var novedadesChoferes = await connection.QueryAsync<NovedadesChoferesDto>(query);
                return novedadesChoferes.ToList();
            });
        }

        public async Task<List<NovedadesChoferesDto>> ObtenerTodasLasNovedadesDto()
        {
            var query = @"
        SELECT *
        FROM vw_NovedadesChoferes Order by FechaInicio desc"; // Comparación sin hora

            return await ConectarAsync(async connection =>
            {
                var novedadesChoferes = await connection.QueryAsync<NovedadesChoferesDto>(query);
                return novedadesChoferes.ToList();
            });
        }

        public async Task<List<ChoferTipoEstado>> ObtenerEstados()
        {
            var query = "SELECT * FROM ChoferTipoEstado";

            return await ConectarAsync(async connection =>
            {
                var noverdadesChoferes = await connection.QueryAsync<ChoferTipoEstado>(query);
                return noverdadesChoferes.ToList();
            });
        }

        public async Task AltaNovedadAsync(ChoferEstado choferEstado, int idUsuario)
        {
            var query = @"
            DECLARE @idEstadoChoferSito INT;

            INSERT INTO ChoferEstado (idChofer, idEstado, Observaciones, FechaInicio, FechaFin, Disponible, Activo)
            VALUES (@idChofer, @idEstado, @Observaciones, @FechaInicio, @FechaFin, @Disponible, 1);

            SET @idEstadoChoferSito = SCOPE_IDENTITY();

            INSERT INTO ChoferEstadoRegistro (
                idEstadoChofer,
                idChofer,
                Motivo,
                FechaInicio,
                FechaFin,
                Observaciones,
                idUsuario,
                Fecha,
                idEstado
            )
            VALUES (
                @idEstadoChoferSito,
                @idChofer,
                'Alta Novedad',
                @FechaInicio,
                @FechaFin,
                @Observaciones,
                @idUsuario,
                GETDATE(),
                @idEstado
            );";

            await ConectarAsync(connection =>
            {
                var parametros = new
                {
                    choferEstado.IdChofer,
                    choferEstado.IdEstado,
                    choferEstado.Observaciones,
                    choferEstado.FechaInicio,
                    choferEstado.FechaFin,
                    choferEstado.Disponible,
                    idUsuario
                };

                return connection.ExecuteAsync(query, parametros);
            });
        }

        public async Task EditarNovedadAsync(ChoferEstado choferEstado, int idUsuario)
        {
            var query = @"
            UPDATE ChoferEstado
            SET
                idChofer = @idChofer,
                idEstado = @idEstado,
                Observaciones = @Observaciones,
                FechaInicio = @FechaInicio,
                FechaFin = @FechaFin,
                Disponible = @Disponible
            WHERE idEstadoChofer = @idEstadoChofer;

            INSERT INTO ChoferEstadoRegistro (
                idEstadoChofer,
                idChofer,
                Motivo,
                FechaInicio,
                FechaFin,
                Observaciones,
                idUsuario,
                Fecha,
                idEstado
            )
            VALUES (
                @idEstadoChofer,
                @idChofer,
                'Editar Ausencia',
                @FechaInicio,
                @FechaFin,
                @Observaciones,
                @idUsuario,
                GETDATE(),
                @idEstado
            );";

            await ConectarAsync(connection =>
            {
                var parametros = new
                {
                    choferEstado.IdEstadoChofer,
                    choferEstado.IdChofer,
                    choferEstado.IdEstado,
                    choferEstado.Observaciones,
                    choferEstado.FechaInicio,
                    choferEstado.FechaFin,
                    choferEstado.Disponible,
                    idUsuario
                };

                return connection.ExecuteAsync(query, parametros);
            });
        }

        public async Task EliminarNovedadAsync(NovedadesChoferesDto choferEstado, int idUsuario)
        {
            var query = @"
            UPDATE ChoferEstado
            SET Activo = 0
            WHERE idEstadoChofer = @idEstadoChofer;

            INSERT INTO ChoferEstadoRegistro (
                idEstadoChofer,
                idChofer,
                Motivo,
                FechaInicio,
                FechaFin,
                Observaciones,
                idUsuario,
                Fecha,
                idEstado
            )
            VALUES (
                @idEstadoChofer,
                @idChofer,
                'Eliminó Ausencia',
                @FechaInicio,
                @FechaFin,
                @Observaciones,
                @idUsuario,
                GETDATE(),
                @idEstado
            );";

            await ConectarAsync(connection =>
            {
                var parametros = new
                {
                    choferEstado.idEstadoChofer,
                    choferEstado.idChofer,
                    choferEstado.idEstado,
                    choferEstado.Observaciones,
                    choferEstado.FechaInicio,
                    choferEstado.FechaFin,
                    idUsuario
                };

                return connection.ExecuteAsync(query, parametros);
            });
        }

        public async Task<List<NovedadesChoferesDto>> ObtenerPorMesYAnioAsync(int mes, int anio)
        {
            DateTime inicioMes = new DateTime(anio, mes, 1);
            DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);

            var query = @"
        SELECT *
        FROM vw_NovedadesChoferes
        WHERE FechaInicio <= @finMes
          AND FechaFin >= @inicioMes
        ORDER BY NombreCompleto, Dias";

            return await ConectarAsync(async connection =>
            {
                var parametros = new DynamicParameters();
                parametros.Add("@inicioMes", inicioMes);
                parametros.Add("@finMes", finMes);

                var novedades = await connection.QueryAsync<NovedadesChoferesDto>(query, parametros);
                return novedades.ToList();
            });
        }

        public async Task<List<NovedadesChoferesDto>> ObtenerPorAnioAsync(int anio)
        {
            var query = @"
        SELECT *
        FROM vw_NovedadesChoferes
        WHERE YEAR(FechaInicio) <= @Anio AND YEAR(FechaFin) >= @Anio";

            return await ConectarAsync(async connection =>
            {
                var novedades = await connection.QueryAsync<NovedadesChoferesDto>(query, new { Anio = anio });
                return novedades.ToList();
            });
        }

        public async Task<List<NovedadesChoferesDto>> ObtenerPorChoferAsync(int idChofer)
        {
            var query = @"
        SELECT *
        FROM vw_NovedadesChoferes
          WHERE idChofer = @idChofer and FechaFin >= CAST(GETDATE() AS DATE)";

            return await ConectarAsync(async connection =>
            {
                var novedades = await connection.QueryAsync<NovedadesChoferesDto>(query, new { IdChofer = idChofer });
                return novedades.ToList();
            });
        }
    }
}