using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class EmpresaRepositorio : BaseRepositorio, IEmpresaRepositorio
    {
        public EmpresaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        { }

        public async Task<List<EmpresaDto>> ObtenerTodasLasEmpresasAsync()
        {
            var query = "SELECT * FROM vw_EmpresaDetalle";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<EmpresaDto>(query).ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task<List<Empresa>> ObtenerTodasLasEmpresas()
        {
            var query = "SELECT * FROM Empresa";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<Empresa>(query).ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task<EmpresaDto?> ObtenerPorIdDto(int idEmpresa)
        {
            var query = "SELECT * FROM vw_EmpresaDetalle where idEmpresa = @idEmpresa";

            return await ConectarAsync(connection =>
            {
                return connection.QueryFirstOrDefaultAsync<EmpresaDto>(query, new { idEmpresa = idEmpresa });
            });
        }

        public Task<Empresa?> ObtenerPorIdAsync(int idEmpresa)
        {
            return ObtenerPorIdGenericoAsync<Empresa>("Empresa", "IdEmpresa", idEmpresa);
        }

        public async Task<List<EmpresaDto>> BuscarEmpresasAsync(string textoBusqueda)
        {
            var query = "SELECT * FROM vw_EmpresaDetalle WHERE Cuit LIKE @TextoBusqueda OR NombreFantasia LIKE @TextoBusqueda";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<EmpresaDto>(query, new { TextoBusqueda = $"%{textoBusqueda}%" })
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task AgregarEmpresaAsync(Empresa nuevaEmpresa)
        {
            var query = @"
            INSERT INTO Empresa (Cuit, idEmpresaTipo, RazonSocial, idLocalida, Domicilio)
            VALUES (@Cuit, @idEmpresaTipo, @RazonSocial, @idLocalida, @Domicilio )";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, nuevaEmpresa);
            });
        }

        public Task EliminarEmpresaAsync(int idEmpresa)
        {
            return EliminarGenéricoAsync<Empresa>("Empresa", idEmpresa);
        }

        public Task ActualizarAsync(Empresa empresa)
        {
            return ActualizarGenéricoAsync("Empresa", empresa);
        }
    }
}