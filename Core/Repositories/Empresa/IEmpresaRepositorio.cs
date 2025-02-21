using Shared.Models;

namespace Core.Repositories
{
    public interface IEmpresaRepositorio
    {
        Task<List<EmpresaDto>> ObtenerTodasLasEmpresasAsync();

        Task<Empresa?> ObtenerPorIdAsync(int idEmpresa);

        Task<EmpresaDto> ObtenerPorIdDto(int idEmpresa);

        Task<List<EmpresaDto>> BuscarEmpresasAsync(string textoBusqueda);

        Task AgregarEmpresaAsync(Empresa nuevaEmpresa);

        Task EliminarEmpresaAsync(int idEmpresa);

        Task ActualizarAsync(Empresa empresa);
    }
}