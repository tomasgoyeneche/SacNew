using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
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