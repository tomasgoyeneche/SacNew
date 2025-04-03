using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class TractorRepositorio : BaseRepositorio, ITractorRepositorio
    {
        public TractorRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<TractorDto> ObtenerPorIdDtoAsync(int idTractor)
        {
            var query = "SELECT * FROM vw_TractoresDetalles WHERE idTractor = @IdTractor";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<TractorDto>(query, new { IdTractor = idTractor }));
        }

        public async Task<List<TractorDto>> ObtenerTodosLosTractoresDto()
        {
            var query = "SELECT * FROM vw_TractoresDetalles order by patente";

            return await ConectarAsync(async connection =>
            {
                var tractores = await connection.QueryAsync<TractorDto>(query);
                return tractores.ToList();
            });
        }

        public async Task EliminarTractorAsync(int idTractor)
        {
            var query = "Update Tractor set Activo = 0 WHERE idTractor = @idTractor";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { idTractor = idTractor });
            });
        }

        public async Task<List<TractorDto>> BuscarTractoresAsync(string textoBusqueda)
        {
            var query = "SELECT * FROM vw_TractoresDetalles WHERE Patente LIKE @TextoBusqueda OR Empresa_Nombre LIKE @TextoBusqueda order by patente";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<TractorDto>(query, new { TextoBusqueda = $"%{textoBusqueda}%" })
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task<Tractor?> ObtenerTractorPorIdAsync(int idTractor)
        {
            var query = @"
            SELECT *
            FROM Tractor
            WHERE IdTractor = @IdTractor";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<Tractor>(query, new { IdTractor = idTractor });
            });
        }

        public async Task ActualizarTractorAsync(Tractor tractor)
        {
            var query = @"
            UPDATE Tractor
            SET Patente = @Patente, Anio = @Anio, IdMarca = @IdMarca, IdModelo = @IdModelo,
                Tara = @Tara, Hp = @Hp, Combustible = @Combustible, Cmt = @Cmt,
                IdEmpresaSatelital = @IdEmpresaSatelital, FechaAlta = @FechaAlta, Configuracion = @Configuracion  
            WHERE IdTractor = @IdTractor";

            await ConectarAsync(async conn =>
            {
                await conn.ExecuteAsync(query, tractor);
            });
        }

        public async Task ActualizarEmpresaTractorAsync(int idTractor, int idEmpresa)
        {
            const string query = "UPDATE Tractor SET IdEmpresa = @idEmpresa WHERE IdTractor = @idTractor";

            await ConectarAsync(async connection =>
            {
                await connection.ExecuteAsync(query, new { idTractor, idEmpresa });
            });
        }
    }
}