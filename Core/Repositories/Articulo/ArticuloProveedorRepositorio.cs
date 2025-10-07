using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ArticuloProveedorRepositorio : BaseRepositorio, IArticuloProveedorRepositorio
    {
        public ArticuloProveedorRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> AgregarAsync(ArticuloProveedor proveedor)
        {
            const string query = @"
        INSERT INTO Proveedor (RazonSocial, CUIT, Direccion, Telefono, Email, Activo)
        VALUES (@RazonSocial, @CUIT, @Direccion, @Telefono, @Email, @Activo);
        SELECT CAST(SCOPE_IDENTITY() as int);";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteScalarAsync<int>(query, proveedor);
            });
        }

        public async Task<int> ActualizarAsync(ArticuloProveedor proveedor)
        {
            const string query = @"
        UPDATE Proveedor
        SET RazonSocial = @RazonSocial,
            CUIT = @CUIT,
            Direccion = @Direccion,
            Telefono = @Telefono,
            Email = @Email,
            Activo = @Activo
        WHERE IdProveedor = @IdProveedor";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteAsync(query, proveedor);
            });
        }

        public async Task<int> EliminarAsync(int idProveedor)
        {
            const string query = "DELETE FROM Proveedor WHERE IdProveedor = @IdProveedor";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteAsync(query, new { IdProveedor = idProveedor });
            });
        }

        public async Task<ArticuloProveedor?> ObtenerPorIdAsync(int idProveedor)
        {
            const string query = "SELECT * FROM Proveedor WHERE IdProveedor = @IdProveedor";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<ArticuloProveedor>(query, new { IdProveedor = idProveedor });
            });
        }

        public async Task<List<ArticuloProveedor>> ObtenerTodosAsync()
        {
            const string query = "SELECT * FROM Proveedor ORDER BY RazonSocial";

            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<ArticuloProveedor>(query);
                return result.ToList();
            });
        }
    }

}
