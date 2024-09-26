using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    internal class PermisoRepositorio : IPermisoRepositorio
    {
        public List<int> ObtenerPermisosPorUsuario(int idUsuario)
        {
            List<int> permisos = new List<int>();
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT idPermiso FROM UsuarioPermiso WHERE idUsuario = @IdUsuario";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            permisos.Add(reader.GetInt32(0)); // Agregamos los permisos
                        }
                    }
                }
            }
            return permisos;
        }
    }
}
