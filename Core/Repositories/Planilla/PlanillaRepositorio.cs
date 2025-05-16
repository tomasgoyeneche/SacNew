using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using Shared.Models.DTOs;

namespace Core.Repositories
{
    public class PlanillaRepositorio : BaseRepositorio, IPlanillaRepositorio
    {
        public PlanillaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        // METODOS DE BUSQUEDA POR ID O GENERAL

        public async Task<Planilla?> ObtenerPorIdAsync(int idPlanilla)
        {
            var query = "SELECT * FROM planilla WHERE idPlanilla = @IdPlanilla";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<Planilla>(query, new { IdPlanilla = idPlanilla }));
        }

        public async Task<List<PlanillaPreguntaDto>> ObtenerPreguntasPorPlanilla(int idPlanilla)
        {
            var query = "SELECT * FROM vw_PlanillaPreguntaDetalle where idPlanilla = @IdPlanilla";

            return await ConectarAsync(async connection =>
            {
                var planillas = await connection.QueryAsync<PlanillaPreguntaDto>(query, new { IdPlanilla = idPlanilla });
                return planillas.ToList();
            });
        }
    }
}