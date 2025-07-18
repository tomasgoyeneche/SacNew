using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class PedidoRepositorio : BaseRepositorio, IPedidoRepositorio
    {
        public PedidoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<Cupeo>> ObtenerCupeoAsync()
        {
            var query = "SELECT * FROM vw_Cupeo";

            return await ConectarAsync(async connection =>
            {
                var cupeo = await connection.QueryAsync<Cupeo>(query);
                return cupeo.ToList();
            });
        }

        public async Task<List<PedidoDto>> ObtenerPedidosPendientes()
        {
            var query = "SELECT * FROM vw_PedidosPendientes";

            return await ConectarAsync(async connection =>
            {
                var cupeo = await connection.QueryAsync<PedidoDto>(query);
                return cupeo.ToList();
            });
        }

        public async Task EliminarPedidoAsync(int idPedido)
        {
            await EliminarGenéricoAsync<Pedido>("Pedido", idPedido);
        }

        public async Task InsertarPedidosAsync(IEnumerable<Pedido> pedidos)
        {
            var query = @"
        INSERT INTO Pedido
        (IdProducto, AlbaranDespacho, PedidoOr, IdLocacion, FechaEntrega, FechaCarga, Cantidad, IdChofer, IdUnidad, Observaciones, IdUsuario, Fecha, Activo)
        VALUES
        (@IdProducto, @AlbaranDespacho, @PedidoOr, @IdLocacion, @FechaEntrega, @FechaCarga, @Cantidad, @IdChofer, @IdUnidad, @Observaciones, @IdUsuario, @Fecha, @Activo)";

            await ConectarAsync(async conn =>
            {
                // Usa ExecuteAsync para batch insert
                await conn.ExecuteAsync(query, pedidos);
            });
        }
    }
}