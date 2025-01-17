using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class EmpresaSatelitalRepositorio : BaseRepositorio, IEmpresaSatelitalRepositorio
    {
        public EmpresaSatelitalRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<List<EmpresaSatelitalDto>> ObtenerSatelitalesPorEmpresaAsync(int idEmpresa)
        {
            return await ConectarAsync(async conn =>
            {
                var query = @"
                SELECT es.idEmpresaSatelital, s.Descripcion, es.Usuario, es.Clave
                FROM EmpresaSatelital es
                INNER JOIN Satelital s ON es.IdSatelital = s.IdSatelital
                WHERE es.IdEmpresa = @IdEmpresa AND es.Activo = 1";

                return (await conn.QueryAsync<EmpresaSatelitalDto>(query, new { IdEmpresa = idEmpresa })).ToList();
            });
        }
    }
}