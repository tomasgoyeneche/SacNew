using Dapper;
using SacNew.Services;

namespace SacNew.Repositories
{
    internal class ConceptoPostaProveedorRepositorio : BaseRepositorio, IConceptoPostaProveedorRepositorio
    {
        public ConceptoPostaProveedorRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService)
        { }

        public async Task AgregarConceptoPostaProveedorAsync(int idConsumo, int idPosta, int idProveedor)
        {
            var query = @"
        INSERT INTO ConceptoPostaProveedor (IdConsumo, IdPosta, IdProveedor)
        VALUES (@IdConsumo, @IdPosta, @IdProveedor)";

            await ConectarAsync(async connection =>
            {
                await connection.ExecuteAsync(query, new { IdConsumo = idConsumo, IdPosta = idPosta, IdProveedor = idProveedor });
            });
        }

        public async Task ActualizarConceptoPostaProveedorAsync(int idConsumo, int idPosta, int idProveedor)
        {
            var query = @"
        UPDATE ConceptoPostaProveedor
        SET IdProveedor = @IdProveedor
        WHERE IdConsumo = @IdConsumo AND IdPosta = @IdPosta";

            await ConectarAsync(async connection =>
            {
                await connection.ExecuteAsync(query, new { IdConsumo = idConsumo, IdPosta = idPosta, IdProveedor = idProveedor });
            });
        }
    }
}