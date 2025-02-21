using Core.Services;

namespace GestionOperativa.Processor
{
    internal class DocumentacionProcessor : IDocumentacionProcessor
    {
        private readonly ICsvService _csvService;

        public DocumentacionProcessor(ICsvService csvService)
        {
            _csvService = csvService;
        }

        public async Task VerificarArchivosFaltantesAsync<T>(
            Func<Task<List<T>>> obtenerRegistros,
            Func<T, string> generarRutaArchivo,
            string rutaCsv,
            Func<T, object> seleccionarDatos,
            Action<int> actualizarRelevamiento
        ) where T : class
        {
            var registros = await obtenerRegistros();
            var registrosFaltantes = new List<object>();

            foreach (var registro in registros)
            {
                string rutaArchivo = generarRutaArchivo(registro);
                if (!File.Exists(rutaArchivo))
                {
                    registrosFaltantes.Add(seleccionarDatos(registro)); // Filtrar qué datos queremos
                }
            }

            await _csvService.ExportarACsvAsync(registrosFaltantes, rutaCsv);

            actualizarRelevamiento(registros.Count);

            if (File.Exists(rutaCsv))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = rutaCsv,
                    UseShellExecute = true
                });
            }
        }
    }
}