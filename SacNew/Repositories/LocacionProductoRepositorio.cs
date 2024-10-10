using SacNew.Models;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    public class LocacionProductoRepositorio : ILocacionProductoRepositorio
    {
        private readonly string _connectionString;

        public LocacionProductoRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<LocacionProducto>> ObtenerPorLocacionIdAsync(int idLocacion)
        {
            var productos = new List<LocacionProducto>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT lp.*, p.Nombre AS ProductoNombre FROM LocacionProducto lp INNER JOIN Producto p ON lp.IdProducto = p.IdProducto WHERE lp.IdLocacion = @IdLocacion";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdLocacion", idLocacion);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        productos.Add(new LocacionProducto
                        {
                            IdLocacionProducto = (int)reader["IdLocacionProducto"],
                            IdLocacion = (int)reader["IdLocacion"],
                            IdProducto = (int)reader["IdProducto"],
                            Producto = new Producto
                            {
                                IdProducto = (int)reader["IdProducto"],
                                Nombre = (string)reader["ProductoNombre"]
                            }
                        });
                    }
                }
            }
            return productos;
        }

        public async Task AgregarAsync(LocacionProducto locacionProducto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO LocacionProducto (IdLocacion, IdProducto) VALUES (@IdLocacion, @IdProducto)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdLocacion", locacionProducto.IdLocacion);
                command.Parameters.AddWithValue("@IdProducto", locacionProducto.IdProducto);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task EliminarAsync(int idLocacionProducto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM LocacionProducto WHERE IdLocacionProducto = @IdLocacionProducto";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdLocacionProducto", idLocacionProducto);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}