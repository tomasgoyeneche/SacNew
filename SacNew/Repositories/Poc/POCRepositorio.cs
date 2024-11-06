using Dapper;
using SacNew.Models;
using SacNew.Models.DTOs;
using SacNew.Services;

namespace SacNew.Repositories
{
    public class POCRepositorio : BaseRepositorio, IPOCRepositorio
    {
        public POCRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService)
        { }

        public async Task<List<POCDto>> ObtenerTodosAsync()
        {
            var query = @"
        SELECT IdPoc, NumeroPOC, PatenteTractor, PatenteSemi, NombreFantasia, NombreCompletoChofer, Estado
        FROM POC_UnidadDetalle
        WHERE Estado = 'Abierta'";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<POCDto>(query).ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task<List<POCDto>> BuscarPOCAsync(string criterio)
        {
            var query = @"
        SELECT IdPoc, NumeroPOC, PatenteTractor, PatenteSemi, NombreFantasia, NombreChofer, ApellidoChofer
        FROM POC_NominaDetalle
        WHERE Activo = 1
        AND (NumeroPOC LIKE @Criterio OR PatenteTractor LIKE @Criterio
        OR PatenteSemi LIKE @Criterio OR NombreFantasia LIKE @Criterio
        OR NombreChofer LIKE @Criterio OR ApellidoChofer LIKE @Criterio)";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<POCDto>(query, new { Criterio = "%" + criterio + "%" }).ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task EliminarPOCAsync(int id)
        {
            var query = "UPDATE POC SET Estado = 'cerrada' WHERE IdPoc = @Id";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { Id = id });
            });
        }

        public async Task<POC?> ObtenerPorIdAsync(int idPoc)
        {
            var query = @"
        SELECT IdPoc, NumeroPOC, IdPosta, idUnidad, idChofer, Odometro, Comentario, FechaCreacion, IdUsuario, idPeriodo, Estado
        FROM POC
        WHERE IdPoc = @IdPoc AND Estado = 'abierta'";

            return await ConectarAsync(connection =>
            {
                return connection.QueryFirstOrDefaultAsync<POC>(query, new { IdPoc = idPoc });
            });
        }

        public async Task AgregarPOCAsync(POC poc)
        {
            var query = @"
        INSERT INTO POC (NumeroPOC, IdPosta, idUnidad, idChofer, Odometro, Comentario, FechaCreacion, IdUsuario, idPeriodo, Estado)
        VALUES (@NumeroPOC, @IdPosta, @IdUnidad, @idChofer, @Odometro, @Comentario, @FechaCreacion, @IdUsuario, @idPeriodo, 'abierta')";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, poc);
            });
        }

        public async Task ActualizarPOCAsync(POC poc)
        {
            var query = @"
        UPDATE POC
        SET NumeroPOC = @NumeroPOC,
            IdPosta = @IdPosta,
            IdUnidad = @IdUnidad,
            IdChofer = @IdChofer,
            Odometro = @Odometro,
            Comentario = @Comentario,
            FechaCreacion = @FechaCreacion,
            IdUsuario = @IdUsuario
        WHERE IdPoc = @IdPoc";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, poc);
            });
        }
    }
}