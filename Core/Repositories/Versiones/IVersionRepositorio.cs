using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IVersionRepositorio
    {
        Task<Versiones?> ObtenerVersionActivaAsync();
        Task<Versiones?> ObtenerPorNumeroAsync(string numeroVersion);
        Task InsertarVersionAsync(Versiones version);
        Task ActualizarVersionAsync(Versiones version);
    }
}
