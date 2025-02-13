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
    internal class SatelitalRepositorio : BaseRepositorio, ISatelitalRepositorio
    {
        public SatelitalRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<Satelital>> ObtenerTodosAsync()
        {
            var query = "SELECT IdSatelital, Descripcion FROM Satelital";
            return await ConectarAsync(async connection =>
            {
                return (await connection.QueryAsync<Satelital>(query)).ToList();
            });
        }
    }
}
