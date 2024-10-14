using Dapper;
using SacNew.Models;
using SacNew.Services;

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

        public async Task<Nomina?> ObtenerPorIdAsync(int idNomina)
        {
            var query = @"
            SELECT n.IdNomina, n.IdEmpresa, t.Patente AS PatenteTractor, s.Patente AS PatenteSemi, c.Nombre AS NombreChofer
            FROM Nomina n
            JOIN Unidad u ON n.IdUnidad = u.IdUnidad
            JOIN Tractor t ON u.IdTractor = t.IdTractor
            JOIN Semi s ON u.IdSemi = s.IdSemi
            JOIN Chofer c ON n.IdChofer = c.IdChofer
            WHERE n.IdNomina = @IdNomina";

            return await ConectarAsync(async connection =>
            {
                var nomina = await connection.QueryFirstOrDefaultAsync<Nomina>(query, new { IdNomina = idNomina });
                return nomina;
            });
        }
    }
}