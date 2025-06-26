using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories.Semi
{
    public class SemiCisternaCompartimientoRepositorio : BaseRepositorio, ISemiCisternaCompartimientoRepositorio
    {
        public SemiCisternaCompartimientoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
           : base(connectionStrings, sesionService) { }

        public async Task<List<SemiCisternaCompartimiento>> ObtenerCompartimientosActivosAsync(int idSemi)
        {
            var query = "SELECT * FROM SemiCisternaCompartimiento WHERE IdSemi = @IdSemi AND Activo = 1 ORDER BY NumeroCompartimiento";
            return (await ConectarAsync(conn => conn.QueryAsync<SemiCisternaCompartimiento>(query, new { IdSemi = idSemi }))).ToList();
        }

        public async Task AgregarCompartimientoAsync(SemiCisternaCompartimiento compartimiento)
        {
            var query = @"INSERT INTO SemiCisternaCompartimiento (IdSemi, NumeroCompartimiento, CapacidadLitros, Activo)
                  VALUES (@IdSemi, @NumeroCompartimiento, @CapacidadLitros, 1)";
            await ConectarAsync(conn => conn.ExecuteAsync(query, compartimiento));
        }
        public async Task EliminarCompartimientoAsync(int idCompartimiento)
        {
            var query = "UPDATE SemiCisternaCompartimiento SET Activo = 0 WHERE IdCompartimiento = @IdCompartimiento";
            await ConectarAsync(conn => conn.ExecuteAsync(query, new { IdCompartimiento = idCompartimiento }));
        }
    }
}