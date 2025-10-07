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
    public class TipoMantenimientoRepositorio : BaseRepositorio, ITipoMantenimientoRepositorio
    {
        public TipoMantenimientoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<TipoMantenimiento>> ObtenerTodosAsync()
        {
            const string query = "SELECT * FROM TipoMantenimiento ORDER BY Nombre";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<TipoMantenimiento>(query);
                return result.ToList();
            });
        }
    }
}
