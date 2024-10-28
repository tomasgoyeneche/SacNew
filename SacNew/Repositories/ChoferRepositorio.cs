using Dapper;
using SacNew.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public class ChoferRepositorio : BaseRepositorio, IChoferRepositorio
    {
        public ChoferRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

        public async Task<int?> ObtenerIdPorDocumentoAsync(string documento)
        {
            var query = "SELECT IdChofer FROM Chofer WHERE Documento = @Documento AND Activo = 1";

            return await ConectarAsync(async conn =>
                await conn.QuerySingleOrDefaultAsync<int?>(query, new { Documento = documento }));
        }
    }
}
