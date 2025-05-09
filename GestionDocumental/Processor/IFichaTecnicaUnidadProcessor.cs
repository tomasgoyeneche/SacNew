using Shared.Models.DTOs;

namespace GestionOperativa.Processor
{
    public interface IFichaTecnicaUnidadProcessor
    {
        Task<FichaTecnicaUnidadDto?> ObtenerFichaTecnicaAsync(int idUnidad);
    }
}