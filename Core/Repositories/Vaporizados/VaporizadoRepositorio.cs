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
    public class VaporizadoRepositorio : BaseRepositorio, IVaporizadoRepositorio
    {
        public VaporizadoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public Task AgregarAsync(Vaporizado vaporizado, int idUsuario)
        {
            var query = @"
            INSERT INTO Vaporizado 
            (NroCertificado, RemitoDanes, IdPosta, CantidadCisternas, IdVaporizadoMotivo, FechaInicio, FechaFin, IdVaporizadoZona, TipoIngreso, EsExterno, IdNomina, IdTe, Observaciones, Activo)
            VALUES 
            (@NroCertificado, @RemitoDanes, @IdPosta, @CantidadCisternas, @IdVaporizadoMotivo, @FechaInicio, @FechaFin, @IdVaporizadoZona, @TipoIngreso, @EsExterno, @IdNomina, @IdTe, @Observaciones, @Activo)";

            return EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, vaporizado),
                "Vaporizado",
                "INSERT",
                null,
                vaporizado
            );
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
    }
}
