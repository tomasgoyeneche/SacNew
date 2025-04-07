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

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns(); // Ajuste automático de columnas

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
            var headerRange = worksheet.Cells[1, 1, 1, propiedades.Length];

            for (int col = 0; col < propiedades.Length; col++)
            {
                worksheet.Cells[1, col + 1].Value = propiedades[col].Name;
            }

            headerRange.Style.Font.Bold = true;
            headerRange.Style.Font.Size = 12;
            headerRange.Style.Font.Color.SetColor(System.Drawing.Color.White);
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray); // 🔸 Fondo gris oscuro
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
                        cell.Value = fecha;
                        cell.Style.Numberformat.Format = "dd/MM/yyyy"; // 🔸 Formato de fecha
                    }
                    else
                    {
                        cell.Value = value;
                    }
                }

                // 🔸 Estilo alternado de fondo gris claro
                if (row % 2 == 0)
                {
                    worksheet.Cells[row, 1, row, propiedades.Length].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[row, 1, row, propiedades.Length].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                row++;
            }
        }
    }
}