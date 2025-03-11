using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class TractorRepositorio : BaseRepositorio, ITractorRepositorio
    {
        public TractorRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }


        public async Task<TractorDto> ObtenerPorIdDtoAsync(int idTractor)
        {
            var query = "SELECT * FROM vw_TractoresDetalles WHERE idTractor = @IdTractor";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<TractorDto>(query, new { IdTractor = idTractor }));
        }



        public async Task<List<TractorDto>> ObtenerTodosLosTractoresDto()
        {
            var query = "SELECT * FROM vw_TractoresDetalles";

            return await ConectarAsync(async connection =>
            {
                var tractores = await connection.QueryAsync<TractorDto>(query);
                return tractores.ToList();
            });
        }
    }
}
