using Dapper;
using SacNew.Models;
using SacNew.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public class ProductoRepositorio : BaseRepositorio, IProductoRepositorio
    {
        public ProductoRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService)
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
