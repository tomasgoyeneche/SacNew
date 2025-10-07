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
    public class ArticuloStockRepositorio : BaseRepositorio, IArticuloStockRepositorio
    {
        public ArticuloStockRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> CrearStockAsync(int idArticulo, int idPosta, decimal cantidadInicial = 0)
        {
            const string query = @"
        INSERT INTO ArticuloStock (IdArticulo, IdPosta, CantidadActual)
        VALUES (@IdArticulo, @IdPosta, @CantidadActual);
        SELECT CAST(SCOPE_IDENTITY() as int);";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteScalarAsync<int>(query, new
                {
                    IdArticulo = idArticulo,
                    IdPosta = idPosta,
                    CantidadActual = cantidadInicial
                });
            });
        }

        public async Task<ArticuloStock?> ObtenerStockAsync(int idArticulo, int idPosta)
        {
            const string query = @"
        SELECT * 
        FROM ArticuloStock 
        WHERE IdArticulo = @IdArticulo AND IdPosta = @IdPosta";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<ArticuloStock>(query, new { IdArticulo = idArticulo, IdPosta = idPosta });
            });
        }

        public async Task<int> ActualizarStockAsync(ArticuloStock stock)
        {
            const string query = @"
        UPDATE ArticuloStock
        SET CantidadActual = @CantidadActual
        WHERE IdArticuloStock = @IdArticuloStock";

            return await ConectarAsync(async conn =>
            {
                return await conn.ExecuteAsync(query, stock);
            });
        }
    }
}
