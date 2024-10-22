using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Services
{
    public class ExcelService
    {
        public ExcelService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Configurar licencia
        }

        // Leer datos genéricos de Excel a una lista de objetos
        public async Task<List<T>> LeerExcelAsync<T>(string filePath, Func<ExcelWorksheet, int, T> mapFunc)
        {
            var resultados = new List<T>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var item = mapFunc(worksheet, row);
                    resultados.Add(item);
                }
            }

            return await Task.FromResult(resultados);
        }

        // Exportar lista de objetos a Excel
        public async Task ExportarAExcelAsync<T>(IEnumerable<T> datos, string filePath, string nombreHoja = "Datos")
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(nombreHoja);

                var propiedades = typeof(T).GetProperties();
                for (int col = 0; col < propiedades.Length; col++)
                {
                    worksheet.Cells[1, col + 1].Value = propiedades[col].Name; // Encabezados
                }

                int row = 2;
                foreach (var item in datos)
                {
                    for (int col = 0; col < propiedades.Length; col++)
                    {
                        worksheet.Cells[row, col + 1].Value = propiedades[col].GetValue(item);
                    }
                    row++;
                }

                await package.SaveAsAsync(new FileInfo(filePath));
            }
        }
    }
}
