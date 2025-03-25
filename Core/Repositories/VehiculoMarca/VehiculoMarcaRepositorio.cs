using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class VehiculoMarcaRepositorio : BaseRepositorio, IVehiculoMarcaRepositorio
    {
        public VehiculoMarcaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<VehiculoMarca>> ObtenerMarcasPorTipoAsync(int tipoVehiculo)
        {
            var query = "SELECT * FROM VehiculoMarca WHERE IdTipoVehiculo = @TipoVehiculo ORDER BY NombreMarca";
            return await ConectarAsync(async conn =>
            {
                var marcas = await conn.QueryAsync<VehiculoMarca>(query, new { TipoVehiculo = tipoVehiculo });
                return marcas.ToList();
            });
        }
    }
}