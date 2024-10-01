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
    internal class ConceptoProveedorRepositorio : IConceptoProveedorRepositorio
    {
        private readonly string _connectionString;

        public ConceptoProveedorRepositorio()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
        }

        public List<Proveedor> ObtenerTodosLosProveedores()
        {
            var proveedores = new List<Proveedor>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ConceptoProveedor WHERE Activo = 1";  // Solo proveedores activos
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        proveedores.Add(new Proveedor
                        {
                            IdProveedor = reader.GetInt32(0),
                            Codigo = reader.GetString(1),
                            RazonSocial = reader.GetString(2),
                            NumeroFicha = reader.GetInt32(3),
                            Activo = reader.GetBoolean(4)
                        });
                    }
                }
            }

            return proveedores;
        }
    }
}
