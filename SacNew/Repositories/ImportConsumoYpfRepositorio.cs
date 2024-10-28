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
    public class ImportConsumoYpfRepositorio : BaseRepositorio, IImportConsumoYpfRepositorio
    {
        public ImportConsumoYpfRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

        public async Task AgregarConsumoAsync(ImportConsumoYpfEnRuta consumo)
        {
            var query = @"
        INSERT INTO ImportConsumoYpfEnRuta 
        (FechaHora, Localidad, Tarjeta, idChofer, idUnidad, Remito, 
         idConsumo, Litros, ImporteTotalYer, ImporteSinImpuestos, Factura, IdPeriodo)
        VALUES 
        (@FechaHora, @Localidad, @Tarjeta, @IdChofer, @IdUnidad, @Remito, 
         @IdConsumo, @Litros, @ImporteTotalYer, @ImporteSinImpuestos, @Factura, @IdPeriodo)";

            await ConectarAsync(conn => conn.ExecuteAsync(query, consumo));
        }

        public async Task<IEnumerable<ImportConsumoYpfEnRuta>> ObtenerPorPeriodoAsync(int idPeriodo)
        {
            var query = "SELECT * FROM ImportConsumoYpfEnRuta WHERE IdPeriodo = @IdPeriodo";
            return await ConectarAsync(conn => conn.QueryAsync<ImportConsumoYpfEnRuta>(query, new { IdPeriodo = idPeriodo }));
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
