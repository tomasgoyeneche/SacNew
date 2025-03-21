using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class PeriodoRepositorio : BaseRepositorio, IPeriodoRepositorio
    {
        public PeriodoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<Periodo>> ObtenerPeriodosParaSeleccionAsync()
        {
            var fechaActual = DateTime.Today;
            int mesActual = fechaActual.Month;
            int anioActual = fechaActual.Year;
            int quincenaActual = fechaActual.Day <= 15 ? 1 : 2; // 🔹 Determina si estamos en 1ra o 2da quincena

            // 🔹 Obtener los tres períodos necesarios
            var query = @"
            SELECT * FROM Periodo 
            WHERE (Anio = @AnioActual AND Mes = @MesActual AND Quincena = @QuincenaActual)
               OR (Anio = @AnioAnterior AND Mes = @MesAnterior AND Quincena = @QuincenaAnterior)
               OR (Anio = @AnioSiguiente AND Mes = @MesSiguiente AND Quincena = @QuincenaSiguiente)
            ORDER BY Anio, Mes, Quincena";

            return await ConectarAsync(async connection =>
            {
                return (await connection.QueryAsync<Periodo>(query, new
                {
                    // 🔹 Período actual
                    AnioActual = anioActual,
                    MesActual = mesActual,
                    QuincenaActual = quincenaActual,

                    // 🔹 Período anterior
                    AnioAnterior = quincenaActual == 1 ? (mesActual == 1 ? anioActual - 1 : anioActual) : anioActual,
                    MesAnterior = quincenaActual == 1 ? (mesActual == 1 ? 12 : mesActual - 1) : mesActual,
                    QuincenaAnterior = quincenaActual == 1 ? 2 : 1,

                    // 🔹 Período siguiente
                    AnioSiguiente = quincenaActual == 2 ? (mesActual == 12 ? anioActual + 1 : anioActual) : anioActual,
                    MesSiguiente = quincenaActual == 2 ? (mesActual == 12 ? 1 : mesActual + 1) : mesActual,
                    QuincenaSiguiente = quincenaActual == 2 ? 1 : 2
                })).ToList();
            });
        }


        public Task<List<Periodo>> ObtenerPeriodosActivosAsync()
        {
            var query = "SELECT * FROM Periodo WHERE Activo = 1";

            return ConectarAsync(connection =>
            {
                return connection.QueryAsync<Periodo>(query)
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task<int?> ObtenerIdPeriodoPorMesAnioAsync(int mes, int anio)
        {
            var query = @"
            SELECT IdPeriodo 
            FROM Periodo 
            WHERE Mes = @Mes AND Anio = @Anio AND Quincena = 1"; 

        return await ConectarAsync(async conn =>
        {
            return await conn.QueryFirstOrDefaultAsync<int?>(query, new { Mes = mes, Anio = anio });
        });
        }
    }
}