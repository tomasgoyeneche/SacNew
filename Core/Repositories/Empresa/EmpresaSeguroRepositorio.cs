using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class EmpresaSeguroRepositorio : BaseRepositorio, IEmpresaSeguroRepositorio
    {
        public EmpresaSeguroRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<EmpresaSeguroDto>> ObtenerSegurosPorEmpresaAsync(int idEmpresa)
        {
            var query = @"SELECT * FROM vw_EmpresaSegurosActivos WHERE idEmpresa = @idEmpresa";

            return await ConectarAsync(async connection =>
                (await connection.QueryAsync<EmpresaSeguroDto>(query, new { idEmpresa })).ToList()
            );
        }

        public async Task<List<EmpresaSeguroDto?>> ObtenerSeguroPorEmpresaYEntidadAsync(int idEmpresa, int idEmpresaSeguroEntidad)
        {
            var query = @"SELECT * FROM vw_EmpresaSegurosActivos
                  WHERE idEmpresa = @idEmpresa AND idEmpresaSeguroEntidad = @idEmpresaSeguroEntidad";

            return await ConectarAsync(async connection =>
                (await connection.QueryAsync<EmpresaSeguroDto?>(query, new { idEmpresa, idEmpresaSeguroEntidad })).ToList()
            );
        }

        public Task<EmpresaSeguro?> ObtenerSeguroPorIdAsync(int idEmpresaSeguro)
        {
            return ObtenerPorIdGenericoAsync<EmpresaSeguro>("EmpresaSeguro", "IdEmpresaSeguro", idEmpresaSeguro);
        }

        public Task AgregarSeguroAsync(EmpresaSeguro seguro)
        {
            return AgregarGenéricoAsync("EmpresaSeguro", seguro);
        }

        public Task ActualizarSeguroAsync(EmpresaSeguro seguro)
        {
            return ActualizarGenéricoAsync("EmpresaSeguro", seguro);
        }

        public Task EliminarSeguroAsync(int idEmpresaSeguro)
        {
            return EliminarGenéricoAsync<EmpresaSeguro>("EmpresaSeguro", idEmpresaSeguro);
        }
    }
}