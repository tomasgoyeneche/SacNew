using Dapper;
using SacNew.Models;
using SacNew.Services;
using static SacNew.Services.Startup;

namespace SacNew.Repositories
{
    internal class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {
        public UsuarioRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

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