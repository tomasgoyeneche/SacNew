using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public interface IConceptoPostaProveedorRepositorio
    {
        void AgregarConceptoPostaProveedor(int idConsumo, int idPosta, int idProveedor);
        void ActualizarConceptoPostaProveedor(int idConsumo, int idPosta, int idProveedor);
    }
}
