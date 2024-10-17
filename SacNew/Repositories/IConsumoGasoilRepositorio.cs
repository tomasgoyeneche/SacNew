using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Repositories
{
    public interface IConsumoGasoilRepositorio
    {
        Task<List<ConsumoGasoil>> ObtenerPorPOCAsync(int idPoc);
        Task AgregarConsumoAsync(ConsumoGasoil consumoGasoil);
        Task EliminarConsumoAsync(int idConsumoGasoil);
        Task<ConsumoGasoil> ObtenerPorIdAsync(int idConsumoGasoil);
    }
}
