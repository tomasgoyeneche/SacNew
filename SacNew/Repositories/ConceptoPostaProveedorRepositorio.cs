using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    internal class ConceptoPostaProveedorRepositorio : IConceptoPostaProveedorRepositorio
    {
        private readonly string _connectionString;

        public ConceptoPostaProveedorRepositorio()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
        }

        public void AgregarConceptoPostaProveedor(int idConsumo, int idPosta, int idProveedor)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO ConceptoPostaProveedor (IdConsumo, IdPosta, IdProveedor) VALUES (@IdConsumo, @IdPosta, @IdProveedor)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdConsumo", idConsumo);
                command.Parameters.AddWithValue("@IdPosta", idPosta);
                command.Parameters.AddWithValue("@IdProveedor", idProveedor);
                command.ExecuteNonQuery();
            }
        }

        public void ActualizarConceptoPostaProveedor(int idConsumo, int idPosta, int idProveedor)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE ConceptoPostaProveedor SET IdProveedor = @IdProveedor WHERE IdConsumo = @IdConsumo AND IdPosta = @IdPosta";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdConsumo", idConsumo);
                command.Parameters.AddWithValue("@IdPosta", idPosta);
                command.Parameters.AddWithValue("@IdProveedor", idProveedor);
                command.ExecuteNonQuery();
            }
        }
    }
}
