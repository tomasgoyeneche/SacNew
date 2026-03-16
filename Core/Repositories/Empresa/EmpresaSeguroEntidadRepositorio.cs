using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class EmpresaSeguroEntidadRepositorio : BaseRepositorio, IEmpresaSeguroEntidadRepositorio
    {
        public EmpresaSeguroEntidadRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<EmpresaSeguroEntidad>> ObtenerTodasAsync()
        {
            const string query = @"SELECT idEmpresaSeguroEntidad, Descripcion
                           FROM EmpresaSeguroEntidad
                           WHERE Activo = 1
                           ORDER BY Descripcion";

            return await ConectarAsync(async connection =>
            {
                var result = await connection.QueryAsync<EmpresaSeguroEntidad>(query);
                return result.ToList();
            });
        }
    }
}