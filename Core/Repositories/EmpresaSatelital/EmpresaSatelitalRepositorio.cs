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

        public async Task AgregarAsync(EmpresaSatelital empresaSatelital)
        {
            var query = @"
        INSERT INTO EmpresaSatelital (IdEmpresa, IdSatelital, Usuario, Clave, Activo)
        VALUES (@IdEmpresa, @IdSatelital, @Usuario, @Clave, @Activo)";

            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, empresaSatelital),
                "EmpresaSatelital",
                "INSERT",
                null,
                empresaSatelital
            );
        }

        public async Task EliminarAsync(int idEmpresaSatelital)
        {
            var query = "UPDATE EmpresaSatelital SET Activo = 0 WHERE IdEmpresaSatelital = @IdEmpresaSatelital";

            var empresaSatelitalAnterior = await ConectarAsync(async connection =>
            {
                return await connection.QueryFirstOrDefaultAsync<EmpresaSatelital>(
                    "SELECT * FROM EmpresaSatelital WHERE IdEmpresaSatelital = @IdEmpresaSatelital",
                    new { IdEmpresaSatelital = idEmpresaSatelital });
            });

            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, new { IdEmpresaSatelital = idEmpresaSatelital }),
                "EmpresaSatelital",
                "UPDATE",
                empresaSatelitalAnterior,
                new { Activo = 0 }
            );
        }

        public async Task<int?> ObtenerIdEmpresaSatelitalAsync(int idEmpresa, int idSatelital)
        {
            var query = @"
            SELECT IdEmpresaSatelital
            FROM EmpresaSatelital
            WHERE IdEmpresa = @IdEmpresa AND IdSatelital = @IdSatelital";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<int?>(query, new { IdEmpresa = idEmpresa, IdSatelital = idSatelital });
            });
        }
    }
}