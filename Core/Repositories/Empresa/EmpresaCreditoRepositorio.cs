using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class EmpresaCreditoRepositorio : BaseRepositorio, IEmpresaCreditoRepositorio
    {
        public EmpresaCreditoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<EmpresaCredito?> ObtenerPorEmpresaAsync(int idEmpresa)
        {
            var query = "SELECT * FROM EmpresaCredito WHERE IdEmpresa = @IdEmpresa AND Estado = 'Activo'";

            return await ConectarAsync(connection =>
            {
                return connection.QueryFirstOrDefaultAsync<EmpresaCredito>(query, new { IdEmpresa = idEmpresa });
            });
        }

        public async Task<EmpresaCredito?> ObtenerPorEmpresaYPeriodoAsync(int idEmpresa, int? idPeriodo)
        {
            var query = "SELECT * FROM EmpresaCredito WHERE IdEmpresa = @IdEmpresa AND  idPeriodo = @IdPeriodo";

            return await ConectarAsync(connection =>
            {
                return connection.QueryFirstOrDefaultAsync<EmpresaCredito>(query, new { IdEmpresa = idEmpresa, IdPeriodo = idPeriodo });
            });
        }

        public async Task ActualizarCreditoAsync(EmpresaCredito empresaCredito)
        {
            var query = @"
            UPDATE EmpresaCredito
            SET CreditoAsignado = @CreditoAsignado,
                CreditoConsumido = @CreditoConsumido
            WHERE IdCredito = @IdCredito";

            await EjecutarConAuditoriaAsync(connection =>
                connection.ExecuteAsync(query, empresaCredito),
                "EmpresaCredito",
                "UPDATE",
                null,
                Newtonsoft.Json.JsonConvert.SerializeObject(empresaCredito));
        }

        public async Task<decimal?> ObtenerCreditoPorEmpresaYPeriodoAsync(int idEmpresa, int idPeriodo)
        {
            var query = @"
            SELECT CreditoAsignado
            FROM EmpresaCredito
            WHERE IdEmpresa = @IdEmpresa AND IdPeriodo = @IdPeriodo AND Estado = 'Activo'";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<decimal?>(query, new { IdEmpresa = idEmpresa, IdPeriodo = idPeriodo });
            });
        }

        public async Task InsertarCreditoAsync(int idEmpresa, int idPeriodo, decimal credito)
        {
            var query = @"
            INSERT INTO EmpresaCredito (IdEmpresa, IdPeriodo, CreditoAsignado)
            VALUES (@IdEmpresa, @IdPeriodo, @CreditoAsignado)";

            await ConectarAsync(async conn =>
            {
                await conn.ExecuteAsync(query, new { IdEmpresa = idEmpresa, IdPeriodo = idPeriodo, CreditoAsignado = credito });
            });
        }

        public async Task ActualizarCreditoPeriodoAsync(int idEmpresa, int idPeriodo, decimal credito)
        {
            var query = @"
            UPDATE EmpresaCredito
            SET CreditoAsignado = @CreditoAsignado
            WHERE IdEmpresa = @IdEmpresa AND IdPeriodo = @IdPeriodo AND Estado = 'Activo'";

            await ConectarAsync(async conn =>
            {
                await conn.ExecuteAsync(query, new { IdEmpresa = idEmpresa, IdPeriodo = idPeriodo, CreditoAsignado = credito });
            });
        }
    }
}