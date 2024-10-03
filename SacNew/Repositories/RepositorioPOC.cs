using SacNew.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public class RepositorioPOC : IRepositorioPOC
    {
        private readonly string _connectionString;

        public RepositorioPOC(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<POCDto> ObtenerTodos()
        {
            var listaPOC = new List<POCDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Agrega la columna Id en la consulta
                var query = "SELECT IdPoc, NumeroPOC, PatenteTractor, PatenteSemi, NombreFantasia, NombreChofer, ApellidoChofer FROM POC_NominaDetalle WHERE Activo = 1";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
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

        public List<POCDto> BuscarPOC(string criterio)
        {
            var listaPOC = new List<POCDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Agrega la columna Id en la consulta
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

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
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
        public void EliminarPOC(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "UPDATE POC SET Activo = 0 WHERE IdPoc = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
