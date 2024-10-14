using Dapper;
using SacNew.Models;
using SacNew.Models.DTOs;
using SacNew.Services;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    public class RepositorioPOC : BaseRepositorio, IRepositorioPOC
    {
        public RepositorioPOC(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService)
        { }

        public async Task<List<POCDto>> ObtenerTodosAsync()
        {
            var query = @"
        SELECT IdPoc, NumeroPOC, PatenteTractor, PatenteSemi, NombreFantasia, NombreChofer, ApellidoChofer
        FROM POC_NominaDetalle
        WHERE Activo = 1";

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
            var query = "UPDATE POC SET Activo = 0 WHERE IdPoc = @Id";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { Id = id });
            });
        }

        public async Task<POC?> ObtenerPorIdAsync(int idPoc)
        {
            var query = @"
        SELECT IdPoc, NumeroPOC, IdPosta, IdNomina, Odometro, Comentario, FechaCreacion, IdUsuario, Activo
        FROM POC
        WHERE IdPoc = @IdPoc AND Activo = 1";

            return await ConectarAsync(connection =>
            {
                return connection.QueryFirstOrDefaultAsync<POC>(query, new { IdPoc = idPoc });
            });
        }

        public async Task AgregarPOCAsync(POC poc)
        {
            var query = @"
        INSERT INTO POC (NumeroPOC, IdPosta, IdNomina, Odometro, Comentario, FechaCreacion, IdUsuario, Activo)
        VALUES (@NumeroPOC, @IdPosta, @IdNomina, @Odometro, @Comentario, @FechaCreacion, @IdUsuario, 1)";

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
            IdNomina = @IdNomina,
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