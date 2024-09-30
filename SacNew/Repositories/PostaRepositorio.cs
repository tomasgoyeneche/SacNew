using SacNew.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    public class PostaRepositorio : IPostaRepositorio
    {
        private readonly string _connectionString;

        public PostaRepositorio()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
        }

        public List<Posta> ObtenerTodasLasPostas()
        {
            var postas = new List<Posta>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Posta";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var posta = new Posta
                        {
                            Id = reader.GetInt32(0),
                            Codigo = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            Direccion = reader.GetString(3),
                            ProvinciaId = reader.GetInt32(4)
                        };
                        postas.Add(posta);
                    }
                }
            }
            return postas;
        }

        public List<Posta> BuscarPostas(string textoBusqueda)
        {
            var postas = new List<Posta>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Posta WHERE Codigo LIKE @TextoBusqueda OR Descripcion LIKE @TextoBusqueda";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TextoBusqueda", $"%{textoBusqueda}%");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var posta = new Posta
                        {
                            Id = reader.GetInt32(0),
                            Codigo = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            Direccion = reader.GetString(3),
                            ProvinciaId = reader.GetInt32(4)
                        };
                        postas.Add(posta);
                    }
                }
            }
            return postas;
        }

        public void AgregarPosta(Posta nuevaPosta)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Posta (Codigo, Descripcion, Direccion, idProvincia) VALUES (@Codigo, @Descripcion, @Direccion, @idProvincia)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Codigo", nuevaPosta.Codigo);
                command.Parameters.AddWithValue("@Descripcion", nuevaPosta.Descripcion);
                command.Parameters.AddWithValue("@Direccion", nuevaPosta.Direccion);
                command.Parameters.AddWithValue("@idProvincia", nuevaPosta.ProvinciaId);

                command.ExecuteNonQuery();
            }
        }

        public void ActualizarPosta(Posta postaActualizada)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Posta SET Codigo = @Codigo, Descripcion = @Descripcion, Direccion = @Direccion, idProvincia = @ProvinciaId WHERE IdPosta = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", postaActualizada.Id);
                command.Parameters.AddWithValue("@Codigo", postaActualizada.Codigo);
                command.Parameters.AddWithValue("@Descripcion", postaActualizada.Descripcion);
                command.Parameters.AddWithValue("@Direccion", postaActualizada.Direccion);
                command.Parameters.AddWithValue("@ProvinciaId", postaActualizada.ProvinciaId);

                command.ExecuteNonQuery();
            }
        }

        public void EliminarPosta(int idPosta)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Posta WHERE IdPosta = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", idPosta);

                command.ExecuteNonQuery();
            }
        }
    }
}