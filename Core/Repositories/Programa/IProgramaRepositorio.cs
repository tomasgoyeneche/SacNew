using Shared.Models;

namespace Core.Repositories
{
    public interface IProgramaRepositorio
    {
        Task<List<Ruteo>> ObtenerRuteoAsync();

        Task<List<RuteoResumen>> ObtenerResumenAsync();

        Task<decimal?> ObtenerOdometerPorNomina(int idNomina);

        Task<Programa?> ObtenerPorIdAsync(int idPrograma);

        Task ActualizarFechaYRegistrarAsync(
         int idPrograma,
         string campo,           // Ej: "CargaLlegada"
         DateTime? fechaNueva,
         int idNomina,         // null si elimina
         int idUsuario);

        Task RegistrarProgramaAsync(int idPrograma, string motivo, string descripcion, int idUsuario);

        Task ActualizarProgramaAsync(Programa programa);

        Task ActualizarCheck(int idPrograma, string campoCheck, string nombreUsuario);

        Task ActualizarRutaRemitoAsync(int idPrograma, string campoRuta, string ruta);

        Task ActualizarProgramaOrigenProductoAsync(int idPrograma, int idOrigen, int idProducto);

        Task ActualizarProgramaTramoDestinoAsync(int idPrograma, int idDestino);

        Task<int> InsertarProgramaRetornandoIdAsync(Programa programa);

        Task InsertarProgramaTramoAsync(ProgramaTramo tramo);

        Task CerrarTramosActivosPorProgramaAsync(int idPrograma);

        Task<ProgramaEstado?> ObtenerEstadoDeBajaPorIdAsync(int idMotivo);

        Task<List<ProgramaEstado>> ObtenerEstadosDeBajaAsync();

        Task<List<VistaPrograma>> ObtenerVistaProgramasAsync();

        Task<List<ProgramaDemoradoInforme>> ObtenerProgramasDemoradosAsync();

        Task<List<Transoft>> ObtenerTransoftAsync(DateTime desde, DateTime hasta);

        Task<List<TransoftMetanol>> ObtenerTransoftMetanolAsync(DateTime desde, DateTime hasta);
    }
}