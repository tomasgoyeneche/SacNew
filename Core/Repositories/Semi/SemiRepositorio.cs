using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class SemiRepositorio : BaseRepositorio, ISemiRepositorio
    {
        public SemiRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
           : base(connectionStrings, sesionService) { }

        public async Task<SemiDto> ObtenerPorIdDtoAsync(int idSemi)
        {
            var query = "SELECT * FROM vw_SemiremolquesDetalles WHERE idSemi = @idSemi";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<SemiDto>(query, new { IdSemi = idSemi }));
        }

        public async Task<List<SemiDto>> ObtenerTodosLosSemisDto()
        {
            var query = "SELECT * FROM vw_SemiremolquesDetalles order by patente";

            return await ConectarAsync(async connection =>
            {
                var tractores = await connection.QueryAsync<SemiDto>(query);
                return tractores.ToList();
            });
        }

        public async Task EliminarSemiAsync(int idSemi)
        {
            var query = "Update Semi set Activo = 0 WHERE idSemi = @idSemi";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { IdSemi = idSemi });
            });
        }

        public async Task<List<SemiDto>> BuscarSemisAsync(string textoBusqueda)
        {
            var query = "SELECT * FROM vw_SemiremolquesDetalles WHERE Patente LIKE @TextoBusqueda OR Empresa_Nombre LIKE @TextoBusqueda order by patente";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<SemiDto>(query, new { TextoBusqueda = $"%{textoBusqueda}%" })
                                 .ContinueWith(task => task.Result.ToList());
            });
        }
    }
}