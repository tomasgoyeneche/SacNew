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
    internal class ProgramaRepositorio : BaseRepositorio, IProgramaRepositorio
    {
        public ProgramaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<Ruteo>> ObtenerRuteoAsync()
        {
            var query = "SELECT * FROM vw_Ruteo";

            return await ConectarAsync(async connection =>
            {
                var ruteo = await connection.QueryAsync<Ruteo>(query);
                return ruteo.ToList();
            });
        }
    }
}
