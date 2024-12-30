using Core.Repositories;
using Core.Services;
using Shared.Models;

namespace GestionFlota.Processor
{
    public class ImportacionMercadoVictoriaProcessor : IImportacionMercadoVictoriaProcessor
    {
        private readonly IExcelService _excelService;
        private readonly IUnidadRepositorio _unidadRepositorio;

        public ImportacionMercadoVictoriaProcessor(
            IExcelService excelService,
            IUnidadRepositorio unidadRepositorio)
        {
            _excelService = excelService;
            _unidadRepositorio = unidadRepositorio;
        }

        public async Task<List<ImportMercadoVictoria>> ImportarDesdeExcelAsync(string filePath, int idPeriodo)
        {
            return await _excelService.LeerExcelAsync(filePath, async (worksheet, row) =>
            {
                var fecha = DateTime.Parse(worksheet.Cells[row, 1].Text); // Columna A
                var litros = decimal.Parse(worksheet.Cells[row, 3].Text); // Columna C
                var patente = worksheet.Cells[row, 5].Text.Replace(" ", string.Empty); // Columna E, limpiar espacios

                var nombreConsumo = worksheet.Cells[row, 14].Text; // Columna N para el nombre del consumo
                var idConsumo = ObtenerIdConsumoDesdeNombre(nombreConsumo);

                var idUnidad = await ObtenerIdUnidadDesdePatenteAsync(patente);
                if (idUnidad == null)
                {
                    throw new Exception($"No se encontró una unidad para la patente: {patente}");
                }

                return new ImportMercadoVictoria
                {
                    Fecha = fecha,
                    Litros = litros,
                    IdUnidad = idUnidad.Value,
                    IdConsumo = idConsumo,
                    IdPeriodo = idPeriodo
                };
            });
        }

        private int ObtenerIdConsumoDesdeNombre(string nombreConsumo)
        {
            if (nombreConsumo.StartsWith("CH-EURO"))
            {
                return 3; // ID para CH-EURO
            }
            else if (nombreConsumo.StartsWith("CH-GO500"))
            {
                return 4; // ID para CH-GO500
            }
            throw new ArgumentException($"No se reconoce el tipo de consumo: {nombreConsumo}");
        }

        public async Task ExportarConsumosAExcelAsync(List<ImportMercadoVictoria> consumos, string filePath)
        {
            await _excelService.ExportarAExcelAsync(consumos, filePath, "Importación Mercado Victoria");
        }

        private async Task<int?> ObtenerIdUnidadDesdePatenteAsync(string patente)
        {
            var idTractor = await _unidadRepositorio.ObtenerIdTractorPorPatenteAsync(patente);
            return idTractor.HasValue
                ? await _unidadRepositorio.ObtenerIdUnidadPorTractorAsync(idTractor.Value)
                : null;
        }
    }
}