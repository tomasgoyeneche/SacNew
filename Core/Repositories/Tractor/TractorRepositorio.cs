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

        // Obtener Tractor Por Id
        public async Task<TractorDto> ObtenerPorIdDtoAsync(int idTractor)
        {
            var query = "SELECT * FROM vw_TractoresDetalles WHERE idTractor = @IdTractor";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<TractorDto>(query, new { IdTractor = idTractor }));
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

        public async Task<List<TractorDto>> ObtenerTodosLosTractoresDto()
        {
            var query = "SELECT * FROM vw_TractoresDetalles order by patente";

            return await ConectarAsync(async connection =>
            {
                var tractores = await connection.QueryAsync<TractorDto>(query);
                return tractores.ToList();
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


        // Otras Busquedas

        public async Task<List<Tractor>> ObtenerTractoresLibresAsync()
        {
            const string sql = @"
        SELECT t.IdTractor, t.Patente, t.Tara
        FROM Tractor t
        WHERE t.IdTractor NOT IN (SELECT IdTractor FROM Unidad WHERE Activo = 1) and t.Activo = 1";

            return await ConectarAsync(async connection =>
            {
                var resultado = await connection.QueryAsync<Tractor>(sql);
                return resultado.ToList();
            });
        }








        // Actualizar, Editar, Eliminar

        public async Task AltaTractorAsync(string patente, int idUsuario)
        {
            var query = "EXEC sp_AltaTractor @patente, @idusuario"; // o ";" si querés como texto

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(
                    query,
                    new { patente = patente, idusuario = idUsuario }
                );
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

        public async Task EliminarTractorAsync(int idTractor)
        {
            var query = "EXEC sp_DarDeBajaTractor @idTractor;";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { idTractor = idTractor });
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

        public async Task ActualizarVencimientoTractorAsync(int idTractor, int idVencimiento, DateTime fechaActualizacion, int idUsuario)
        {
            var query = @"
        UPDATE TractorVencimiento
        SET FechaVencimiento = @fechaActualizacion,
            IdUsuario = @idUsuario
        WHERE IdTractor = @idTractor AND IdVencimiento = @idVencimiento";

            await ConectarAsync(async connection =>
            {
                await connection.ExecuteAsync(query, new
                {
                    idTractor,
                    idVencimiento,
                    fechaActualizacion,
                    idUsuario
                });
            });
        }
    }
}