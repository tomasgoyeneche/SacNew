using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class ImportMercadoVictoriaRepositorio : BaseRepositorio, IImportMercadoVictoriaRepositorio
    {
        public ImportMercadoVictoriaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<ImportMercadoVictoria>> ObtenerPorPeriodoAsync(int idPeriodo)
        {
            var query = "SELECT * FROM importMercadoVictoria WHERE idPeriodo = @idPeriodo";
            return (await ConectarAsync(conn =>
                conn.QueryAsync<ImportMercadoVictoria>(query, new { idPeriodo })
            )).ToList();
        }

        public async Task AgregarConsumoAsync(ImportMercadoVictoria consumo)
        {
            var query = @"
        INSERT INTO importMercadoVictoria
        (idUnidad, Fecha, Litros, idConsumo, idPeriodo)
        VALUES
        (@IdUnidad, @Fecha, @Litros, @IdConsumo, @IdPeriodo)";

            await EjecutarConAuditoriaAsync(
                async conn => await conn.ExecuteAsync(query, consumo),
                "importMercadoVictoria",
                "INSERT",
                null,
                consumo
            );
        }

        public async Task<bool> ExistenDatosParaPeriodoAsync(int idPeriodo)
        {
            var query = "SELECT COUNT(1) FROM importMercadoVictoria WHERE IdPeriodo = @idPeriodo";
            return await ConectarAsync(conn =>
                conn.ExecuteScalarAsync<int>(query, new { idPeriodo })
            ) > 0;
        }
    }
}