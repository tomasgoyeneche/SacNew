using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
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

        public Task<List<Usuario>> ObtenerTodosAsync()
        {
            var query = "SELECT * FROM Usuario WHERE Activo = 1";

            return ConectarAsync(connection =>
            {
                return connection.QueryAsync<Usuario>(query)
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public Task<List<Usuario>> BuscarPorCriterioAsync(string criterio)
        {
            var query = "SELECT * FROM Usuario WHERE Activo = 1 AND (NombreUsuario LIKE @Criterio OR NombreCompleto LIKE @Criterio)";

            return ConectarAsync(connection =>
            {
                return connection.QueryAsync<Usuario>(query, new { Criterio = $"%{criterio}%" })
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task EliminarAsync(int idUsuario)
        {
            var query = "UPDATE Usuario SET Activo = 0 WHERE idUsuario = @idUsuario";

            // Obtener los valores anteriores antes de la eliminación
            var usuarioAnterior = await ObtenerPorIdAsync(idUsuario);

            // Registrar el cambio de estado como "Eliminado" en la auditoría
            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, new { IdUsuario = idUsuario }),
                "Usuario",
                "DELETE",
                usuarioAnterior,  // Pasamos los valores anteriores sin serializarlos
                null  // No hay valores nuevos ya que solo se desactiva el registro
            );
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int idUsuario)
        {
            return await ObtenerPorIdGenericoAsync<Usuario>("Usuario", "idUsuario", idUsuario);
        }

        public Task AgregarAsync(Usuario usuario)
        {
            var query = @"
            INSERT INTO Usuario (NombreUsuario, Contrasena, NombreCompleto, idPosta, Activo)
            VALUES (@NombreUsuario, @Contrasena, @NombreCompleto, @idPosta, @Activo)";

            return EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, usuario),
                "Usuario",
                "INSERT",
                null,
                usuario
            );
        }

        public async Task ActualizarAsync(Usuario usuario)
        {
            var usuarioAnterior = await ObtenerPorIdAsync(usuario.IdUsuario);

            var query = @"
            UPDATE Usuario
            SET NombreUsuario = @NombreUsuario, Contrasena = @Contrasena, NombreCompleto = @NombreCompleto, idPosta = @idPosta, Activo = @Activo
            WHERE idUsuario = @idUsuario";

            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, usuario),
                "Usuario",
                "UPDATE",
                usuarioAnterior,
                usuario
            );
        }
    }
}