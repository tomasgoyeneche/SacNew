using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public interface ILocacionProductoRepositorio
    {
        Task<List<LocacionProducto>> ObtenerPorLocacionIdAsync(int idLocacion);
        Task AgregarAsync(LocacionProducto locacionProducto);
        Task EliminarAsync(int idLocacionProducto);
    }
}
