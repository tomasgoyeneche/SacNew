using Dapper;
using SacNew.Models;
using SacNew.Services;

namespace SacNew.Repositories
{
    internal class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {
        public UsuarioRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

        public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
        {
            var query = @"
        SELECT idUsuario, nombreUsuario, contrasena, nombreCompleto, activo, idPosta
        FROM Usuario
        WHERE nombreUsuario = @NombreUsuario";

            return await ConectarAsync(async connection =>
            {
                return await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { NombreUsuario = nombreUsuario });
            });
        }
    }
}