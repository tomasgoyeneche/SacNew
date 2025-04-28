using Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Processor
{
    public interface IFichaTecnicaUnidadProcessor
    {
        Task<FichaTecnicaUnidadDto?> ObtenerFichaTecnicaAsync(int idUnidad);
    }
}
