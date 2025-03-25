using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class SemiCisternaTipoCargaRepositorio : BaseRepositorio, ISemiCisternaTipoCargaRepositorio
    {
        public SemiCisternaTipoCargaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<SemiCisternaTipoCarga>> ObtenerTiposCargaAsync()
        {
            var query = "SELECT * FROM SemiCisternaTipoCarga ORDER BY Descripcion";
            return await ConectarAsync(async conn =>
            {
                var tiposCarga = await conn.QueryAsync<SemiCisternaTipoCarga>(query);
                return tiposCarga.ToList();
            });
        }
    }
}