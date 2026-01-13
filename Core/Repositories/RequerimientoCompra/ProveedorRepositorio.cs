using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using Shared.Models.RequerimientoCompra;

namespace Core.Repositories.RequerimientoCompra
{
    public class ProveedorRepositorio : BaseRepositorio, IProveedorRccRepositorio
    {
        public ProveedorRepositorio(ConnectionStrings cs, ISesionService sesion)
            : base(cs, sesion) { }

        public async Task<List<ProveedorRcc>> ObtenerActivosAsync()
        {
            const string sql = @"
        SELECT *
        FROM Proveedor
        WHERE Activo = 1
        ORDER BY RazonSocial";

            return (await ConectarAsyncFo(conn =>
                conn.QueryAsync<ProveedorRcc>(sql)
            )).ToList();
        }

        public async Task<ProveedorRcc?> ObtenerPorIdAsync(int idProveedor)
        {
            const string sql = @"SELECT * FROM Proveedor WHERE IdProveedor = @idProveedor";

            return await ConectarAsyncFo(conn =>
                conn.QueryFirstOrDefaultAsync<ProveedorRcc>(sql, new { idProveedor })
            );
        }
    }
}