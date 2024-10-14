using Dapper;
using SacNew.Models;
using SacNew.Services;

namespace SacNew.Repositories
{
    public class ProvinciaRepositorio : BaseRepositorio, IProvinciaRepositorio
    {
        public ProvinciaRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService) { }

        public List<Provincia> ObtenerProvincias()
        {
            var query = "SELECT idProvincia, nombreProvincia FROM Provincia";

            return Conectar(connection =>
            {
                return connection.Query<Provincia>(query).ToList(); // Dapper mapea directamente a la lista de Provincias
            });
        }
    }
}