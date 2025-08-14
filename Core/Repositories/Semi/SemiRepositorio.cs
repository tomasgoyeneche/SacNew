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

        // Obtener Semi Por Id

        public async Task<List<SemiDto>> ObtenerTodosLosSemisDto()
        {
            var query = "SELECT * FROM vw_SemiremolquesDetalles order by patente";

            return await ConectarAsync(async connection =>
            {
                var tractores = await connection.QueryAsync<SemiDto>(query);
                return tractores.ToList();
            });
        }

        public async Task<List<Shared.Models.Semi>> ObtenerTodosLosSemis()
        {
            var query = "SELECT * FROM Semi order by patente";

            return await ConectarAsync(async connection =>
            {
                var tractores = await connection.QueryAsync<Shared.Models.Semi>(query);
                return tractores.ToList();
            });
        }

        public async Task<SemiDto?> ObtenerPorIdDtoAsync(int idSemi)
        {
            var query = "SELECT * FROM vw_SemiremolquesDetalles WHERE idSemi = @idSemi";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<SemiDto?>(query, new { IdSemi = idSemi }));
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

        public Task<Shared.Models.Semi?> ObtenerSemiPorIdAsync(int idSemi)
        {
            return ObtenerPorIdGenericoAsync<Shared.Models.Semi>("Semi", "IdSemi", idSemi);
        }

        // Obtener por otras busquedas

        public async Task<List<Shared.Models.Semi>> ObtenerSemisLibresAsync()
        {
            const string sql = @"
        SELECT s.IdSemi, s.Patente, s.Tara
        FROM Semi s
        WHERE s.IdSemi NOT IN (SELECT IdSemi FROM Unidad WHERE Activo = 1) and s.Activo = 1";

            return await ConectarAsync(async connection =>
            {
                var resultado = await connection.QueryAsync<Shared.Models.Semi>(sql);
                return resultado.ToList();
            });
        }

        // Actualizar, Editar, Eliminar

        public async Task AltaSemiAsync(string patente, int idUsuario)
        {
            var query = "EXEC sp_AltaSemi @patente, @idusuario"; // o ";" si querés como texto

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(
                    query,
                    new { patente = patente, idusuario = idUsuario }
                );
            });
        }

        public async Task EliminarSemiAsync(int idSemi)
        {
            var query = "EXEC sp_DarDeBajaSemi @idsemi";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { IdSemi = idSemi });
            });
        }

        public Task ActualizarSemiAsync(Shared.Models.Semi semi)
        {
            return ActualizarGenéricoAsync("Semi", semi);
        }

        public async Task ActualizarEmpresaSemiAsync(int idSemi, int idEmpresa)
        {
            const string query = "UPDATE Semi SET IdEmpresa = @idEmpresa WHERE IdSemi = @idSemi";

            await ConectarAsync(async connection =>
            {
                await connection.ExecuteAsync(query, new { idSemi, idEmpresa });
            });
        }

        public Task ActualizarVencimientoSemiAsync(int idSemi, int idVencimiento, DateTime fechaActualizacion, int idUsuario)
        {
            var query = @"
            UPDATE SemiCisternaVencimiento
            SET FechaVencimiento = @FechaVencimiento,
                IdUsuario = @idUsuario
            WHERE IdSemi = @idSemi AND IdVencimiento = @idVencimiento";

            var valoresNuevos = new
            {
                IdSemi = idSemi,
                IdVencimiento = idVencimiento,
                FechaVencimiento = fechaActualizacion,
                IdUsuario = idUsuario
            };

            return EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, valoresNuevos),
                "SemiCisternaVencimiento",
                "UPDATE",
                new { IdSemi = idSemi, IdVencimiento = idVencimiento },
                valoresNuevos
            );
        }
    }
}