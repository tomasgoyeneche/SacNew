using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class VehiculoModeloRepositorio : BaseRepositorio, IVehiculoModeloRepositorio
    {
        public VehiculoModeloRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<VehiculoModelo>> ObtenerModelosPorMarcaAsync(int idMarca)
        {
            var query = "SELECT * FROM VehiculoModelo WHERE IdMarca = @IdMarca ORDER BY NombreModelo";
            return await ConectarAsync(async conn =>
            {
                var modelos = await conn.QueryAsync<VehiculoModelo>(query, new { IdMarca = idMarca });
                return modelos.ToList();
            });
        }
    }
}