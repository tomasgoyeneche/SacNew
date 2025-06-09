using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IProgramaRepositorio
    {
        Task<List<Ruteo>> ObtenerRuteoAsync();

        Task<List<RuteoResumen>> ObtenerResumenAsync();

        Task<Programa?> ObtenerPorIdAsync(int idPrograma);

        Task ActualizarFechaYRegistrarAsync(
     int idPrograma,
     string campo,           // Ej: "CargaLlegada"
     DateTime? fechaNueva,
     int idNomina,         // null si elimina
     int idUsuario);

        Task ActualizarProgramaAsync(Programa programa);
        Task ActualizarCheck(int idPrograma, string campoCheck, string nombreUsuario);
         Task ActualizarRutaRemitoAsync(int idPrograma, string campoRuta, string ruta);
    }
}
