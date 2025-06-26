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

        public Task<Producto?> ObtenerPorIdAsync(int idProducto)
        {
            return ObtenerPorIdGenericoAsync<Producto>("Producto", "IdProducto", idProducto);
        }

        public async Task<List<ProductoSinonimo>> ObtenerTodosSinonimosAsync()
        {
            var query = "SELECT * FROM ProductoSinonimo where Activo = 1";
            return (await ConectarAsync(conn =>
                conn.QueryAsync<ProductoSinonimo>(query))).ToList();
        }

        public async Task AgregarSinonimoAsync(ProductoSinonimo sinonimo)
        {
            var query = "INSERT INTO ProductoSinonimo (IdProducto, Sinonimo, IdUsuario) VALUES (@IdProducto, @Sinonimo, @IdUsuario)";
            await ConectarAsync(conn => conn.ExecuteAsync(query, sinonimo));
        }
    }
}