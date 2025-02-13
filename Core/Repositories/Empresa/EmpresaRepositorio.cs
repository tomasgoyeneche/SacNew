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

        public async Task<EmpresaDto?> ObtenerPorIdDto(int idEmpresa)
        {
            var query = "SELECT * FROM vw_EmpresaDetalle where idEmpresa = @idEmpresa";

            return await ConectarAsync(connection =>
            {
                return connection.QueryFirstOrDefaultAsync<EmpresaDto>(query, new { idEmpresa = idEmpresa });
            });
        }


        public async Task<Empresa?> ObtenerPorIdAsync(int idEmpresa)
        {
            var query = "SELECT * FROM Empresa where idEmpresa = @idEmpresa";

            return await ConectarAsync(connection =>
            {
                return connection.QueryFirstOrDefaultAsync<Empresa>(query, new { idEmpresa = idEmpresa });
            });
        }

        public async Task<List<Empresa>> BuscarEmpresasAsync(string textoBusqueda)
        {
            var query = "SELECT * FROM vw_EmpresaDetalle WHERE Cuit LIKE @TextoBusqueda OR NombreFantasia LIKE @TextoBusqueda";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<Empresa>(query, new { TextoBusqueda = $"%{textoBusqueda}%" })
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task AgregarEmpresaAsync(Empresa nuevaEmpresa)
        {
            var query = @"
            INSERT INTO Empresa (Codigo, Descripcion, Direccion, idProvincia)
            VALUES (@Codigo, @Descripcion, @Direccion, @idProvincia)";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, nuevaEmpresa);
            });
        }

        public async Task EliminarEmpresaAsync(int idEmpresa)
        {
            var query = "Update Empresa set Activo = 0 WHERE IdEmpresa = @IdEmpresa";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { IdEmpresa = idEmpresa });
            });
        }

        public async Task ActualizarAsync(Empresa empresa)
        {
            var empresaAnterior = await ObtenerPorIdAsync(empresa.IdEmpresa);

            var query = @"
                UPDATE Empresa
                SET Cuit = @Cuit,
                    IdEmpresaTipo = @IdEmpresaTipo,
                    RazonSocial = @RazonSocial,
                    NombreFantasia = @NombreFantasia,
                    IdLocalidad = @IdLocalidad,
                    Domicilio = @Domicilio,
                    Telefono = @Telefono,
                    Email = @Email
                WHERE IdEmpresa = @IdEmpresa";

            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, empresa),
                "Empresa",
                "UPDATE",
                empresaAnterior,
                empresa
            );
        }
    }
}