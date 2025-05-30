using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IVaporizadoRepositorio
    {
        Task AgregarAsync(Vaporizado vaporizado, int idUsuario);
        Task EditarAsync(Vaporizado vaporizado, int idUsuario);

        Task<Vaporizado?> ObtenerPorNominaAsync(int idNomina);
        Task<Vaporizado?> ObtenerPorTeAsync(int idTe);
    }
}
