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

        public string ObtenerDescripcionPorId(int idConsumoTipo)
        {
            var query = "SELECT Descripcion FROM ConceptoTipo WHERE IdConsumoTipo = @IdConsumoTipo";

            return Conectar(connection =>
            {
                var descripcion = connection.ExecuteScalar<string>(query, new { IdConsumoTipo = idConsumoTipo });
                return descripcion ?? "Tipo no encontrado";
            });
        }

        public List<ConceptoTipo> ObtenerTodosLosTipos()
        {
            var query = "SELECT * FROM ConceptoTipo";

            return Conectar(connection =>
            {
                var tipos = connection.Query<ConceptoTipo>(query).ToList();
                return tipos;
            });
        }
    }
}