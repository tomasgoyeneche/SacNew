using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using Shared.Models.RequerimientoCompra;

namespace Core.Repositories.RequerimientoCompra
{
    public class UsuarioRccRepositorio : BaseRepositorio, IUsuarioRccRepositorio
    {
        public UsuarioRccRepositorio(ConnectionStrings cs, ISesionService sesion)
            : base(cs, sesion) { }

        public async Task<List<UsuarioRcc>> ObtenerActivosAsync()
        {
            const string sql = @"
        SELECT
            IdUsuario,
            usuario AS UsuarioLogin,
            clave,
            nombreApellido,
            funcion,
            administrador,
            activo,
            fechaAlta
        FROM Usuario
        WHERE Activo = 1
        ORDER BY nombreApellido";

            return (await ConectarAsyncFo(conn =>
                conn.QueryAsync<UsuarioRcc>(sql)
            )).ToList();
        }

        public async Task<UsuarioRcc?> ObtenerPorIdAsync(int idUsuario)
        {
            const string sql = @"
        SELECT
            IdUsuario,
            usuario AS UsuarioLogin,
            clave,
            nombreApellido,
            funcion,
            administrador,
            activo,
            fechaAlta
        FROM Usuario
        WHERE IdUsuario = @idUsuario";

            return await ConectarAsyncFo(conn =>
                conn.QueryFirstOrDefaultAsync<UsuarioRcc>(sql, new { idUsuario })
            );
        }
    }
}