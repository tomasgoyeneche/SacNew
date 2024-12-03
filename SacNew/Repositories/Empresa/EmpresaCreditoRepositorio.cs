using Dapper;
using SacNew.Models;
using SacNew.Services;
using static SacNew.Services.Startup;

namespace SacNew.Repositories
{
    public class EmpresaCreditoRepositorio : BaseRepositorio, IEmpresaCreditoRepositorio
    {
        public EmpresaCreditoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<EmpresaCredito?> ObtenerPorEmpresaAsync(int idEmpresa)
        {
            var query = "SELECT * FROM EmpresaCredito WHERE IdEmpresa = @IdEmpresa AND Activo = 1";

            return await ConectarAsync(connection =>
            {
                return connection.QueryFirstOrDefaultAsync<EmpresaCredito>(query, new { IdEmpresa = idEmpresa });
            });
        }

        public async Task ActualizarCreditoAsync(EmpresaCredito empresaCredito)
        {
            var query = @"
            UPDATE EmpresaCredito
            SET CreditoAsignado = @CreditoAsignado,
                CreditoConsumido = @CreditoConsumido,
                FechaModificacion = @FechaModificacion
            WHERE IdCredito = @IdCredito";

            await EjecutarConAuditoriaAsync(connection =>
                connection.ExecuteAsync(query, empresaCredito),
                "EmpresaCredito",
                "UPDATE",
                null,
                Newtonsoft.Json.JsonConvert.SerializeObject(empresaCredito));
        }

        public async Task AgregarCreditoAsync(EmpresaCredito empresaCredito)
        {
            var query = @"
            INSERT INTO EmpresaCredito (IdEmpresa, Mes, CreditoAsignado, CreditoConsumido, CreditoDisponible, Activo, FechaModificacion)
            VALUES (@IdEmpresa, @Mes, @CreditoAsignado, @CreditoConsumido, @CreditoDisponible, @Activo, @FechaModificacion)";

            await EjecutarConAuditoriaAsync(connection =>
                connection.ExecuteAsync(query, empresaCredito),
                "EmpresaCredito",
                "INSERT",
                null,
                Newtonsoft.Json.JsonConvert.SerializeObject(empresaCredito));
        }
    }
}