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
                (await connection.QueryAsync<EmpresaSeguroDto>(query, new { idEmpresa, idEmpresaSeguroEntidad })).ToList()
            );
        }

        public async Task<EmpresaSeguro?> ObtenerSeguroPorIdAsync(int idEmpresaSeguro)
        {
            var query = @"SELECT * FROM EmpresaSeguro
                  WHERE idEmpresaSeguro = @idEmpresaSeguro AND Activo = 1";

            return await ConectarAsync(connection =>
                connection.QuerySingleOrDefaultAsync<EmpresaSeguro>(query, new { idEmpresaSeguro })
            );
        }

        public async Task AgregarSeguroAsync(EmpresaSeguro seguro)
        {
            var query = @"
        INSERT INTO EmpresaSeguro (
            idEmpresa, idEmpresaSeguroEntidad, idCia, idCobertura,
            numeroPoliza, certificadoMensual, vigenciaAnual, activo
        )
        VALUES (
            @idEmpresa, @idEmpresaSeguroEntidad, @idCia, @idCobertura,
            @numeroPoliza, @certificadoMensual, @vigenciaAnual, 1
        );";

            await ConectarAsync(conn => conn.ExecuteAsync(query, seguro));
        }

        public async Task ActualizarSeguroAsync(EmpresaSeguro seguro)
        {
            var query = @"
        UPDATE EmpresaSeguro SET
            idCia = @idCia,
            idCobertura = @idCobertura,
            numeroPoliza = @numeroPoliza,
            certificadoMensual = @certificadoMensual,
            vigenciaAnual = @vigenciaAnual
        WHERE idEmpresaSeguro = @idEmpresaSeguro";

            await ConectarAsync(conn => conn.ExecuteAsync(query, seguro));
        }

        public async Task EliminarSeguroAsync(int idEmpresaSeguro)
        {
            var query = @"UPDATE EmpresaSeguro SET activo = 0 WHERE idEmpresaSeguro = @idEmpresaSeguro";
            await ConectarAsync(conn => conn.ExecuteAsync(query, new { idEmpresaSeguro }));
        }
    }
}