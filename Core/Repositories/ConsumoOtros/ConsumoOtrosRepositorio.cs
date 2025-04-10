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
             ImporteTotal, Aclaracion, Activo, FechaRemito, Dolar)
            VALUES
            (@IdPOC, @IdConsumo, @NumeroVale, @Cantidad,
             @ImporteTotal, @Aclaracion, @Activo, @FechaRemito, @Dolar)";

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
            ImporteTotal = @ImporteTotal, Aclaracion = @Aclaracion, FechaRemito = @FechaRemito, Dolar = @Dolar
        WHERE IdConsumoOtros = @IdConsumoOtros";

            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, consumo),
                "ConsumoOtros",
                "UPDATE",
                consumo,
                consumo
            );
        }

        public async Task<List<InformeConsumoPocDto>> ObtenerPorFechaCargaAsync(DateTime fechaCarga, int idPosta)
        {
            var query = @"
        SELECT * 
        FROM vw_InformeConsumoPoc 
        WHERE CAST(fechacarga AS DATE) = @fechaCarga and idPosta = @idPosta";

            return await ConectarAsync(conn =>
                conn.QueryAsync<InformeConsumoPocDto>(query, new { fechaCarga = fechaCarga.Date, idPosta })
            ).ContinueWith(t => t.Result.ToList());
        }

        public async Task<List<InformeConsumoPocDto>> BuscarConsumosAsync(
                int? idConcepto,
                int? idPosta,
                int? idEmpresa,
                int? idUnidad,
                int? idChofer,
                string numeroPOC,
                string estado,
                DateTime? fechaCreacionDesde,
                DateTime? fechaCreacionHasta,
                DateTime? fechaCierreDesde,
                DateTime? fechaCierreHasta)
        {
            var filtros = new Dictionary<string, object?>
                {
                    { "idconsumo", idConcepto },
                    { "idposta", idPosta },
                    { "idempresa", idEmpresa },
                    { "idunidad", idUnidad },
                    { "idchofer", idChofer },
                    { "numeropoc_like", string.IsNullOrWhiteSpace(numeroPOC) ? null : numeroPOC },
                    { "estado", estado == "Todas" ? null : estado },
                    { "fechacreacion_desde", fechaCreacionDesde },
                    { "fechacreacion_hasta", fechaCreacionHasta },
                    { "fechacierre_desde", fechaCierreDesde },
                    { "fechacierre_hasta", fechaCierreHasta },
                };

            var (whereClause, parametros) = ConstruirFiltroDinamico(filtros);

            var query = $"SELECT * FROM vw_InformeConsumoPoc {whereClause}";

            return await ConectarAsync(async connection =>
            {
                var resultados = await connection.QueryAsync<InformeConsumoPocDto>(query, parametros);
                return resultados.ToList();
            });
        }
    }
}