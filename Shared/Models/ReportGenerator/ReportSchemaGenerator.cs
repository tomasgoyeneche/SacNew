using System.Text;
using System.Xml.Serialization;

namespace Shared.Models
{
    public class ReportSchemaGenerator
    {
        public static void GenerarEsquemaReportes()
        {
            // Define los modelos utilizados en el reporte
            var types = new[]
            {
                typeof(NominaMetanolActivaDto),
                //typeof(RcImputacionDetalleDto),
                //typeof(RcDescripcion)
            };

            var xri = new XmlReflectionImporter();
            var xss = new XmlSchemas();
            var xse = new XmlSchemaExporter(xss);

            foreach (var type in types)
            {
                var xtm = xri.ImportTypeMapping(type);
                xse.ExportTypeMapping(xtm);
            }

            // Define la carpeta de salida relativa a la solución
            var solutionRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            if (string.IsNullOrEmpty(solutionRoot))
            {
                throw new DirectoryNotFoundException("No se pudo determinar la ruta raíz de la solución.");
            }

            var outputPath = Path.Combine(solutionRoot, "Shared", "Models", "ReportModels");
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            var schemaFile = Path.Combine(outputPath, "ReportItemNominaMetanol.xsd");

            // Escribe el archivo XSD
            using var sw = new StreamWriter(schemaFile, false, Encoding.UTF8);
            for (int i = 0; i < xss.Count; i++)
            {
                var xs = xss[i];
                xs.Id = "ReportItemSchemas";
                xs.Write(sw);
            }

            Console.WriteLine($"Esquema generado en: {schemaFile}");
        }
    }
}