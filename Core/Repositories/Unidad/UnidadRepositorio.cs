using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using Shared.Models.DTOs;

namespace Core.Repositories
{
    public class UnidadRepositorio : BaseRepositorio, IUnidadRepositorio
    {
        public UnidadRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        // Obtener Unidad Por Id
        public async Task<List<UnidadPatenteDto>> ObtenerUnidadesPatenteDtoAsync()
        {
            var query = @"
            SELECT * FROM UnidadPatentesEmpresaVista";

            return await ConectarAsync(async connection =>
            {
                var unidades = await connection.QueryAsync<UnidadPatenteDto>(query);
                return unidades.ToList();
            });
        }

        public async Task<List<UnidadDto>> ObtenerUnidadesDtoAsync()
        {
            var query = "SELECT * FROM vw_UnidadesDetalles";

            return await ConectarAsync(async connection =>
            {
                var chofers = await connection.QueryAsync<UnidadDto>(query);
                return chofers.ToList();
            });
        }

        public async Task<UnidadDto> ObtenerPorIdDtoAsync(int idUnidad)
        {
            var query = "SELECT * FROM vw_UnidadesDetalles WHERE IdUnidad = @idUnidad";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<UnidadDto>(query, new { IdUnidad = idUnidad }));
        }

        public async Task<UnidadPatenteDto?> ObtenerPorIdAsync(int idUnidad)
        {
            var query = @"
            SELECT * FROM UnidadPatentesEmpresaVista
            WHERE IdUnidad = @IdUnidad";

            return await ConectarAsync(async connection =>
            {
                var unidad = await connection.QueryFirstOrDefaultAsync<UnidadPatenteDto>(query, new { IdUnidad = idUnidad });
                return unidad;
            });
        }

        // Obtener Por Otras Opciones

        public async Task<int?> ObtenerIdTractorPorPatenteAsync(string patente)
        {
            var query = @"
            SELECT IdTractor
            FROM Tractor
            WHERE Patente = @Patente AND Activo = 1";

            return await ConectarAsync(async conn =>
                await conn.QuerySingleOrDefaultAsync<int?>(query, new { Patente = patente }));
        }

        public async Task<int?> ObtenerIdUnidadPorTractorAsync(int idTractor)
        {
            var query = @"
            SELECT IdUnidad
            FROM Unidad
            WHERE IdTractor = @IdTractor AND Activo = 1";

            return await ConectarAsync(async conn =>
                await conn.QuerySingleOrDefaultAsync<int?>(query, new { IdTractor = idTractor }));
        }

        public async Task<List<NominaMetanolActivaDto>> ObtenerNominaMetanolActiva()
        {
            var query = "SELECT * FROM vw_NominaMetanol where activo = 1";

            return await ConectarAsync(async connection =>
            {
                var chofers = await connection.QueryAsync<NominaMetanolActivaDto>(query);
                return chofers.ToList();
            });
        }

        // Actualizar, Editar, Eliminar
        public async Task EliminarUnidadAsync(int idUnidad)
        {
            var query = "Update Unidad set Activo = 0 WHERE idUnidad = @idUnidad";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { IdUnidad = idUnidad });
            });
        }

        public async Task ActualizarVencimientoUnidadAsync(int idUnidad, int idTipoVencimiento, DateTime fechaActualizacion, int idUsuario)
        {
            var query = @"
        UPDATE UnidadVencimiento
        SET FechaVencimiento = @fechaActualizacion,
            IdUsuario = @idUsuario
        WHERE IdUnidad = @idUnidad AND idTipoVencimiento = @idTipoVencimiento";

            await ConectarAsync(async connection =>
            {
                await connection.ExecuteAsync(query, new
                {
                    idUnidad,
                    idTipoVencimiento,
                    fechaActualizacion,
                    idUsuario
                });
            });
        }
    }
}