using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public interface IConceptoProveedorRepositorio
    {
        List<Proveedor> ObtenerTodosLosProveedores();

    }
}
