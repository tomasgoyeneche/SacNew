using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;

namespace SacNew.Processor
{
    public class VolvoConnectProcessor : IVolvoConnectProcessor
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IExcelService _excelService;

        public VolvoConnectProcessor(
            IUnidadRepositorio unidadRepositorio,
            IExcelService excelService)
        {
            _unidadRepositorio = unidadRepositorio;
            _excelService = excelService;
        }

        public async Task<List<ImportVolvoConnect>> ImportarDesdeExcelAsync(string filePath, int idPeriodo)
        {
            return await _excelService.LeerExcelAsync(filePath, async (worksheet, row) =>
            {
                var patente = worksheet.Cells[row, 1].Text.Replace(" ", string.Empty); // Columna A
                var idUnidad = await ObtenerIdUnidadDesdePatenteAsync(patente);

                if (idUnidad == null)
                {
                    throw new Exception($"No se encontró una unidad para la patente: {patente}");
                }

                return new ImportVolvoConnect
                {
                    IdUnidad = idUnidad.Value,
                    Kilometros = decimal.Parse(worksheet.Cells[row, 4].Text), // Columna D
                    PromedioGasoilEnMarcha = decimal.Parse(worksheet.Cells[row, 6].Text), // Columna F
                    GasoilEnMarcha = decimal.Parse(worksheet.Cells[row, 7].Text), // Columna G
                    PromedioGasoilEnConduccion = decimal.Parse(worksheet.Cells[row, 9].Text), // Columna I
                    GasoilEnConduccion = decimal.Parse(worksheet.Cells[row, 8].Text), // Columna H
                    IdPeriodo = idPeriodo
                };
            }, "Datos del informe");  // Pasar el nombre de la hoja como parámetro
        }

        private async Task<int?> ObtenerIdUnidadDesdePatenteAsync(string patente)
        {
            var idTractor = await _unidadRepositorio.ObtenerIdTractorPorPatenteAsync(patente);
            return idTractor.HasValue
                ? await _unidadRepositorio.ObtenerIdUnidadPorTractorAsync(idTractor.Value)
                : null;
        }

        public async Task ExportarAExcelAsync(List<ImportVolvoConnect> datos, string filePath)
        {
            await _excelService.ExportarAExcelAsync(datos, filePath, "VolvoConnect");
        }
    }
}