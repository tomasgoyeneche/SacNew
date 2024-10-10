namespace SacNew.Services
{
    public interface IAuditoriaService
    {
        Task RegistrarAuditoriaAsync(string tablaModificada, string accion, int registroModificadoId, string valoresAnteriores, string valoresNuevos);
    }
}