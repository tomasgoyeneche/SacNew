using SacNew.Models;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    public class NominaRepositorio : INominaRepositorio
    {
        private readonly string _connectionString;

        public NominaRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Nomina> ObtenerTodasLasNominas()
        {
            var listaNominas = new List<Nomina>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                SELECT n.IdNomina, t.Patente AS PatenteTractor, s.Patente AS PatenteSemi, c.Nombre AS NombreChofer
                FROM Nomina n
                JOIN Unidad u ON n.idUnidad = u.idUnidad
                JOIN Tractor t ON u.idTractor = t.idTractor
                JOIN Semi s ON u.idSemi = s.idSemi
                JOIN Chofer c ON n.idChofer = c.idChofer";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaNominas.Add(new Nomina
                            {
                                IdNomina = Convert.ToInt32(reader["IdNomina"]),
                                PatenteTractor = reader["PatenteTractor"].ToString(),
                                PatenteSemi = reader["PatenteSemi"].ToString(),
                                NombreChofer = reader["NombreChofer"].ToString()
                            });
                        }
                    }
                }
            }

            return listaNominas;
        }
    }
}