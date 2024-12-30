using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class ConceptoTipoRepositorio : BaseRepositorio, IConceptoTipoRepositorio
    {
        public ConceptoTipoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
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