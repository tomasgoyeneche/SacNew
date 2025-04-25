using Shared.Models;

namespace Core.Repositories
{
    public interface IEmpresaRepositorio
    {
        // Busqueda y Obtener Por Id
        Task<List<EmpresaDto>> ObtenerTodasLasEmpresasAsync();

        Task<List<Empresa>> ObtenerTodasLasEmpresas();
        Task<Empresa?> ObtenerPorIdAsync(int idEmpresa);

        Task<EmpresaDto> ObtenerPorIdDto(int idEmpresa);

        Task<List<EmpresaDto>> BuscarEmpresasAsync(string textoBusqueda);

        // Agregar Empresa

        Task AgregarEmpresaAsync(Empresa nuevaEmpresa);

        Task EliminarEmpresaAsync(int idEmpresa);

        Task ActualizarAsync(Empresa empresa);
    }
}