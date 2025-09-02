using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace Core.Services
{
    public class ExcelService : IExcelService
    {
        public ExcelService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Configurar licencia
        }

        // Leer datos genéricos de Excel a una lista de objetos
        public async Task<List<T>> LeerExcelAsync<T>(
         string filePath,
         Func<ExcelWorksheet, int, Task<T>> mapFunc,
         string? sheetName = null)
        {
            var resultados = new List<T>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = SeleccionarHoja(package, sheetName);

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var item = await mapFunc(worksheet, row);
                    resultados.Add(item);
                }
            }

            return resultados;
        }

        // Exportar lista de objetos a Excel

        public async Task ExportarAExcelAsync<T>(
            IEnumerable<T> datos,
            string filePath,
            string nombreHoja = "Datos")
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(nombreHoja);
                EscribirEncabezados(worksheet, typeof(T));
                EscribirDatos(worksheet, datos);

                if (worksheet.Dimension != null)
                {
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                }

                await package.SaveAsAsync(new FileInfo(filePath));
            }
        }

        // Método privado para seleccionar la hoja de cálculo
        private ExcelWorksheet SeleccionarHoja(ExcelPackage package, string? sheetName)
        {
            var worksheet = !string.IsNullOrEmpty(sheetName)
                ? package.Workbook.Worksheets[sheetName]
                : package.Workbook.Worksheets[0];

            if (worksheet == null)
                throw new ArgumentException($"No se encontró la hoja '{sheetName ?? "primera hoja"}'.");

            return worksheet;
        }

        private void EscribirEncabezados(ExcelWorksheet worksheet, Type tipo)
        {
            var propiedades = tipo.GetProperties();
            if (propiedades.Length == 0)
                throw new Exception("El objeto no contiene propiedades para exportar.");

            for (int col = 0; col < propiedades.Length; col++)
            {
                worksheet.Cells[1, col + 1].Value = propiedades[col].Name;
            }

            var headerRange = worksheet.Cells[1, 1, 1, propiedades.Length];

            // 🔹 Estilo de encabezado
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Font.Size = 11;
            headerRange.Style.Font.Color.SetColor(Color.White);
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.DimGray); // gris oscuro
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            worksheet.Row(1).Height = 20;
        }

        // Método privado para escribir datos en el Excel
        private void EscribirDatos<T>(ExcelWorksheet worksheet, IEnumerable<T> datos)
        {
            int row = 2;
            var propiedades = typeof(T).GetProperties();

            foreach (var item in datos)
            {
                for (int col = 0; col < propiedades.Length; col++)
                {
                    var value = propiedades[col].GetValue(item);
                    var cell = worksheet.Cells[row, col + 1];

                    if (value is DateTime fecha)
                    {
                        if (fecha == DateTime.MinValue)
                        {
                            cell.Value = null;
                        }
                        else
                        {
                            cell.Value = fecha;

                            // 🔹 Si la hora es 00:00:00, mostrar solo fecha
                            if (fecha.TimeOfDay == TimeSpan.Zero)
                            {
                                cell.Style.Numberformat.Format = "dd/MM/yyyy";
                            }
                            else
                            {
                                // Mostrar fecha + hora y minutos
                                cell.Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
                            }
                        }
                    }
                    else
                    {
                        cell.Value = value;
                    }
                }
                row++;
            }
        }

    }
}