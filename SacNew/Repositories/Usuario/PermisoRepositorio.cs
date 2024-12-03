using Dapper;
using SacNew.Services;
using static SacNew.Services.Startup;

namespace SacNew.Repositories
{
    internal class PermisoRepositorio : BaseRepositorio, IPermisoRepositorio
    {
        public PermisoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<int>> ObtenerPermisosPorUsuarioAsync(int idUsuario)
        {
            var query = "SELECT idPermiso FROM UsuarioPermiso WHERE idUsuario = @IdUsuario";

            return await ConectarAsync(async connection =>
            {
                var permisos = await connection.QueryAsync<int>(query, new { IdUsuario = idUsuario });
                return permisos.ToList(); // Dapper devuelve IEnumerable, lo convertimos a List
            });
        }
    }
}