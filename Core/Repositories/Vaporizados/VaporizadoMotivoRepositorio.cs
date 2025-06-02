using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class VaporizadoMotivoRepositorio : BaseRepositorio, IVaporizadoMotivoRepositorio
    {
        public VaporizadoMotivoRepositorio(ConnectionStrings cs, ISesionService sesionService)
            : base(cs, sesionService) { }

        public async Task<List<VaporizadoMotivo>> ObtenerTodosAsync()
        {
            var query = "SELECT * FROM VaporizadoMotivo WHERE Activo = 1";
            return await ConectarAsync(async conn =>
                (await conn.QueryAsync<VaporizadoMotivo>(query)).ToList()
            );
        }
    }
}