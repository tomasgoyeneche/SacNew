using SacNew.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public interface IRepositorioPOC
    {
        List<POCDto> ObtenerTodos();
        List<POCDto> BuscarPOC(string criterio);
        void EliminarPOC(int id);
    }
}
