using SacNew.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    internal class UsuarioRepositorio : IUsuarioRepositorio
    {
        public Usuario ObtenerPorNombreUsuario(string nombreUsuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT idUsuario, nombreUsuario,contrasena, nombreCompleto, activo FROM Usuario WHERE nombreUsuario = @NombreUsuario";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                IdUsuario = reader.GetInt32(0),
                                NombreUsuario = reader.GetString(1),
                                Contrasena = reader.GetString(2),
                                NombreCompleto = reader.GetString(3),
                                Activo = reader.GetBoolean(4)
                            };
                        }
                    }
                }
            }
            return null; // Si no se encuentra el usuario
        }
    }
}