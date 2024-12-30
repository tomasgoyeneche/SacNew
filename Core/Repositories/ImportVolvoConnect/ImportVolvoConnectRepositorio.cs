using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class ImportVolvoConnectRepositorio : BaseRepositorio, IImportVolvoConnectRepositorio
    {
        public ImportVolvoConnectRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task AgregarImportacionAsync(ImportVolvoConnect importacion)
        {
            var query = @"
        INSERT INTO ImportVolvoConnect
        (IdUnidad, Kilometros, PromedioGasoilEnMarcha, GasoilEnMarcha,
         PromedioGasoilEnConduccion, GasoilEnConduccion, IdPeriodo)
        VALUES
        (@IdUnidad, @Kilometros, @PromedioGasoilEnMarcha, @GasoilEnMarcha,
         @PromedioGasoilEnConduccion, @GasoilEnConduccion, @IdPeriodo)";

            await ConectarAsync(conn => conn.ExecuteAsync(query, importacion));
        }

        public async Task<bool> ExistenDatosParaPeriodoAsync(int idPeriodo)
        {
            var query = "SELECT COUNT(1) FROM ImportVolvoConnect WHERE IdPeriodo = @idPeriodo";
            return await ConectarAsync(conn =>
                conn.ExecuteScalarAsync<int>(query, new { idPeriodo })
            ) > 0;
        }

        public async Task<List<ImportVolvoConnect>> ObtenerPorPeriodoAsync(int idPeriodo)
        {
            var query = "SELECT * FROM ImportVolvoConnect WHERE IdPeriodo = @idPeriodo";
            var resultado = await ConectarAsync(conn =>
                conn.QueryAsync<ImportVolvoConnect>(query, new { idPeriodo })
            );
            return resultado.ToList();  // Convertimos el resultado a una lista.
        }
    }
}