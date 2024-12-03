using Dapper;
using SacNew.Models;
using SacNew.Services;
using static SacNew.Services.Startup;

namespace SacNew.Repositories
{
    public class LocacionProductoRepositorio : BaseRepositorio, ILocacionProductoRepositorio
    {
        public LocacionProductoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<List<LocacionProducto>> ObtenerPorLocacionIdAsync(int idLocacion)
        {
            var query = @"
    SELECT lp.IdLocacionProducto, lp.IdLocacion, lp.IdProducto, p.IdProducto, p.Nombre
    FROM LocacionProducto lp
    INNER JOIN Producto p ON lp.IdProducto = p.IdProducto
    WHERE lp.IdLocacion = @IdLocacion";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<LocacionProducto, Producto, LocacionProducto>(
                    query,
                    (locacionProducto, producto) =>
                    {
                        locacionProducto.Producto = producto;  // Asignar el producto mapeado
                        return locacionProducto;
                    },
                    new { IdLocacion = idLocacion },  // Parámetro para la consulta
                    splitOn: "IdProducto"  // Indicar dónde empieza el objeto Producto
                ).ContinueWith(task => task.Result.ToList());
            });
        }

        public Task AgregarAsync(LocacionProducto locacionProducto)
        {
            var query = "INSERT INTO LocacionProducto (IdLocacion, IdProducto) VALUES (@IdLocacion, @IdProducto)";

            return EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, locacionProducto),
                "LocacionProducto",
                "INSERT",
                null,  // No hay valores anteriores ya que es un nuevo registro
                locacionProducto  // Pasamos directamente el objeto sin serializar
            );
        }

        public async Task EliminarAsync(int idLocacionProducto)
        {
            var query = "DELETE FROM LocacionProducto WHERE IdLocacionProducto = @IdLocacionProducto";

            // Obtener el producto antes de eliminar para la auditoría
            var locacionProductoAnterior = await ObtenerPorIdAsync(idLocacionProducto);

            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, new { IdLocacionProducto = idLocacionProducto }),
                "LocacionProducto",
                "DELETE",
                locacionProductoAnterior,  // Pasamos directamente el objeto sin serializar
                null  // No hay valores nuevos ya que se elimina el registro
            );
        }

        private async Task<LocacionProducto?> ObtenerPorIdAsync(int idLocacionProducto)
        {
            var query = "SELECT * FROM LocacionProducto WHERE IdLocacionProducto = @IdLocacionProducto";

            return await ConectarAsync(connection =>
                connection.QueryFirstOrDefaultAsync<LocacionProducto>(query, new { IdLocacionProducto = idLocacionProducto }));
        }
    }
}