using Shared.Models;

namespace Core.Repositories
{
    public interface IPermisoRepositorio
    {
        Task<List<Permiso>> ObtenerPermisosPorUsuarioAsync(int idUsuario);

        Task<List<Permiso>> ObtenerTodoslosPermisos();

        Task EliminarPermisoAsync(UsuarioPermiso permisoUsuario);

        Task AgregarPermisoAsync(UsuarioPermiso permiso);

        Task<UsuarioPermiso?> ObtenerRelacionAsync(int idUsuario, int idPermiso);

        Task<List<string>> ObtenerPermisosParaSesionAsync(int idUsuario);
    }
}