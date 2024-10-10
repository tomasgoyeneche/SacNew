using SacNew.Models;
using System.Data;
using System.Data.SqlClient;

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
            using (var command = new SqlCommand("SELECT * FROM Locacion WHERE Activo = 1", connection))
            {
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        locaciones.Add(new Locacion
                        {
                            IdLocacion = reader.GetInt32(reader.GetOrdinal("IdLocacion")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                            Carga = reader.GetBoolean(reader.GetOrdinal("Carga")),
                            Descarga = reader.GetBoolean(reader.GetOrdinal("Descarga")),
                            Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                        });
                    }
                }
            }

            return locaciones;
        }

        public async Task<Locacion> ObtenerPorIdAsync(int idLocacion)
        {
            Locacion locacion = null;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SELECT * FROM Locacion WHERE IdLocacion = @IdLocacion", connection))
            {
                command.Parameters.Add("@IdLocacion", SqlDbType.Int).Value = idLocacion;
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        locacion = new Locacion
                        {
                            IdLocacion = reader.GetInt32(reader.GetOrdinal("IdLocacion")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                            Carga = reader.GetBoolean(reader.GetOrdinal("Carga")),
                            Descarga = reader.GetBoolean(reader.GetOrdinal("Descarga")),
                            Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                        };
                    }
                }
            }
            return locacion;
        }

        public async Task<List<Locacion>> BuscarPorCriterioAsync(string criterio)
        {
            var locaciones = new List<Locacion>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SELECT * FROM Locacion WHERE Activo = 1 AND (Nombre LIKE @Criterio OR Direccion LIKE @Criterio)", connection))
            {
                command.Parameters.Add("@Criterio", SqlDbType.NVarChar).Value = $"%{criterio}%";
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        locaciones.Add(new Locacion
                        {
                            IdLocacion = reader.GetInt32(reader.GetOrdinal("IdLocacion")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                            Carga = reader.GetBoolean(reader.GetOrdinal("Carga")),
                            Descarga = reader.GetBoolean(reader.GetOrdinal("Descarga")),
                            Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                        });
                    }
                }
            }

            return locaciones;
        }

        public async Task AgregarAsync(Locacion locacion)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("INSERT INTO Locacion (Nombre, Direccion, Carga, Descarga, Activo) VALUES (@Nombre, @Direccion, @Carga, @Descarga, @Activo)", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = locacion.Nombre;
                command.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = locacion.Direccion;
                command.Parameters.Add("@Carga", SqlDbType.Bit).Value = locacion.Carga;
                command.Parameters.Add("@Descarga", SqlDbType.Bit).Value = locacion.Descarga;
                command.Parameters.Add("@Activo", SqlDbType.Bit).Value = locacion.Activo;
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task ActualizarAsync(Locacion locacion)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("UPDATE Locacion SET Nombre = @Nombre, Direccion = @Direccion, Carga = @Carga, Descarga = @Descarga, Activo = @Activo WHERE IdLocacion = @IdLocacion", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = locacion.Nombre;
                command.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = locacion.Direccion;
                command.Parameters.Add("@Carga", SqlDbType.Bit).Value = locacion.Carga;
                command.Parameters.Add("@Descarga", SqlDbType.Bit).Value = locacion.Descarga;
                command.Parameters.Add("@Activo", SqlDbType.Bit).Value = locacion.Activo;
                command.Parameters.Add("@IdLocacion", SqlDbType.Int).Value = locacion.IdLocacion;
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task EliminarAsync(int idLocacion)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("UPDATE Locacion SET Activo = 0 WHERE IdLocacion = @IdLocacion", connection))
            {
                command.Parameters.Add("@IdLocacion", SqlDbType.Int).Value = idLocacion;
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}