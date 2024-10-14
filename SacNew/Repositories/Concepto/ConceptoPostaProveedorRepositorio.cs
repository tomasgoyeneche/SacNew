using Dapper;
using SacNew.Services;

namespace SacNew.Repositories
{
    internal class ConceptoPostaProveedorRepositorio : BaseRepositorio, IConceptoPostaProveedorRepositorio
    {
        public ConceptoPostaProveedorRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService)
        { }

        public void AgregarConceptoPostaProveedor(int idConsumo, int idPosta, int idProveedor)
        {
            var query = @"
        INSERT INTO ConceptoPostaProveedor (IdConsumo, IdPosta, IdProveedor)
        VALUES (@IdConsumo, @IdPosta, @IdProveedor)";

            Conectar(connection =>
            {
                connection.Execute(query, new { IdConsumo = idConsumo, IdPosta = idPosta, IdProveedor = idProveedor });
                return 0;
            });
        }

        public void ActualizarConceptoPostaProveedor(int idConsumo, int idPosta, int idProveedor)
        {
            var query = @"
        UPDATE ConceptoPostaProveedor
        SET IdProveedor = @IdProveedor
        WHERE IdConsumo = @IdConsumo AND IdPosta = @IdPosta";

            Conectar(connection =>
            {
                connection.Execute(query, new { IdConsumo = idConsumo, IdPosta = idPosta, IdProveedor = idProveedor });
                return 0;
            });
        }
    }
}