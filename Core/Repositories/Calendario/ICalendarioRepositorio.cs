using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ICalendarioRepositorio
    {
        Task<List<(int Mes, int Anio)>> ObtenerMesesAniosDisponiblesAsync();
        //Task<List<CalendarioDiaDto>> ObtenerDiasPorMesYAnioAsync(int mes, int anio);

        Task<List<(DateTime Fecha, int Dia)>> ObtenerDiasPorMesYAnioAsync(int mes, int anio);

        Task<List<int>> ObtenerAniosDisponiblesAsync();
        
    }
}
