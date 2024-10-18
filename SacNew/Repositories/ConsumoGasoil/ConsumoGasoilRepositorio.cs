using Dapper;
using SacNew.Models;
using SacNew.Services;

namespace SacNew.Repositories
{
    public class ConsumoGasoilRepositorio : BaseRepositorio, IConsumoGasoilRepositorio
    {
        public ConsumoGasoilRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

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
            (idPOC, idConsumo, NumeroVale, LitrosAutorizados, LitrosCargados,
             PrecioTotal, Observaciones, Activo, FechaCarga)
            VALUES
            (@IdPOC, @IdConsumo, @NumeroVale, @LitrosAutorizados, @LitrosCargados,
             @PrecioTotal, @Observaciones, @Activo, @FechaCarga)";

            await EjecutarConAuditoriaAsync(
                async conn => await conn.ExecuteAsync(query, consumoGasoil),
                "ConsumoGasoil",
                "INSERT",
                null,
                consumoGasoil
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
    }
}