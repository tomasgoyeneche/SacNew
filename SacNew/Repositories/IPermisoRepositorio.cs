using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public interface IPermisoRepositorio
    {
        List<int> ObtenerPermisosPorUsuario(int idUsuario);

    }
}
