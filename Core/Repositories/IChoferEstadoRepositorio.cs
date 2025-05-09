using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IChoferEstadoRepositorio
    {
        Task<List<NovedadesChoferesDto>> ObtenerNovedadesDto();

        Task<List<NovedadesChoferesDto>> ObtenerTodasLasNovedadesDto();

        Task<List<ChoferTipoEstado>> ObtenerEstados();

        Task AltaNovedadAsync(ChoferEstado choferEstado, int idUsuario);

        Task EditarNovedadAsync(ChoferEstado choferEstado, int idUsuario);

        Task EliminarNovedadAsync(NovedadesChoferesDto choferEstado, int idUsuario);

        Task<List<NovedadesChoferesDto>> ObtenerPorMesYAnioAsync(int mes, int anio);
        Task<List<NovedadesChoferesDto>> ObtenerPorAnioAsync(int anio);
    }
}
