using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class ChoferRepositorio : BaseRepositorio, IChoferRepositorio
    {
        public ChoferRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        // METODOS DE BUSQUEDA POR ID O GENERAL

        public Task<Chofer?> ObtenerPorIdAsync(int idChofer)
        {
            return ObtenerPorIdGenericoAsync<Chofer>("Chofer", "IdChofer", idChofer);
        }


        public async Task<ChoferDto?> ObtenerPorIdDtoAsync(int idChofer)
        {
            var query = "SELECT * FROM vw_ChoferesDetalles WHERE idChofer = @IdChofer";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<ChoferDto>(query, new { IdChofer = idChofer }));
        }

        public async Task<List<Chofer?>> ObtenerTodosLosChoferes()
        {
            var query = "SELECT * FROM Chofer WHERE Activo = 1";

            return await ConectarAsync(async connection =>
            {
                var chofers = await connection.QueryAsync<Chofer?>(query);
                return chofers.ToList();
            });
        }

        public async Task<List<ChoferesLibresDto?>> ObtenerTodosLosChoferesLibres()
        {
            var query = "SELECT * FROM vw_ChoferesLibres";

            return await ConectarAsync(async connection =>
            {
                var chofers = await connection.QueryAsync<ChoferesLibresDto?>(query);
                return chofers.ToList();
            });
        }

        public async Task<List<Chofer?>> ObtenerTodosLosChoferesPorEmpresa(int idEmpresa)
        {
            var query = "SELECT * FROM Chofer WHERE Activo = 1 and IdEmpresa = @IdEmpresa";

            return await ConectarAsync(async connection =>
            {
                var chofers = await connection.QueryAsync<Chofer?>(query, new { idEmpresa = idEmpresa });
                return chofers.ToList();
            });
        }

        public async Task<List<ChoferDto?>> ObtenerTodosLosChoferesDto()
        {
            var query = "SELECT * FROM vw_ChoferesDetalles";

            return await ConectarAsync(async connection =>
            {
                var chofers = await connection.QueryAsync<ChoferDto?>(query);
                return chofers.ToList();
            });
        }

        // METODOS DE BUSQUEDA ESPECIFICOS

        public async Task<List<ChoferDto>> BuscarAsync(string textoBusqueda)
        {
            var query = "SELECT * FROM Chofer WHERE activo = 1 and (Nombre LIKE @TextoBusqueda OR Apellido LIKE @TextoBusqueda OR Documento LIKE @TextoBusqueda)";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<ChoferDto>(query, new { TextoBusqueda = $"%{textoBusqueda}%" })
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task<int?> ObtenerIdPorDocumentoAsync(string documento)
        {
            var query = "SELECT IdChofer FROM Chofer WHERE Documento = @Documento";

            return await ConectarAsync(async conn =>
                await conn.QuerySingleOrDefaultAsync<int?>(query, new { Documento = documento }));
        }

        // Metodos de ABM
        public async Task EliminarChoferAsync(int idChofer)
        {
            var query = "EXEC sp_DarDeBajaChofer @IdChofer;";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { IdChofer = idChofer });
            });
        }

        public Task ActualizarAsync(Chofer chofer)
        {
            return ActualizarGenéricoAsync("Chofer", chofer);
        }


        public async Task ActualizarEmpresaChoferAsync(int idChofer, int idEmpresa)
        {
            const string query = "UPDATE Chofer SET IdEmpresa = @idEmpresa WHERE IdChofer = @idChofer";

            await ConectarAsync(async connection =>
            {
                await connection.ExecuteAsync(query, new { idChofer, idEmpresa });
            });
        }

        public async Task AltaChoferAsync(string nombre, string apellido, string documento, int idUsuario)
        {
            var query = "EXEC sp_AltaChofer @nombre, @apellido, @documento, @idusuario"; // o ";" si querés como texto

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(
                    query,
                    new { nombre = nombre, apellido = apellido, documento = documento, idusuario = idUsuario }
                );
            });
        }

        public Task ActualizarVencimientoChoferAsync(int idChofer, int idVencimiento, DateTime fechaActualizacion, int idUsuario)
        {
            var query = @"
            UPDATE ChoferVencimiento
            SET FechaVencimiento = @FechaVencimiento,
                IdUsuario = @idUsuario
            WHERE IdChofer = @idChofer AND IdVencimiento = @idVencimiento";

            var valoresNuevos = new
            {
                IdChofer = idChofer,
                IdVencimiento = idVencimiento,
                FechaVencimiento = fechaActualizacion,
                IdUsuario = idUsuario
            };

            // Usamos EjecutarConAuditoriaAsync para que haga el fetch de valores anteriores, ejecute el update y registre la auditoría
            return EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, valoresNuevos),
                "ChoferVencimiento",
                "UPDATE",
                new { IdChofer = idChofer, IdVencimiento = idVencimiento },
                valoresNuevos
            );
        }
    }
}