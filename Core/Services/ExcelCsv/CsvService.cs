using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Text;

namespace Core.Services
{
    public class CsvService : ICsvService
    {
        // Exportar lista de objetos a CSV
        public async Task ExportarACsvAsync<T>(IEnumerable<T> datos, string filePath, bool incluirEncabezados = true)
        {
            if (EstaArchivoEnUso(filePath))
            {
                return;
            }

            using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(true))) // UTF-8 con BOM
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = incluirEncabezados,
                Delimiter = ";" // Asegurar delimitador correcto para Excel
            }))
            {
                await csv.WriteRecordsAsync(datos);
            }
        }

        public async Task ExportarFilasSeparadasAsync(IEnumerable<string[]> filas, string filePath)
        {
            if (EstaArchivoEnUso(filePath))
                return;

            using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
            {
                foreach (var fila in filas)
                {
                    string linea = string.Join(";", fila);
                    await writer.WriteLineAsync(linea);
                }
            }
        }

        private bool EstaArchivoEnUso(string filePath)
        {
            if (!File.Exists(filePath)) return false;

            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return false; // Si se puede abrir, el archivo no está en uso.
                }
            }
            catch (IOException)
            {
                return true; // Si hay una excepción, el archivo está en uso.
            }
        }

        // Leer datos de un archivo CSV y convertirlos a una lista de objetos
        public async Task<List<T>> LeerCsvAsync<T>(string filePath, bool tieneEncabezados = true)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = tieneEncabezados
            }))
            {
                return await csv.GetRecordsAsync<T>().ToListAsync();
            }
        }

        // Leer CSV con una función de mapeo personalizada
        public async Task<List<T>> LeerCsvConMapeoAsync<T, TMap>(string filePath) where TMap : ClassMap<T>
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<TMap>();
                return await csv.GetRecordsAsync<T>().ToListAsync();
            }
        }

        // Leer CSV línea por línea y procesar cada fila con una acción
        public async Task ProcesarCsvLineaPorLineaAsync(string filePath, Func<dynamic, Task> procesarLinea, bool tieneEncabezados = true)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = tieneEncabezados
            }))
            {
                await foreach (var record in csv.GetRecordsAsync<dynamic>())
                {
                    await procesarLinea(record);
                }
            }
        }
    }
}