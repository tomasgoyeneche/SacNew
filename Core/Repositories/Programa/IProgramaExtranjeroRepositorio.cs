using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IProgramaExtranjeroRepositorio
    {
        Task<List<ProgramaExtranjero>> ObtenerHitosextranjerosPorProgramaAsync(int idPrograma);
        Task InsertarHitoExtranjeroAsync(ProgramaExtranjero hito);
        Task ActualizarHitoExtranjeroAsync(int idProgramaExtranjero, DateTime nuevaFecha, decimal odometro);
        Task BajaHitoExtranjeroAsync(int idProgramaExtranjero);
    }
}
