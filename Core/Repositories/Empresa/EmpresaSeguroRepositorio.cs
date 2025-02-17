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

        public async Task<EmpresaSeguro?> ObtenerPorEmpresaAsync(int idEmpresa)
        {
            var query = "SELECT * FROM EmpresaSeguro WHERE IdEmpresa = @IdEmpresa";
            return await ConectarAsync(async connection =>
            {
                return await connection.QueryFirstOrDefaultAsync<EmpresaSeguro>(query, new { IdEmpresa = idEmpresa });
            });
        }

        public async Task ActualizarAsync(EmpresaSeguro seguro)
        {
            var seguroAnterior = await ObtenerPorEmpresaAsync(seguro.IdEmpresa);

            var query = @"
        UPDATE EmpresaSeguro
        SET IdCia = @IdCia,
            IdCobertura = @IdCobertura,
            NumeroPoliza = @NumeroPoliza,
            VigenciaHasta = @VigenciaHasta,
            PagoDesde = @PagoDesde,
            PagoHasta = @PagoHasta
        WHERE IdSeguroEmpresa = @IdSeguroEmpresa";

            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, seguro),
                "EmpresaSeguro",
                "UPDATE",
                seguroAnterior,
                seguro
            );
        }
    }
}