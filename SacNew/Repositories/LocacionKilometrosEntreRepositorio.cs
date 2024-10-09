using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    internal class LocacionKilometrosEntreRepositorio : ILocacionKilometrosEntreRepositorio
    {
        private readonly string _connectionString;

        public LocacionKilometrosEntreRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<LocacionKilometrosEntre>> ObtenerPorLocacionIdAsync(int idLocacion)
        {
            var kilometrosEntre = new List<LocacionKilometrosEntre>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT lk.*, l.Nombre AS LocacionDestinoNombre 
                          FROM LocacionKilometrosEntre lk 
                          INNER JOIN Locacion l ON lk.IdLocacionDestino = l.IdLocacion
                          WHERE lk.IdLocacionOrigen = @IdLocacion";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdLocacion", idLocacion);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        kilometrosEntre.Add(new LocacionKilometrosEntre
                        {
                            IdKilometros = (int)reader["IdKilometros"],
                            IdLocacionOrigen = (int)reader["IdLocacionOrigen"],
                            IdLocacionDestino = (int)reader["IdLocacionDestino"],
                            Kilometros = (int)reader["Kilometros"],
                            LocacionDestino = new Locacion
                            {
                                IdLocacion = (int)reader["IdLocacionDestino"],
                                Nombre = (string)reader["LocacionDestinoNombre"]
                            }
                        });
                    }
                }
            }
            return kilometrosEntre;
        }

        public async Task AgregarAsync(LocacionKilometrosEntre locacionKilometrosEntre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"INSERT INTO LocacionKilometrosEntre (IdLocacionOrigen, IdLocacionDestino, Kilometros) 
                          VALUES (@IdLocacionOrigen, @IdLocacionDestino, @Kilometros)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdLocacionOrigen", locacionKilometrosEntre.IdLocacionOrigen);
                command.Parameters.AddWithValue("@IdLocacionDestino", locacionKilometrosEntre.IdLocacionDestino);
                command.Parameters.AddWithValue("@Kilometros", locacionKilometrosEntre.Kilometros);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task EliminarAsync(int idKilometros)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM LocacionKilometrosEntre WHERE IdKilometros = @IdKilometros";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdKilometros", idKilometros);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
