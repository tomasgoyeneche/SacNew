using Dapper;
using SacNew.Models;
using SacNew.Services;
using static SacNew.Services.Startup;

namespace SacNew.Repositories
{
    internal class ConceptoRepositorio : BaseRepositorio, IConceptoRepositorio
    {
        public ConceptoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
        { }

        public async Task<List<Concepto>> ObtenerTodosLosConceptosAsync()
        {
            var query = "SELECT * FROM Concepto WHERE Activo = 1";
            return await ConectarAsync(async connection =>
            {
                var conceptos = await connection.QueryAsync<Concepto>(query);
                return conceptos.ToList();
            });
        }

        public async Task<List<Concepto>> ObtenerPorTipoAsync(int idTipoConsumo)
        {
            var query = @"
        SELECT *
        FROM Concepto
        WHERE IdConsumoTipo = @IdTipoConsumo AND Activo = 1";

            return await ConectarAsync(async connection =>
            {
                var conceptos = await connection.QueryAsync<Concepto>(query, new { IdTipoConsumo = idTipoConsumo });
                return conceptos.ToList();
            });
        }

        public async Task<Concepto?> ObtenerPorIdAsync(int idConsumo)
        {
            var query = "SELECT * FROM Concepto WHERE IdConsumo = @IdConsumo";
            return await ConectarAsync(async connection =>
            {
                return await connection.QueryFirstOrDefaultAsync<Concepto>(query, new { IdConsumo = idConsumo });
            });
        }

        public async Task<List<Concepto>> BuscarConceptosAsync(string textoBusqueda)
        {
            var query = "SELECT * FROM Concepto WHERE Codigo LIKE @Busqueda OR Descripcion LIKE @Busqueda";
            return await ConectarAsync(async connection =>
            {
                var conceptos = await connection.QueryAsync<Concepto>(query, new { Busqueda = $"%{textoBusqueda}%" });
                return conceptos.ToList();
            });
        }

        public async Task AgregarConceptoAsync(Concepto concepto)
        {
            var query = @"
        INSERT INTO Concepto (Codigo, Descripcion, idConsumoTipo, PrecioActual, Vigencia, PrecioAnterior, Activo, IdUsuario, FechaModificacion)
        VALUES (@Codigo, @Descripcion, @IdConsumoTipo, @PrecioActual, @Vigencia, @PrecioAnterior, @Activo, @IdUsuario, @FechaModificacion)";

            await ConectarAsync(async connection =>
            {
                await connection.ExecuteAsync(query, concepto);
            });
        }

        public async Task ActualizarConceptoAsync(Concepto concepto)
        {
            var query = @"
        UPDATE Concepto
        SET Codigo = @Codigo, Descripcion = @Descripcion, idConsumoTipo = @idConsumoTipo, PrecioActual = @PrecioActual,
        Vigencia = @Vigencia, PrecioAnterior = @PrecioAnterior, Activo = @Activo, IdUsuario = @IdUsuario, FechaModificacion = @FechaModificacion
        WHERE IdConsumo = @IdConsumo";

            await ConectarAsync(async connection =>
            {
                await connection.ExecuteAsync(query, concepto);
            });
        }

        public async Task EliminarConceptoAsync(int idConsumo)
        {
            var query = "UPDATE Concepto SET Activo = 0 WHERE IdConsumo = @IdConsumo";
            await ConectarAsync(async connection =>
            {
                await connection.ExecuteAsync(query, new { IdConsumo = idConsumo });
            });
        }
    }
}