using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using Shared.Models.DTOs;

namespace Core.Repositories
{
    public class ConsumoOtrosRepositorio : BaseRepositorio, IConsumoOtrosRepositorio
    {
        public ConsumoOtrosRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
           : base(connectionStrings, sesionService) { }

        public async Task<List<ConsumosUnificadosDto>> ObtenerPorPocAsync(int idPoc)
        {
            return await ConectarAsync(async connection =>
            {
                const string query = "SELECT * FROM vw_ConsumosUnificados WHERE IdPoc = @IdPoc";
                var consumos = await connection.QueryAsync<ConsumosUnificadosDto>(query, new { IdPoc = idPoc });
                return consumos.ToList();
            });
        }

        public async Task AgregarConsumoAsync(ConsumoOtros consumoOtros)
        {
            var query = @"
            INSERT INTO ConsumoOtros
            (idPOC, idConsumo, NumeroVale, Cantidad,
             ImporteTotal, Aclaracion, Activo, FechaRemito)
            VALUES
            (@IdPOC, @IdConsumo, @NumeroVale, @Cantidad,
             @ImporteTotal, @Aclaracion, @Activo, @FechaRemito)";

            await EjecutarConAuditoriaAsync(
                async conn => await conn.ExecuteAsync(query, consumoOtros),
                "ConsumoOtros",
                "INSERT",
                null,
                consumoOtros
            );
        }

        public async Task EliminarConsumoAsync(int idConsumoOtros)
        {
            var query = "UPDATE ConsumoOtros SET Activo = 0 WHERE idConsumoOtros = @idConsumoOtros";

            var valoresAnteriores = await ObtenerPorIdAsync(idConsumoOtros);
            await EjecutarConAuditoriaAsync(
                async conn => await conn.ExecuteAsync(query, new { idConsumoOtros }),
                "ConsumoOtros",
                "DELETE",
                valoresAnteriores,
                null
            );
        }

        public async Task<ConsumoOtros?> ObtenerPorIdAsync(int idConsumoOtros)
        {
            var query = "SELECT * FROM ConsumoOtros WHERE idConsumoOtros = @idConsumoOtros";
            return await ConectarAsync(conn =>
                conn.QuerySingleOrDefaultAsync<ConsumoOtros>(query, new { idConsumoOtros })
            );
        }

        public async Task ActualizarConsumoAsync(ConsumoOtros consumo)
        {
            var query = @"
        UPDATE ConsumoOtros
        SET IdConsumo = @IdConsumo, NumeroVale = @NumeroVale, Cantidad = @Cantidad,
            ImporteTotal = @ImporteTotal, Aclaracion = @Aclaracion, FechaRemito = @FechaRemito
        WHERE IdConsumo = @IdConsumo";

            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, consumo),
                "ConsumoOtros",
                "UPDATE",
                consumo,
                consumo
            );
        }
    }
}