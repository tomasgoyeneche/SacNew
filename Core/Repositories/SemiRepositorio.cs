using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class SemiRepositorio : BaseRepositorio, ISemiRepositorio
    {
        public SemiRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
           : base(connectionStrings, sesionService) { }


        public async Task<SemiDto> ObtenerPorIdDtoAsync(int idSemi)
        {
            var query = "SELECT * FROM vw_TractoresDetalles WHERE idSemi = @idSemi";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<SemiDto>(query, new { IdSemi = idSemi }));
        }



        public async Task<List<SemiDto>> ObtenerTodosLosSemisDto()
        {
            var query = "SELECT * FROM vw_SemiremolquesDetalles";

            return await ConectarAsync(async connection =>
            {
                var tractores = await connection.QueryAsync<SemiDto>(query);
                return tractores.ToList();
            });
        }
    }
}
