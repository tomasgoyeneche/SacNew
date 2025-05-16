using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class CalendarioRepositorio : BaseRepositorio, ICalendarioRepositorio
    {
        public CalendarioRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        {
        }

        public async Task<List<(int Mes, int Anio)>> ObtenerMesesAniosDisponiblesAsync()
        {
            const string query = @"
            SELECT DISTINCT Mes, Anio
            FROM Calendario
            ORDER BY Anio DESC, Mes DESC";

            return await ConectarAsync(async connection =>
            {
                var resultados = await connection.QueryAsync<(int Mes, int Anio)>(query);
                return resultados.ToList();
            });
        }

        public async Task<List<(DateTime Fecha, int Dia)>> ObtenerDiasPorMesYAnioAsync(int mes, int anio)
        {
            var query = @"
        SELECT Fecha, Dia
        FROM Calendario
        WHERE Mes = @Mes AND Anio = @Anio
        ORDER BY Fecha";

            return await ConectarAsync(async connection =>
            {
                var resultados = await connection.QueryAsync(query, new { Mes = mes, Anio = anio });

                return resultados
                    .Select(r => ((DateTime)r.Fecha, (int)r.Dia))
                    .ToList();
            });
        }

        public async Task<List<int>> ObtenerAniosDisponiblesAsync()
        {
            var query = "SELECT DISTINCT Anio FROM Calendario ORDER BY Anio DESC";
            return await ConectarAsync(async connection =>
            {
                var resultado = await connection.QueryAsync<int>(query);
                return resultado.ToList();
            });
        }
    }
}