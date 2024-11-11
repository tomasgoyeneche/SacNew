using Dapper;
using SacNew.Models.DTOs;
using SacNew.Services;

namespace SacNew.Repositories
{
    public class UnidadRepositorio : BaseRepositorio, IUnidadRepositorio
    {
        public UnidadRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

        public List<UnidadPatenteDto> ObtenerUnidadesPatenteDto()
        {
            var query = @"
            SELECT * FROM UnidadPatentesEmpresaVista";

            return Conectar(connection =>
            {
                var unidades = connection.Query<UnidadPatenteDto>(query).ToList();
                return unidades;
            });
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
    }
}