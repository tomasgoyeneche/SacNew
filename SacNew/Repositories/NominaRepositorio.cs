using Dapper;
using SacNew.Models;
using SacNew.Services;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    public class NominaRepositorio : BaseRepositorio, INominaRepositorio
    {
        public NominaRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService)
        {
        }

        public List<Nomina> ObtenerTodasLasNominas()
        {
            var query = @"
            SELECT n.IdNomina, t.Patente AS PatenteTractor, s.Patente AS PatenteSemi, c.Nombre AS NombreChofer
            FROM Nomina n
            JOIN Unidad u ON n.idUnidad = u.idUnidad
            JOIN Tractor t ON u.idTractor = t.idTractor
            JOIN Semi s ON u.idSemi = s.idSemi
            JOIN Chofer c ON n.idChofer = c.idChofer";

            return Conectar(connection =>
            {
                var nominas = connection.Query<Nomina>(query).ToList();
                return nominas;
            });
        }
    }
}