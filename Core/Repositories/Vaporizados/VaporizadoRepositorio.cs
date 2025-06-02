using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class VaporizadoRepositorio : BaseRepositorio, IVaporizadoRepositorio
    {
        public VaporizadoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public Task AgregarAsync(Vaporizado vaporizado, int idUsuario)
        {
            var query = @"
            INSERT INTO Vaporizado
            (NroCertificado, RemitoDanes, IdPosta, CantidadCisternas, IdVaporizadoMotivo, FechaInicio, FechaFin, IdVaporizadoZona, TipoIngreso, EsExterno, IdNomina, IdTe, Observaciones, Activo, idGuardiaIngreso)
            VALUES
            (@NroCertificado, @RemitoDanes, @IdPosta, @CantidadCisternas, @IdVaporizadoMotivo, @FechaInicio, @FechaFin, @IdVaporizadoZona, @TipoIngreso, @EsExterno, @IdNomina, @IdTe, @Observaciones, @Activo, @IdGuardiaIngreso)";

            return EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, vaporizado),
                "Vaporizado",
                "INSERT",
                null,
                vaporizado
            );
        }

        public async Task<List<VaporizadoDto>> ObtenerTodosLosVaporizadosDto()
        {
            var query = @"
        SELECT *
        FROM vw_VaporizadoDetalle"; // Comparación sin hora

            return await ConectarAsync(async connection =>
            {
                var vaporizados = await connection.QueryAsync<VaporizadoDto>(query);
                return vaporizados.ToList();
            });
        }

        public async Task<Vaporizado?> ObtenerPorIdAsync(int idVaporizado)
        {
            return await ObtenerPorIdGenericoAsync<Vaporizado>("Vaporizado", "IdVaporizado", idVaporizado);
        }

        public async Task<List<VaporizadoDto>> ObtenerVaporizadosDtoPorPosta(int idPosta)
        {
            var query = @"
            SELECT *
        FROM vw_VaporizadoDetalle where idPosta = @idPosta"; // Elige el de mayor IdVaporizado (el más nuevo)

            return await ConectarAsync(async connection =>
            {
                var vaporizados = await connection.QueryAsync<VaporizadoDto>(query, new { IdPosta = idPosta });
                return vaporizados.ToList();
            });
        }

        public async Task EditarAsync(Vaporizado vaporizado, int idUsuario)
        {
            var query = @"
                UPDATE Vaporizado SET
                    NroCertificado = @NroCertificado,
                    RemitoDanes = @RemitoDanes,
                    CantidadCisternas = @CantidadCisternas,
                    IdVaporizadoMotivo = @IdVaporizadoMotivo,
                    FechaInicio = @FechaInicio,
                    FechaFin = @FechaFin,
                    IdVaporizadoZona = @IdVaporizadoZona,
                    Observaciones = @Observaciones
                WHERE IdVaporizado = @IdVaporizado
            ";

            await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, vaporizado),
                "Vaporizado",
                "UPDATE",
                await ObtenerPorIdGenericoAsync<Vaporizado>("Vaporizado", "IdVaporizado", vaporizado.IdVaporizado),
                vaporizado
            );
        }

        public async Task<Vaporizado?> ObtenerPorNominaAsync(int idNomina)
        {
            var query = @"
            SELECT TOP 1 * FROM Vaporizado
            WHERE IdNomina = @idNomina
            ORDER BY IdVaporizado DESC"; // Elige el de mayor IdVaporizado (el más nuevo)

            return await ConectarAsync(conn =>
                conn.QueryFirstOrDefaultAsync<Vaporizado>(query, new { idNomina }));
        }

        public async Task<Vaporizado?> ObtenerPorTeAsync(int idTe)
        {
            var query = @"
            SELECT TOP 1 * FROM Vaporizado
            WHERE IdTe = @idTe
            ORDER BY IdVaporizado DESC"; // Elige el de mayor IdVaporizado (el más nuevo)

            return await ConectarAsync(conn =>
                conn.QueryFirstOrDefaultAsync<Vaporizado>(query, new { idTe }));
        }

        public Task EliminarAsync(int idVaporizado)
        {
            return EliminarGenéricoAsync<Vaporizado>("Vaporizado", idVaporizado);
        }
    }
}