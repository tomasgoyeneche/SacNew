using Shared.Models;

namespace Core.Repositories
{
    public interface IChoferRepositorio
    {
        // Obtener Chofer Por Id
        Task<List<Chofer?>> ObtenerTodosLosChoferes();

        Task<List<ChoferDto?>> ObtenerTodosLosChoferesDto();

        Task<ChoferDto?> ObtenerPorIdDtoAsync(int idChofer);

        Task<Chofer?> ObtenerPorIdAsync(int idChofer);

        // Obtener Por Otras Opciones

        Task<int?> ObtenerIdPorDocumentoAsync(string documento);

        Task<List<ChoferDto>> BuscarAsync(string textoBusqueda);

        // Actualizar, Editar, Eliminar

        Task EliminarChoferAsync(int idChofer);

        Task ActualizarAsync(Chofer chofer);

        Task ActualizarEmpresaChoferAsync(int idChofer, int idEmpresa);

        Task AltaChoferAsync(string nombre, string apellido, string documento, int idUsuario);

        Task ActualizarVencimientoChoferAsync(int idChofer, int idVencimiento, DateTime fechaActualizacion, int idUsuario);
    }
}