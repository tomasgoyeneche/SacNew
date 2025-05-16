using Core.Base;
using Core.Services;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class NominaRepositorio : BaseRepositorio, INominaRepositorio
    {
        public NominaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<Nomina?> ObtenerNominaActivaPorUnidadAsync(int idUnidad, DateTime fechaReferencia)
        {
            var query = @"
            SELECT TOP 1 * 
            FROM Nomina 
            WHERE IdUnidad = @idUnidad 
              AND FechaAlta <= @fecha
              AND (FechaBaja IS NULL OR FechaBaja >= @fecha)
              AND Activo = 1";

            return await ConectarAsync(conn =>
                conn.QueryFirstOrDefaultAsync<Nomina>(query, new
                {
                    idUnidad,
                    fecha = fechaReferencia.Date
                })
            );
        }
    }

}
