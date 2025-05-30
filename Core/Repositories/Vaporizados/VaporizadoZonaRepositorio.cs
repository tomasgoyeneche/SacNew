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
    public class VaporizadoZonaRepositorio : BaseRepositorio, IVaporizadoZonaRepositorio
    {
        public VaporizadoZonaRepositorio(ConnectionStrings cs, ISesionService sesionService)
            : base(cs, sesionService) { }

        public async Task<List<VaporizadoZona>> ObtenerTodasAsync()
        {
            var query = "SELECT * FROM VaporizadoZona WHERE Activo = 1";
            return await ConectarAsync(async conn =>
                (await conn.QueryAsync<VaporizadoZona>(query)).ToList()
            );
        }
    }
}
