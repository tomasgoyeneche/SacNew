using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class UnidadMantenimientoRepositorio : BaseRepositorio, IUnidadMantenimientoRepositorio
    {
        public UnidadMantenimientoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<List<UnidadMantenimientoDto>> ObtenerNovedadesDto()
        {
            var query = @"
        SELECT *
        FROM vw_NovedadesUnidades
        WHERE FechaFin >= CAST(GETDATE() AS DATE) Order by FechaInicio"; // Comparación sin hora

            return await ConectarAsync(async connection =>
            {
                var novedadesUnidades = await connection.QueryAsync<UnidadMantenimientoDto>(query);
                return novedadesUnidades.ToList();
            });
        }

        public async Task<List<UnidadMantenimientoDto>> ObtenerTodasLasNovedadesDto()
        {
            var query = @"
        SELECT *
        FROM vw_NovedadesUnidades Order by FechaInicio"; // Comparación sin hora

            return await ConectarAsync(async connection =>
            {
                var novedadesUnidades = await connection.QueryAsync<UnidadMantenimientoDto>(query);
                return novedadesUnidades.ToList();
            });
        }

        public async Task<List<UnidadMantenimientoEstado>> ObtenerEstados()
        {
            var query = "SELECT * FROM UnidadMantenimientoEstado";

            return await ConectarAsync(async connection =>
            {
                var noverdadesChoferes = await connection.QueryAsync<UnidadMantenimientoEstado>(query);
                return noverdadesChoferes.ToList();
            });
        }

        public async Task AltaNovedadAsync(UnidadMantenimiento unidadMantenimimento, int idUsuario)
        {
            var query = @"
            DECLARE @idEstadoUnidadSito INT;

            INSERT INTO UnidadMantenimiento (idUnidad, idMantenimientoEstado, Observaciones, FechaInicio, FechaFin, Odometro, Activo)
            VALUES (@idUnidad, @idMantenimientoEstado, @Observaciones, @FechaInicio, @FechaFin, @Odometro, 1);

            SET @idEstadoUnidadSito = SCOPE_IDENTITY();

            INSERT INTO UnidadMantenimientoRegistro (
                idUnidadMantenimiento,
                idUnidad,
                Motivo,
                FechaInicio,
                FechaFin,
                Observaciones,
                idUsuario,
                Fecha,
                idMantenimientoEstado
            )
            VALUES (
                @idEstadoUnidadSito,
                @idUnidad,
                'Alta Mantenimiento',
                @FechaInicio,
                @FechaFin,
                @Observaciones,
                @idUsuario,
                GETDATE(),
                @idMantenimientoEstado
            );";

            await ConectarAsync(connection =>
            {
                var parametros = new
                {
                    unidadMantenimimento.IdUnidad,
                    unidadMantenimimento.IdMantenimientoEstado,
                    unidadMantenimimento.Observaciones,
                    unidadMantenimimento.FechaInicio,
                    unidadMantenimimento.FechaFin,
                    unidadMantenimimento.Odometro,
                    idUsuario
                };

                return connection.ExecuteAsync(query, parametros);
            });
        }

        public async Task EditarNovedadAsync(UnidadMantenimiento unidadMantenimiento, int idUsuario)
        {
            var query = @"
            UPDATE UnidadMantenimiento
            SET
                idUnidad = @idUnidad,
                idMantenimientoEstado = @idMantenimientoEstado,
                Observaciones = @Observaciones,
                FechaInicio = @FechaInicio,
                FechaFin = @FechaFin,
                Odometro = @Odometro
            WHERE idUnidadMantenimiento = @idUnidadMantenimiento;

            INSERT INTO UnidadMantenimientoRegistro (
                idUnidadMantenimiento,
                idUnidad,
                Motivo,
                FechaInicio,
                FechaFin,
                Observaciones,
                idUsuario,
                Fecha,
                idMantenimientoEstado
            )
            VALUES (
                @idUnidadMantenimiento,
                @idUnidad,
                'Editar Mantenimiento',
                @FechaInicio,
                @FechaFin,
                @Observaciones,
                @idUsuario,
                GETDATE(),
                @idMantenimientoEstado
            );";

            await ConectarAsync(connection =>
            {
                var parametros = new
                {
                    unidadMantenimiento.IdUnidadMantenimiento,
                    unidadMantenimiento.IdUnidad,
                    unidadMantenimiento.IdMantenimientoEstado,
                    unidadMantenimiento.Observaciones,
                    unidadMantenimiento.FechaInicio,
                    unidadMantenimiento.FechaFin,
                    unidadMantenimiento.Odometro,
                    idUsuario
                };

                return connection.ExecuteAsync(query, parametros);
            });
        }

        public async Task EliminarNovedadAsync(UnidadMantenimientoDto unidadMantenimiento, int idUsuario)
        {
            var query = @"
            UPDATE UnidadMantenimiento
            SET Activo = 0
            WHERE idUnidadMantenimiento = @idUnidadMantenimiento;

            INSERT INTO UnidadMantenimientoRegistro (
                idUnidadMantenimiento,
                idUnidad,
                Motivo,
                FechaInicio,
                FechaFin,
                Observaciones,
                idUsuario,
                Fecha,
                idMantenimientoEstado
            )
            VALUES (
                @idUnidadMantenimiento,
                @idUnidad,
                'Eliminó Mantenimiento',
                @FechaInicio,
                @FechaFin,
                @Observaciones,
                @idUsuario,
                GETDATE(),
                @idMantenimientoEstado
            );";

            await ConectarAsync(connection =>
            {
                var parametros = new
                {
                    unidadMantenimiento.idUnidadMantenimiento,
                    unidadMantenimiento.idUnidad,
                    unidadMantenimiento.idMantenimientoEstado,
                    unidadMantenimiento.Observaciones,
                    unidadMantenimiento.FechaInicio,
                    unidadMantenimiento.FechaFin,
                    idUsuario
                };

                return connection.ExecuteAsync(query, parametros);
            });
        }

        public async Task<List<UnidadMantenimientoDto>> ObtenerPorMesYAnioAsync(int mes, int anio)
        {
            DateTime inicioMes = new DateTime(anio, mes, 1);
            DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);

            var query = @"
        SELECT *
        FROM vw_NovedadesUnidades
        WHERE FechaInicio <= @finMes
          AND FechaFin >= @inicioMes
        ORDER BY Tractor, Dias";

            return await ConectarAsync(async connection =>
            {
                var parametros = new DynamicParameters();
                parametros.Add("@inicioMes", inicioMes);
                parametros.Add("@finMes", finMes);

                var novedades = await connection.QueryAsync<UnidadMantenimientoDto>(query, parametros);
                return novedades.ToList();
            });
        }

        public async Task<List<UnidadMantenimientoDto>> ObtenerPorAnioAsync(int anio)
        {
            var query = @"
        SELECT *
        FROM vw_NovedadesUnidades
        WHERE YEAR(FechaInicio) <= @Anio AND YEAR(FechaFin) >= @Anio";

            return await ConectarAsync(async connection =>
            {
                var novedades = await connection.QueryAsync<UnidadMantenimientoDto>(query, new { Anio = anio });
                return novedades.ToList();
            });
        }

        public async Task<List<UnidadMantenimientoDto>> ObtenerPorUnidadAsync(int idUnidad)
        {
            var query = @"
        SELECT *
        FROM vw_NovedadesUnidades
        WHERE idUnidad = @idUnidad and FechaFin >= CAST(GETDATE() AS DATE)";

            return await ConectarAsync(async connection =>
            {
                var novedades = await connection.QueryAsync<UnidadMantenimientoDto>(query, new { IdUnidad = idUnidad });
                return novedades.ToList();
            });
        }
    }
}