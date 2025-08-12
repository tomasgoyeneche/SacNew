using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using System.Data;

namespace Core.Repositories
{
    public class UnidadRepositorio : BaseRepositorio, IUnidadRepositorio
    {
        public UnidadRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<UnidadDto>> ObtenerUnidadesDtoAsync()
        {
            var query = "SELECT * FROM vw_UnidadesDetalles";

            return await ConectarAsync(async connection =>
            {
                var chofers = await connection.QueryAsync<UnidadDto>(query);
                return chofers.ToList();
            });
        }

        public async Task<List<Unidad>> ObtenerUnidadesAsync()
        {
            var query = "SELECT * FROM Unidad";

            return await ConectarAsync(async connection =>
            {
                var chofers = await connection.QueryAsync<Unidad>(query);
                return chofers.ToList();
            });
        }

        public async Task<UnidadDto?> ObtenerPorIdDtoAsync(int idUnidad)
        {
            var query = "SELECT * FROM vw_UnidadesDetalles WHERE IdUnidad = @idUnidad";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<UnidadDto>(query, new { IdUnidad = idUnidad }));
        }

        public Task<Unidad?> ObtenerPorUnidadIdAsync(int idUnidad)
        {
            return ObtenerPorIdGenericoAsync<Unidad>("Unidad", "IdUnidad", idUnidad);
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
            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(
                    "sp_DarDeBajaUnidad",
                    new { IdUnidad = idUnidad },
                    commandType: CommandType.StoredProcedure
                );
            });
        }

        public Task ActualizarVencimientoUnidadAsync(int idUnidad, int idTipoVencimiento, DateTime fechaActualizacion, int idUsuario)
        {
            var query = @"
        UPDATE UnidadVencimiento
        SET FechaVencimiento = @FechaVencimiento,
            IdUsuario = @IdUsuario
        WHERE IdUnidad = @IdUnidad AND idTipoVencimiento = @IdTipoVencimiento";

            var valoresNuevos = new
            {
                IdUnidad = idUnidad,
                IdTipoVencimiento = idTipoVencimiento,
                FechaVencimiento = fechaActualizacion,
                IdUsuario = idUsuario
            };

            return EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, valoresNuevos),
                "UnidadVencimiento",
                "UPDATE",
                new { IdUnidad = idUnidad, IdTipoVencimiento = idTipoVencimiento },
                valoresNuevos
            );
        }

        public async Task AgregarUnidadAsync(Unidad unidad, int idUsuario)
        {
            await ConectarAsync(async connection =>
            {
                await connection.ExecuteAsync(
                    "sp_AltaUnidad",
                    new
                    {
                        unidad.IdTractor,
                        unidad.IdSemi,
                        unidad.TaraTotal,
                        unidad.IdEmpresa,
                        unidad.Metanol,
                        unidad.Gasoil,
                        unidad.LujanCuyo,
                        unidad.AptoBo,
                        unidad.Activo,
                        idUsuario = idUsuario
                    },
                    commandType: CommandType.StoredProcedure
                );
            });
        }
    }
}