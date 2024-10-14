using Dapper;
using SacNew.Models;
using SacNew.Services;

namespace SacNew.Repositories
{
    internal class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {
        public UsuarioRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

        public Usuario? ObtenerPorNombreUsuario(string nombreUsuario)
        {
            var query = "SELECT idUsuario, nombreUsuario, contrasena, nombreCompleto, activo FROM Usuario WHERE nombreUsuario = @NombreUsuario";

            return Conectar(connection =>
            {
                return connection.QueryFirstOrDefault<Usuario>(query, new { NombreUsuario = nombreUsuario });
            });
        }
    }
}