using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class EmpresaTipoRepositorio : BaseRepositorio, IEmpresaTipoRepositorio
    {
        public EmpresaTipoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<EmpresaTipo>> ObtenerTodosAsync()
        {
            var query = "SELECT idEmpresaTipo, Descripcion FROM EmpresaTipo";
            return await ConectarAsync(async connection =>
            {
                return (await connection.QueryAsync<EmpresaTipo>(query)).ToList();
            });
        }
    }
}