namespace GestionOperativa.Processor
{
    public interface IDocumentacionProcessor
    {
        Task VerificarArchivosFaltantesAsync<T>(
            Func<Task<List<T>>> obtenerRegistros,
            Func<T, string> generarRutaArchivo,
            string rutaCsv,
            Func<T, object> seleccionarDatos,
            Action<int> actualizarRelevamiento
        ) where T : class;
    }
}