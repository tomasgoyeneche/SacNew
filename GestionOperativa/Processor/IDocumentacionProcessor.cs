namespace GestionOperativa.Processor
{
    public interface IDocumentacionProcessor
    {
        Task VerificarArchivosFaltantesAsync<T>(
           Func<Task<IEnumerable<T>>> obtenerRegistros,
           Func<T, string?> generarRutaArchivo,
           string rutaCsv,
           Func<T, object> seleccionarDatos,
           Action<int> actualizarRelevamiento
       ) where T : class;

        Task ExportarVencimientosAsync<T>(
        Func<Task<IEnumerable<T>>> obtenerRegistros,
        string rutaCsv,
        Dictionary<string, Func<T, object>> camposBase, // 🔹 Datos principales como Nombre, CUIT, etc.
        Dictionary<string, Func<T, DateTime?>> camposDeVencimiento, // 🔹 Campos con fechas de vencimiento
        Action<int> actualizarRelevamiento);
    }
}