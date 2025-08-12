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
        public async Task<TractorDto?> ObtenerPorIdDtoAsync(int idTractor)
        {
            var query = "SELECT * FROM vw_TractoresDetalles WHERE idTractor = @IdTractor";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<TractorDto?>(query, new { IdTractor = idTractor }));
        }

        public Task<Tractor?> ObtenerTractorPorIdAsync(int idTractor)
        {
            return ObtenerPorIdGenericoAsync<Tractor>("Tractor", "IdTractor", idTractor);
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

        public async Task<List<Tractor>> ObtenerTodosLosTractores()
        {
            var query = "SELECT * FROM Tractor order by patente";

            return await ConectarAsync(async connection =>
            {
                var tractores = await connection.QueryAsync<Tractor>(query);
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

        public Task AltaTractorAsync(string patente, int idUsuario)
        {
            var query = "EXEC sp_AltaTractor @patente, @idusuario";
            return ConectarAsync(connection =>
                connection.ExecuteAsync(query, new { patente, idusuario = idUsuario })
            );
        }


        public Task ActualizarTractorAsync(Tractor tractor)
        {
            return ActualizarGenéricoAsync("Tractor", tractor);
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

        public Task ActualizarVencimientoTractorAsync(int idTractor, int idVencimiento, DateTime fechaActualizacion, int idUsuario)
        {
            var query = @"
            UPDATE TractorVencimiento
            SET FechaVencimiento = @FechaVencimiento,
                IdUsuario = @idUsuario
            WHERE IdTractor = @idTractor AND IdVencimiento = @idVencimiento";

            var valoresNuevos = new
            {
                IdTractor = idTractor,
                IdVencimiento = idVencimiento,
                FechaVencimiento = fechaActualizacion,
                IdUsuario = idUsuario
            };

            return EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, valoresNuevos),
                "TractorVencimiento",
                "UPDATE",
                new { IdTractor = idTractor, IdVencimiento = idVencimiento },
                valoresNuevos
            );
        }
    }
}