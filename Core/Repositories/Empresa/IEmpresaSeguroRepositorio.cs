using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IEmpresaSeguroRepositorio
    {
        Task<EmpresaSeguro?> ObtenerPorEmpresaAsync(int idEmpresa);
        Task ActualizarAsync(EmpresaSeguro seguro);
    }
}
