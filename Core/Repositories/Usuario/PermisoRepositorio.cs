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

        public async Task<List<string>> ObtenerPermisosPorUsuarioAsync(int idUsuario)
        {
            var query = "SELECT p.nombrepermiso FROM permiso p JOIN usuariopermiso up ON p.idpermiso = up.idpermiso WHERE up.idusuario = @idusuario;";

            return await ConectarAsync(async connection =>
            {
                var permisos = await connection.QueryAsync<string>(query, new { IdUsuario = idUsuario });
                return permisos.ToList(); // Dapper devuelve IEnumerable, lo convertimos a List
            });
        }
    }
}