using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class ConsumoGasoilRepositorio : BaseRepositorio, IConsumoGasoilRepositorio
    {
        public ConsumoGasoilRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<ConsumoGasoil>> ObtenerPorPOCAsync(int idPoc)
        {
            var query = "SELECT * FROM ConsumoGasoil WHERE idPOC = @idPoc AND Activo = 1";
            return (await ConectarAsync(conn =>
                conn.QueryAsync<ConsumoGasoil>(query, new { idPoc })
            )).ToList();
        }

        public async Task AgregarConsumoAsync(ConsumoGasoil consumoGasoil)
        {
            var query = @"
            INSERT INTO ConsumoGasoil
            (idPOC, idConsumo, NumeroVale, LitrosAutorizados, idPrograma, LitrosCargados,
             PrecioTotal, Observaciones, Activo, FechaCarga, Dolar, TransitoEspecial)
            VALUES
            (@IdPOC, @IdConsumo, @NumeroVale, @LitrosAutorizados, @idPrograma, @LitrosCargados,
             @PrecioTotal, @Observaciones, @Activo, @FechaCarga, @Dolar, @TransitoEspecial)";

            await EjecutarConAuditoriaAsync(
                async conn => await conn.ExecuteAsync(query, consumoGasoil),
                "ConsumoGasoil",
                "INSERT",
                null,
                consumoGasoil
            );
        }

        public async Task ActualizarConsumoAsync(ConsumoGasoil consumo)
        {
            var query = @"
        UPDATE ConsumoGasoil
        SET IdConsumo = @IdConsumo, NumeroVale = @NumeroVale, LitrosCargados = @LitrosCargados,
            PrecioTotal = @PrecioTotal, Observaciones = @Observaciones, FechaCarga = @FechaCarga, Dolar = @Dolar, TransitoEspecial = @TransitoEspecial
        WHERE IdConsumoGasoil = @IdConsumoGasoil";

            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, consumo),
                "ConsumoGasoil",
                "UPDATE",
                consumo,
                consumo
            );
        }

        public async Task EliminarConsumoAsync(int idConsumoGasoil)
        {
            var query = "UPDATE ConsumoGasoil SET Activo = 0 WHERE idConsumoGasoil = @idConsumoGasoil";

            var valoresAnteriores = await ObtenerPorIdAsync(idConsumoGasoil);
            await EjecutarConAuditoriaAsync(
                async conn => await conn.ExecuteAsync(query, new { idConsumoGasoil }),
                "ConsumoGasoil",
                "DELETE",
                valoresAnteriores,
                null
            );
        }

        public async Task<ConsumoGasoil?> ObtenerPorIdAsync(int idConsumoGasoil)
        {
            var query = "SELECT * FROM ConsumoGasoil WHERE idConsumoGasoil = @idConsumoGasoil";
            return await ConectarAsync(conn =>
                conn.QuerySingleOrDefaultAsync<ConsumoGasoil>(query, new { idConsumoGasoil })
            );
        }

        public async Task<decimal> ObtenerLitrosCargadosPorProgramaAsync(int idPrograma)
        {
            return await ConectarAsync(async connection =>
            {
                const string query = @"
                SELECT ISNULL(SUM(LitrosCargados), 0)
                FROM ConsumoGasoil
                WHERE IdPrograma = @IdPrograma AND Activo = 1";

                return await connection.ExecuteScalarAsync<decimal>(query, new { IdPrograma = idPrograma });
            });
        }

        public async Task<(int IdPrograma, decimal Kilometros, string Destino)?>
         ObtenerProgramaPorPatenteAsync(string patenteTractor, DateTime fechaCreacion)
            {
                return await ConectarAsync(async connection =>
                {
                    const string query = @"
        SELECT TOP 1 
            IdPrograma, 
            Kilometros,
            Destino
        FROM vw_ProgramaCombustible
        WHERE PatenteTractor = @PatenteTractor
          AND (
              (@FechaCreacion >= FechaCarga AND @FechaCreacion < SalidaEntrega)
              OR
              (@FechaCreacion >= FechaCarga AND SalidaEntrega IS NULL)
              OR
              (FechaCarga > @FechaCreacion)
          )
        ORDER BY
            CASE 
                WHEN @FechaCreacion >= FechaCarga AND SalidaEntrega IS NULL THEN 1
                WHEN @FechaCreacion >= FechaCarga AND @FechaCreacion < SalidaEntrega THEN 2
                WHEN FechaCarga > @FechaCreacion THEN 3
            END,
            ABS(DATEDIFF(SECOND, FechaCarga, @FechaCreacion))";
                return await connection.QuerySingleOrDefaultAsync<(int, decimal, string)?>(query,
                    new
                    {
                        PatenteTractor = patenteTractor,
                        FechaCreacion = fechaCreacion
                    });
            });
        }

        public async Task<List<ConsumoGasoilAutorizadoDto>> ObtenerConsumosPorProgramaAsync(int idPrograma, string patente)
        {
            return await ConectarAsync(async connection =>
            {
                const string query = @"
                SELECT IdConsumoGasoil, NumeroPoc, NumeroVale, IdPrograma, LitrosAutorizados, LitrosCargados, Observaciones, FechaCarga
                FROM vw_ConsumoGasoilAutorizadoActivo
                WHERE IdPrograma = @IdPrograma and patente = @patente";

                return (await connection.QueryAsync<ConsumoGasoilAutorizadoDto>(query, new { IdPrograma = idPrograma, patente = patente })).ToList();
            });
        }

        public async Task<List<ConsumoGasoilAutorizadoDto>> ObtenerConsumosPorProgramaEditableAsync(int idConsumo, int idPrograma, string patente)
        {
            return await ConectarAsync(async connection =>
            {
                const string query = @"
                SELECT IdConsumoGasoil, NumeroPoc, NumeroVale, IdPrograma, LitrosAutorizados, LitrosCargados, Observaciones, FechaCarga
                FROM vw_ConsumoGasoilAutorizadoActivo
                WHERE IdPrograma = @IdPrograma and patente = @patente and IdConsumoGasoil != @IdConsumo";

                return (await connection.QueryAsync<ConsumoGasoilAutorizadoDto>(query, new { IdPrograma = idPrograma, patente = patente, IdConsumo = idConsumo })).ToList();
            });
        }

        public async Task<int?> ObtenerIdProgramaAnteriorAsync(string patente, int idProgramaActual)
        {
            return await ConectarAsync(async connection =>
            {
                const string query = @"
                SELECT TOP 1 IdPrograma
                FROM vw_ConsumoGasoilAutorizadoActivo
                WHERE Patente = @Patente AND IdPrograma < @IdProgramaActual
                ORDER BY IdPrograma DESC";

                return await connection.QueryFirstOrDefaultAsync<int?>(query, new
                {
                    Patente = patente,
                    IdProgramaActual = idProgramaActual
                });
            });
        }

        public async Task<ConsumoGasoilAutorizadoDto?> ObtenerUltimoConsumoPorPatenteAsync(string patente)
        {
            return await ConectarAsync(async connection =>
            {
                const string query = @"
                SELECT TOP 1 *
                FROM vw_ConsumoGasoilAutorizadoActivo
                WHERE Patente = @Patente
                ORDER BY FechaCarga DESC";

                return await connection.QuerySingleOrDefaultAsync<ConsumoGasoilAutorizadoDto>(query, new { Patente = patente });
            });
        }

        public async Task<List<ConsumoGasoilAutorizadoDto>> ObtenerConsumosUltimosDosMesesDesdeFechaAsync(
            string patente,
            int idProgramaActual,
            DateTime fechaBase)
        {
            var fechaDesde = fechaBase.AddMonths(-2);

            return await ConectarAsync(async connection =>
            {
                const string query = @"
                SELECT IdConsumoGasoil, NumeroPoc, NumeroVale, IdPrograma,
                       LitrosAutorizados, LitrosCargados, Observaciones, FechaCarga
                FROM vw_ConsumoGasoilAutorizadoActivo
                WHERE Patente = @Patente
                  AND FechaCarga BETWEEN @FechaDesde AND @FechaHasta
                  AND (@IdProgramaActual = 0 OR IdPrograma <> @IdProgramaActual)
                ORDER BY IdConsumoGasoil DESC";

                return (await connection.QueryAsync<ConsumoGasoilAutorizadoDto>(query, new
                {
                    Patente = patente,
                    IdProgramaActual = idProgramaActual,
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaBase
                })).ToList();
            });
        }
    }
}