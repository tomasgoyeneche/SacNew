using SacNew.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    internal class ConceptoRepositorio : IConceptoRepositorio
    {
        private readonly string _connectionString;

        public ConceptoRepositorio()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
        }

        public List<Concepto> ObtenerTodosLosConceptos()
        {
            var conceptos = new List<Concepto>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Concepto where activo = 1";
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        conceptos.Add(new Concepto
                        {
                            IdConsumo = reader.GetInt32(0),
                            Codigo = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            IdTipoConsumo = reader.GetInt32(3),
                            PrecioActual = reader.GetDecimal(4),
                            Vigencia = reader.GetDateTime(5),
                            PrecioAnterior = reader.GetDecimal(6),
                            Activo = reader.GetBoolean(7),
                            IdUsuario = reader.GetInt32(8),
                            FechaModificacion = reader.GetDateTime(9)
                        });
                    }
                }
            }
            return conceptos;
        }

        public Concepto ObtenerPorId(int idConsumo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Concepto WHERE IdConsumo = @IdConsumo";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdConsumo", idConsumo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Concepto
                        {
                            IdConsumo = reader.GetInt32(0),
                            Codigo = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            IdTipoConsumo = reader.GetInt32(3),
                            PrecioActual = reader.GetDecimal(4),
                            Vigencia = reader.GetDateTime(5),
                            PrecioAnterior = reader.GetDecimal(6),
                            Activo = reader.GetBoolean(7),
                            IdUsuario = reader.GetInt32(8),
                            FechaModificacion = reader.GetDateTime(9)
                        };
                    }
                }
            }
            return null;
        }

        public List<Concepto> BuscarConceptos(string textoBusqueda)
        {
            var conceptos = new List<Concepto>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Concepto WHERE Codigo LIKE @Busqueda OR Descripcion LIKE @Busqueda";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Busqueda", "%" + textoBusqueda + "%");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        conceptos.Add(new Concepto
                        {
                            IdConsumo = reader.GetInt32(0),
                            Codigo = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            IdTipoConsumo = reader.GetInt32(3),
                            PrecioActual = reader.GetDecimal(4),
                            Vigencia = reader.GetDateTime(5),
                            PrecioAnterior = reader.GetDecimal(6),
                            Activo = reader.GetBoolean(7),
                            IdUsuario = reader.GetInt32(8),
                            FechaModificacion = reader.GetDateTime(9)
                        });
                    }
                }
            }
            return conceptos;
        }

        public void AgregarConcepto(Concepto concepto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Concepto (Codigo, Descripcion, idConsumoTipo, PrecioActual, Vigencia, PrecioAnterior, Activo, IdUsuario, FechaModificacion) " +
                               "VALUES (@Codigo, @Descripcion, @IdTipoConsumo, @PrecioActual, @Vigencia, @PrecioAnterior, @Activo, @IdUsuario, @FechaModificacion)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Codigo", concepto.Codigo);
                command.Parameters.AddWithValue("@Descripcion", concepto.Descripcion);
                command.Parameters.AddWithValue("@IdTipoConsumo", concepto.IdTipoConsumo);
                command.Parameters.AddWithValue("@PrecioActual", concepto.PrecioActual);
                command.Parameters.AddWithValue("@Vigencia", concepto.Vigencia);
                command.Parameters.AddWithValue("@PrecioAnterior", concepto.PrecioAnterior);
                command.Parameters.AddWithValue("@Activo", concepto.Activo);
                command.Parameters.AddWithValue("@IdUsuario", concepto.IdUsuario);
                command.Parameters.AddWithValue("@FechaModificacion", concepto.FechaModificacion);
                command.ExecuteNonQuery();
            }
        }

        public void ActualizarConcepto(Concepto concepto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Concepto SET Codigo = @Codigo, Descripcion = @Descripcion, idConsumoTipo = @IdTipoConsumo, PrecioActual = @PrecioActual, " +
                               "Vigencia = @Vigencia, PrecioAnterior = @PrecioAnterior, Activo = @Activo, IdUsuario = @IdUsuario, FechaModificacion = @FechaModificacion " +
                               "WHERE IdConsumo = @IdConsumo";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Codigo", concepto.Codigo);
                command.Parameters.AddWithValue("@Descripcion", concepto.Descripcion);
                command.Parameters.AddWithValue("@IdTipoConsumo", concepto.IdTipoConsumo);
                command.Parameters.AddWithValue("@PrecioActual", concepto.PrecioActual);
                command.Parameters.AddWithValue("@Vigencia", concepto.Vigencia);
                command.Parameters.AddWithValue("@PrecioAnterior", concepto.PrecioAnterior);
                command.Parameters.AddWithValue("@Activo", concepto.Activo);
                command.Parameters.AddWithValue("@IdUsuario", concepto.IdUsuario);
                command.Parameters.AddWithValue("@FechaModificacion", concepto.FechaModificacion);
                command.Parameters.AddWithValue("@IdConsumo", concepto.IdConsumo);
                command.ExecuteNonQuery();
            }
        }

        public void EliminarConcepto(int idConsumo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Concepto SET Activo = 0 WHERE IdConsumo = @IdConsumo";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdConsumo", idConsumo);
                command.ExecuteNonQuery();
            }
        }
    }
}