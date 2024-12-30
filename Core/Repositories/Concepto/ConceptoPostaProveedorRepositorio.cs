using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class ConceptoPostaProveedorRepositorio : BaseRepositorio, IConceptoPostaProveedorRepositorio
    {
        public ConceptoPostaProveedorRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService)
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