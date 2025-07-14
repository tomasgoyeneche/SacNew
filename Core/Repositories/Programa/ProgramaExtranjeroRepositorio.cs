using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    internal class ProgramaExtranjeroRepositorio : BaseRepositorio, IProgramaExtranjeroRepositorio
    {
        public ProgramaExtranjeroRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<ProgramaExtranjero>> ObtenerHitosextranjerosPorProgramaAsync(int idPrograma)
        {
            var query = @"SELECT * FROM ProgramaExtranjero WHERE IdPrograma = @idPrograma AND Activo = 1";
            return await ConectarAsync(async connection =>
            {
                var result = await connection.QueryAsync<ProgramaExtranjero>(query, new { idPrograma = idPrograma});
                return result.ToList();
            });
        }


        public async Task InsertarHitoExtranjeroAsync(ProgramaExtranjero hito)
        {
            var sql = @"INSERT INTO ProgramaExtranjero (IdPrograma, IdProgramaTipoPunto, IdProgramaTipoEvento, Odometro, Fecha, Activo)
                VALUES (@IdPrograma, @IdProgramaTipoPunto, @IdProgramaTipoEvento, @Odometro, @Fecha, @Activo)";
            await ConectarAsync(conn => conn.ExecuteAsync(sql, hito));
        }

        public async Task ActualizarHitoExtranjeroAsync(int idProgramaExtranjero, DateTime nuevaFecha, decimal odometro)
        {
            var sql = "UPDATE ProgramaExtranjero SET Fecha = @nuevaFecha, Odometro = @odometro WHERE IdProgramaExtranjero = @idProgramaExtranjero";
            await ConectarAsync(conn => conn.ExecuteAsync(sql, new { idProgramaExtranjero, nuevaFecha, odometro }));
        }

        public async Task BajaHitoExtranjeroAsync(int idProgramaExtranjero)
        {
            var sql = "UPDATE ProgramaExtranjero SET Activo = 0 WHERE IdProgramaExtranjero = @idProgramaExtranjero";
            await ConectarAsync(conn => conn.ExecuteAsync(sql, new { idProgramaExtranjero }));
        }

    }
}
