using SacNew.Models;
using SacNew.Models.DTOs;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    public class RepositorioPOC : IRepositorioPOC
    {
        private readonly string _connectionString;

        public RepositorioPOC(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<POCDto>> ObtenerTodosAsync()
        {
            var listaPOC = new List<POCDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT IdPoc, NumeroPOC, PatenteTractor, PatenteSemi, NombreFantasia, NombreChofer, ApellidoChofer FROM POC_NominaDetalle WHERE Activo = 1";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var pocDto = new POCDto
                            {
                                IdPoc = Convert.ToInt32(reader["IdPoc"]),
                                NumeroPOC = reader["NumeroPOC"].ToString(),
                                PatenteTractor = reader["PatenteTractor"].ToString(),
                                PatenteSemi = reader["PatenteSemi"].ToString(),
                                NombreFantasia = reader["NombreFantasia"].ToString(),
                                NombreChofer = reader["NombreChofer"].ToString(),
                                ApellidoChofer = reader["ApellidoChofer"].ToString(),
                            };
                            listaPOC.Add(pocDto);
                        }
                    }
                }
            }

            return listaPOC;
        }

        public async Task<List<POCDto>> BuscarPOCAsync(string criterio)
        {
            var listaPOC = new List<POCDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                SELECT IdPoc, NumeroPOC, PatenteTractor, PatenteSemi, NombreFantasia, NombreChofer, ApellidoChofer
                FROM POC_NominaDetalle
                WHERE Activo = 1
                AND (NumeroPOC LIKE @Criterio
                OR PatenteTractor LIKE @Criterio
                OR PatenteSemi LIKE @Criterio
                OR NombreFantasia LIKE @Criterio
                OR NombreChofer LIKE @Criterio
                OR ApellidoChofer LIKE @Criterio)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Criterio", "%" + criterio + "%");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var pocDto = new POCDto
                            {
                                IdPoc = Convert.ToInt32(reader["IdPoc"]),
                                NumeroPOC = reader["NumeroPOC"].ToString(),
                                PatenteTractor = reader["PatenteTractor"].ToString(),
                                PatenteSemi = reader["PatenteSemi"].ToString(),
                                NombreFantasia = reader["NombreFantasia"].ToString(),
                                NombreChofer = reader["NombreChofer"].ToString(),
                                ApellidoChofer = reader["ApellidoChofer"].ToString(),
                            };
                            listaPOC.Add(pocDto);
                        }
                    }
                }
            }

            return listaPOC;
        }

        public async Task EliminarPOCAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "UPDATE POC SET Activo = 0 WHERE IdPoc = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<POC> ObtenerPorIdAsync(int idPoc)
        {
            POC poc = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                SELECT IdPoc, NumeroPOC, IdPosta, IdNomina, Odometro, Comentario, FechaCreacion, IdUsuario, Activo
                FROM POC
                WHERE IdPoc = @IdPoc AND Activo = 1";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPoc", idPoc);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            poc = new POC
                            {
                                IdPOC = Convert.ToInt32(reader["IdPoc"]),
                                NumeroPOC = reader["NumeroPOC"].ToString(),
                                IdPosta = Convert.ToInt32(reader["IdPosta"]),
                                IdNomina = Convert.ToInt32(reader["IdNomina"]),
                                Odometro = Convert.ToDouble(reader["Odometro"]),
                                Comentario = reader["Comentario"].ToString(),
                                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                Activo = Convert.ToBoolean(reader["Activo"])
                            };
                        }
                    }
                }
            }

            return poc;
        }

        public async Task AgregarPOCAsync(POC poc)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                INSERT INTO POC (NumeroPOC, IdPosta, IdNomina, Odometro, Comentario, FechaCreacion, IdUsuario, Activo)
                VALUES (@NumeroPOC, @IdPosta, @IdNomina, @Odometro, @Comentario, @FechaCreacion, @IdUsuario, 1)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumeroPOC", poc.NumeroPOC);
                    command.Parameters.AddWithValue("@IdPosta", poc.IdPosta);
                    command.Parameters.AddWithValue("@IdNomina", poc.IdNomina);
                    command.Parameters.AddWithValue("@Odometro", poc.Odometro);
                    command.Parameters.AddWithValue("@Comentario", poc.Comentario);
                    command.Parameters.AddWithValue("@FechaCreacion", poc.FechaCreacion);
                    command.Parameters.AddWithValue("@IdUsuario", poc.IdUsuario);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task ActualizarPOCAsync(POC poc)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                UPDATE POC
                SET NumeroPOC = @NumeroPOC,
                    IdPosta = @IdPosta,
                    IdNomina = @IdNomina,
                    Odometro = @Odometro,
                    Comentario = @Comentario,
                    FechaCreacion = @FechaCreacion,
                    IdUsuario = @IdUsuario
                WHERE IdPoc = @IdPoc";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumeroPOC", poc.NumeroPOC);
                    command.Parameters.AddWithValue("@IdPosta", poc.IdPosta);
                    command.Parameters.AddWithValue("@IdNomina", poc.IdNomina);
                    command.Parameters.AddWithValue("@Odometro", poc.Odometro);
                    command.Parameters.AddWithValue("@Comentario", poc.Comentario);
                    command.Parameters.AddWithValue("@FechaCreacion", poc.FechaCreacion);
                    command.Parameters.AddWithValue("@IdUsuario", poc.IdUsuario);
                    command.Parameters.AddWithValue("@IdPoc", poc.IdPOC);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}