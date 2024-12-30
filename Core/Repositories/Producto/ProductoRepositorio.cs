using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class ProductoRepositorio : BaseRepositorio, IProductoRepositorio
    {
        public ProductoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            var query = "SELECT * FROM Producto WHERE Activo = 1";

            return await ConectarAsync(async connection =>
            {
                var productos = await connection.QueryAsync<Producto>(query);
                return productos.ToList();
            });
        }
    }
}