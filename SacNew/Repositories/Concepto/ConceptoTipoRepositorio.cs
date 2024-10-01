using SacNew.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    internal class ConceptoTipoRepositorio : IConceptoTipoRepositorio
    {
        private readonly string _connectionString;

        public ConceptoTipoRepositorio()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
        }

        public string ObtenerDescripcionPorId(int idConsumoTipo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Descripcion FROM ConceptoTipo WHERE IdConsumoTipo = @IdConsumoTipo";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdConsumoTipo", idConsumoTipo);

                var descripcion = command.ExecuteScalar()?.ToString();
                return descripcion ?? "Tipo no encontrado";
            }
        }

        public List<ConceptoTipo> ObtenerTodosLosTipos()
        {
            var tipos = new List<ConceptoTipo>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ConceptoTipo";
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tipos.Add(new ConceptoTipo
                        {
                            IdConsumoTipo = reader.GetInt32(0),
                            Codigo = reader.GetString(1),
                            Descripcion = reader.GetString(2)
                        });
                    }
                }
            }
            return tipos;
        }
    }
}