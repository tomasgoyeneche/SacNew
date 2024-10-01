namespace SacNew.Repositories
{
    public interface IConceptoPostaProveedorRepositorio
    {
        void AgregarConceptoPostaProveedor(int idConsumo, int idPosta, int idProveedor);

        void ActualizarConceptoPostaProveedor(int idConsumo, int idPosta, int idProveedor);
    }
}