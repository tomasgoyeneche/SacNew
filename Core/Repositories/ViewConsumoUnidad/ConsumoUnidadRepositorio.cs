using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class ConsumoUnidadRepositorio : BaseRepositorio, IConsumoUnidadRepositorio
    {
        public ConsumoUnidadRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<List<InformeConsumoUnidad>> ObtenerConsumosPorPeriodoAsync(int idPeriodo)
        {
            var query = @"
        EXEC sp_ImportInformeConsumos @idPeriodo";

            return await ConectarAsync(async conn =>
            {
                var consumos = await conn.QueryAsync<InformeConsumoUnidad>(
                    query, new { idPeriodo }
                );
                return consumos.ToList();
            });
        }

        public async Task GuardarConsumoAsync(InformeConsumoUnidad consumo)
        {
            var query = @"
        INSERT INTO InformeConsumoUnidad
        (PatenteTractor, ConsumoLitrosMes, KilometrosRecorridos, LitrosCienKilometros,
         LitrosTotalesYPF, PorcentajeYPF, LitrosTotalesMercadoVictoria, PorcentajeMercadoVictoria,
         LitrosTotales, DiferenciaLitros, PorcentajeDiferencia, LitrosConsumidosReales,
         DiferenciaLitrosTeoricosReales, PorcentajeDiferenciaLitrosReales, IdPeriodo)
        VALUES
        (@PatenteTractor, @ConsumoLitrosMes, @KilometrosRecorridos, @LitrosCienKilometros,
         @LitrosTotalesYPF, @PorcentajeYPF, @LitrosTotalesMercadoVictoria, @PorcentajeMercadoVictoria,
         @LitrosTotales, @DiferenciaLitros, @PorcentajeDiferencia, @LitrosConsumidosReales,
         @DiferenciaLitrosTeoricosReales, @PorcentajeDiferenciaLitrosReales, @IdPeriodo)";

            await ConectarAsync(async conn =>
            {
                await conn.ExecuteAsync(query, consumo);
            });
        }
    }
}