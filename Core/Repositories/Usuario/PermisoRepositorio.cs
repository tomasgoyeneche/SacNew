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

        public async Task<List<Permiso>> ObtenerPermisosPorUsuarioAsync(int idUsuario)
        {
            var query = "SELECT p.nombrepermiso, p.idpermiso FROM permiso p JOIN usuariopermiso up ON p.idpermiso = up.idpermiso WHERE up.idusuario = @idusuario and up.activo = 1;";

            return await ConectarAsync(async connection =>
            {
                var permisos = await connection.QueryAsync<Permiso>(query, new { IdUsuario = idUsuario });
                return permisos.ToList(); // Dapper devuelve IEnumerable, lo convertimos a List
            });
        }

        public async Task<List<string>> ObtenerPermisosParaSesionAsync(int idUsuario)
        {
            var query = "SELECT p.nombrepermiso FROM permiso p JOIN usuariopermiso up ON p.idpermiso = up.idpermiso WHERE up.idusuario = @idusuario and up.activo = 1;";

            return await ConectarAsync(async connection =>
            {
                var permisos = await connection.QueryAsync<string>(query, new { IdUsuario = idUsuario });
                return permisos.ToList(); // Dapper devuelve IEnumerable, lo convertimos a List
            });
        }

        public Task<List<Permiso>> ObtenerTodoslosPermisos()
        {
            var query = "SELECT * FROM Permiso";

            return ConectarAsync(connection =>
            {
                return connection.QueryAsync<Permiso>(query)
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task AgregarPermisoAsync(UsuarioPermiso permiso)
        {
            var query = @"
            INSERT INTO UsuarioPermiso (IdUsuario, IdPermiso)
            VALUES (@IdUsuario, @IdPermiso)";

            await EjecutarConAuditoriaAsync(
                async conn => await conn.ExecuteAsync(query, permiso),
                "UsuarioPermiso",
                "INSERT",
                null,
                permiso
            );
        }

        public async Task EliminarPermisoAsync(UsuarioPermiso permisoUsuario)
        {
            var query = "UPDATE UsuarioPermiso SET Activo = 0 WHERE IdUsuario = @IdUsuario and IdPermiso = @IdPermiso";

            // Registrar el cambio de estado como "Eliminado" en la auditoría
            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query,permisoUsuario),
                "UsuarioPermiso",
                "DELETE",
                null,  // Pasamos los valores anteriores sin serializarlos
                null  // No hay valores nuevos ya que solo se desactiva el registro
            );
        }

        public async Task<UsuarioPermiso?> ObtenerRelacionAsync(int idUsuario, int idPermiso)
        {
            var query = @"
            SELECT *
            FROM UsuarioPermiso
            WHERE idUsuario = @idUsuario AND idPermiso = @idPermiso AND Activo = 1";

            return await ConectarAsync(conn =>
                conn.QuerySingleOrDefaultAsync<UsuarioPermiso>(query, new { IdUsuario = idUsuario, IdPermiso = idPermiso }));
        }

    }
}