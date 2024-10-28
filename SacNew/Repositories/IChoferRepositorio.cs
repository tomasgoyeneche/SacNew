using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public interface IChoferRepositorio
    {
        Task<int?> ObtenerIdPorDocumentoAsync(string documento);
    }
}
