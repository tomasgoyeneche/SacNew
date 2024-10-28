using Dapper;
using SacNew.Models;
using SacNew.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public class ImportVolvoConnectRepositorio : BaseRepositorio, IImportVolvoConnectRepositorio
    {
        public ImportVolvoConnectRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

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
