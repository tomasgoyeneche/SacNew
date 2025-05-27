using Shared.Models;

namespace Core.Repositories
{
    public interface IGuardiaRepositorio
    {
        Task<int> RegistrarIngresoAsync(GuardiaIngreso ingreso, int idUsuario, string Observacion);

        Task<int> RegistrarIngresoTransitoEspecialAsync(TransitoEspecial te, int idPosta, DateTime fechaIngreso, int idUsuario);

        Task<List<GuardiaDto>> ObtenerGuardiasPorPostaAsync(int idPosta);

        Task EliminarIngresoCompletoAsync(int idIngreso);

        Task ActualizarEstadoIngresoAsync(int idIngreso, int nuevoEstado);

        Task EliminarRegistroAsync(int idRegistro);

        Task<GuardiaEstado?> ObtenerEstadoPorIdAsync(int idEstado);

        Task RegistrarCambioEstadoAsync(int idGuardiaIngreso, int idUsuario, DateTime fecha, int nuevoEstado, string observacion);

        Task<List<GuardiaHistorialDto>> ObtenerHistorialPorIngresoAsync(int idGuardiaIngreso);

        Task<(int unidades, int tractores, int semis, int choferes)> ObtenerResumenEnParadorAsync(int idPosta);

        Task RegistrarSalidaAsync(int idGuardiaIngreso, int idUsuario, DateTime fecha, string observacion);

        Task<bool> EstaEnParadorAsync(string patente);
    }
}