using SacNew.Models;
using SacNew.Models.DTOs;

namespace SacNew.Repositories
{
    public interface IRepositorioPOC
    {
        List<POCDto> ObtenerTodos();

        List<POCDto> BuscarPOC(string criterio);

        void EliminarPOC(int id);

        POC ObtenerPorId(int idPoc);

        void AgregarPOC(POC poc);

        void ActualizarPOC(POC poc);
    }
}