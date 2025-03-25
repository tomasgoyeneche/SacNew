using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class SemiCisternaMaterialRepositorio : BaseRepositorio, ISemiCisternaMaterialRepositorio
    {
        public SemiCisternaMaterialRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<SemiCisternaMaterial>> ObtenerMaterialesAsync()
        {
            var query = "SELECT * FROM SemiCisternaMaterial ORDER BY Descripcion";
            return await ConectarAsync(async conn =>
            {
                var materiales = await conn.QueryAsync<SemiCisternaMaterial>(query);
                return materiales.ToList();
            });
        }
    }
}