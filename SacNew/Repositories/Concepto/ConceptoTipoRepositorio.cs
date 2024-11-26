using Dapper;
using SacNew.Models;
using SacNew.Services;

namespace SacNew.Repositories
{
    internal class ConceptoTipoRepositorio : BaseRepositorio, IConceptoTipoRepositorio
    {
        public ConceptoTipoRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService)
        { }

        public async Task<string> ObtenerDescripcionPorIdAsync(int idConsumoTipo)
        {
            var query = "SELECT Descripcion FROM ConceptoTipo WHERE IdConsumoTipo = @IdConsumoTipo";

            return await ConectarAsync(async connection =>
            {
                var descripcion = await connection.ExecuteScalarAsync<string>(query, new { IdConsumoTipo = idConsumoTipo });
                return descripcion ?? "Tipo no encontrado";
            });
        }

        public async Task<List<ConceptoTipo>> ObtenerTodosLosTiposAsync()
        {
            var query = "SELECT * FROM ConceptoTipo";

            return await ConectarAsync(async connection =>
            {
                var tipos = await connection.QueryAsync<ConceptoTipo>(query);
                return tipos.ToList(); // Convertimos el IEnumerable a List
            });
        }
    }
}