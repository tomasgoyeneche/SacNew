namespace SacNew.Repositories
{
    public interface IConceptoPostaProveedorRepositorio
    {
        Task AgregarConceptoPostaProveedorAsync(int idConsumo, int idPosta, int idProveedor);

        Task ActualizarConceptoPostaProveedorAsync(int idConsumo, int idPosta, int idProveedor);
    }
}