using Core.Services;
using System.Collections.Concurrent;
using System.Dynamic;

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
            Func<Task<IEnumerable<T>>> obtenerRegistros,
            Func<T, string?> generarRutaArchivo,
            string rutaCsv,
            Func<T, object> seleccionarDatos,
            Action<int> actualizarRelevamiento
        ) where T : class
        {
            var registros = await obtenerRegistros();
            var registrosFaltantes = new ConcurrentBag<object>();

            await Task.Run(() =>
            {
                Parallel.ForEach(registros, registro =>
                {
                    string? rutaArchivo = generarRutaArchivo(registro);
                    if (!string.IsNullOrEmpty(rutaArchivo) && !File.Exists(rutaArchivo))
                    {
                        registrosFaltantes.Add(seleccionarDatos(registro));
                    }
                });
            });

            await _csvService.ExportarACsvAsync(registrosFaltantes, rutaCsv);

            actualizarRelevamiento(registros.Count());

            if (File.Exists(rutaCsv))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = rutaCsv,
                    UseShellExecute = true
                });
            }
        }

        public async Task ExportarVencimientosAsync<T>(
    Func<Task<IEnumerable<T>>> obtenerRegistros,
    string rutaCsv,
    Dictionary<string, Func<T, object>> camposBase, // 🔹 Datos principales como Nombre, CUIT, etc.
    Dictionary<string, Func<T, DateTime?>> camposDeVencimiento, // 🔹 Campos con fechas de vencimiento
    Action<int> actualizarRelevamiento)
        {
            var registros = await obtenerRegistros();
            var registrosVencimientos = new List<dynamic>(); // 🔹 CsvHelper sí puede manejarlo

            await Task.Run(() =>
            {
                Parallel.ForEach(registros, registro =>
                {
                    var datos = new ExpandoObject() as IDictionary<string, object>; // 🔹 Clase anónima para CSV

                    // 📌 Extraer datos base (Nombre, CUIT, Documento, etc.)
                    foreach (var campo in camposBase)
                    {
                        datos[campo.Key] = campo.Value(registro) ?? "Desconocido";
                    }

                    // 📌 Evaluar vencimientos
                    foreach (var campo in camposDeVencimiento)
                    {
                        DateTime? fechaVencimiento = campo.Value(registro);
                        datos[$"{campo.Key}Fecha"] = fechaVencimiento?.ToString("yyyy-MM-dd") ?? "Faltante";
                        datos[$"{campo.Key}Estado"] = fechaVencimiento == null ? "Faltante" :
                                                      fechaVencimiento >= DateTime.Today ? "Vigente" : "Vencido";
                    }

                    registrosVencimientos.Add(datos);
                });
            });

            await _csvService.ExportarACsvAsync(registrosVencimientos, rutaCsv);
            actualizarRelevamiento(registros.Count());

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