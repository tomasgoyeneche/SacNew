using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public class LocacionRepositorio : ILocacionRepositorio
    {
        private readonly string _connectionString;

        public LocacionRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Locacion>> ObtenerTodasAsync()
        {
            var locaciones = new List<Locacion>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Locacion WHERE Activo = 1";  // Solo locaciones activas

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var locacion = new Locacion
                            {
                                IdLocacion = reader.GetInt32(reader.GetOrdinal("IdLocacion")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                Carga = reader.GetBoolean(reader.GetOrdinal("Carga")),
                                Descarga = reader.GetBoolean(reader.GetOrdinal("Descarga")),
                                Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                            };

                            locaciones.Add(locacion);
                        }
                    }
                }
            }

            return locaciones;
        }

        public async Task<List<Locacion>> BuscarPorCriterioAsync(string criterio)
        {
            var locaciones = new List<Locacion>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Locacion WHERE Activo = 1 AND (Nombre LIKE @Criterio OR Direccion LIKE @Criterio)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Criterio", $"%{criterio}%");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var locacion = new Locacion
                            {
                                IdLocacion = reader.GetInt32(reader.GetOrdinal("IdLocacion")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                Carga = reader.GetBoolean(reader.GetOrdinal("Carga")),
                                Descarga = reader.GetBoolean(reader.GetOrdinal("Descarga")),
                                Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                            };

                            locaciones.Add(locacion);
                        }
                    }
                }
            }

            return locaciones;
        }

        public async Task EliminarAsync(int idLocacion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE Locacion SET Activo = 0 WHERE IdLocacion = @IdLocacion";  // Baja lógica

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdLocacion", idLocacion);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
