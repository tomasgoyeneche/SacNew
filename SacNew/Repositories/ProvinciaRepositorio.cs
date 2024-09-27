using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public class ProvinciaRepositorio : IProvinciaRepositorio
    {
        private readonly string _connectionString;

        public ProvinciaRepositorio()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
        }

        public List<Provincia> ObtenerProvincias()
        {
            var provincias = new List<Provincia>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT idProvincia, nombreProvincia FROM Provincia";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var provincia = new Provincia
                        {
                            IdProvincia = reader.GetInt32(0),
                            NombreProvincia = reader.GetString(1)
                        };
                        provincias.Add(provincia);
                    }
                }
            }
            return provincias;
        }
    }
}
