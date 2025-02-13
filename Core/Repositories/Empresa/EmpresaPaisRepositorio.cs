using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class EmpresaPaisRepositorio : BaseRepositorio, IEmpresaPaisRepositorio
    {
        public EmpresaPaisRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<List<EmpresaPaisDto>> ObtenerPaisesPorEmpresaAsync(int idEmpresa)
        {
            return await ConectarAsync(async conn =>
            {
                var query = @"
                SELECT ep.idEmpresaPais, p.NombrePais
                FROM EmpresaPais ep
                INNER JOIN Pais p ON ep.IdPais = p.IdPais
                WHERE ep.IdEmpresa = @IdEmpresa AND ep.Activo = 1";

                return (await conn.QueryAsync<EmpresaPaisDto>(query, new { IdEmpresa = idEmpresa })).ToList();
            });
        }
    }
}