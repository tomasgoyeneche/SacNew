using Dapper;
using SacNew.Services;

namespace SacNew.Repositories
{
    internal class PermisoRepositorio : BaseRepositorio, IPermisoRepositorio
    {
        public PermisoRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

        public List<int> ObtenerPermisosPorUsuario(int idUsuario)
        {
            var query = "SELECT idPermiso FROM UsuarioPermiso WHERE idUsuario = @IdUsuario";

            return Conectar(connection =>
            {
                return connection.Query<int>(query, new { IdUsuario = idUsuario }).ToList(); // Dapper devuelve directamente la lista de permisos
            });
        }
    }
}