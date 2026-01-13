using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class ImportConsumoYpfRepositorio : BaseRepositorio, IImportConsumoYpfRepositorio
    {
        public ImportConsumoYpfRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> AgregarConsumoAsync(ImportConsumoYpfEnRuta consumo)
        {
            return await AgregarGenéricoAsync("ImportConsumoYPFEnRuta", consumo);
        }

        public async Task<int> ActualizarConsumoAsync(ImportConsumoYpfEnRuta consumo)
        {
            return await ActualizarGenéricoAsync("ImportConsumoYPFEnRuta", consumo);
        }

        public async Task<IEnumerable<ImportConsumoYpfEnRuta>> ObtenerPorPeriodoAsync(int idPeriodo)
        {
            var query = "SELECT * FROM ImportConsumoYpfEnRuta WHERE IdPeriodo = @IdPeriodo";
            var resultado = await ConectarAsync(conn =>
                conn.QueryAsync<ImportConsumoYpfEnRuta>(query, new { idPeriodo })
            );
            return resultado.ToList();  // Convertimos el resultado a una lista.
        }

        public async Task EliminarConsumosPorPeriodoAsync(int idPeriodo)
        {
            var query = "DELETE FROM ImportConsumoYpfEnRuta WHERE IdPeriodo = @IdPeriodo";
            await ConectarAsync(conn => conn.ExecuteAsync(query, new { IdPeriodo = idPeriodo }));
        }

        public async Task<bool> ExistenConsumosParaPeriodoAsync(int idPeriodo)
        {
            var query = "SELECT COUNT(1) FROM ImportConsumoYpfEnRuta WHERE IdPeriodo = @idPeriodo";
            return await ConectarAsync(conn =>
                conn.ExecuteScalarAsync<int>(query, new { idPeriodo })
            ) > 0;
        }
    }
}