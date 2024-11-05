using OfficeOpenXml;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Repositories.Chofer;
using SacNew.Services;

namespace SacNew.Processor
{
    public class ImportacionYpfProcessor : IImportacionYpfProcessor
    {
        private readonly IExcelService _excelService;

        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;

        public ImportacionYpfProcessor(
            IExcelService excelService,
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio)
        {
            _excelService = excelService ?? throw new ArgumentNullException(nameof(excelService));
            _choferRepositorio = choferRepositorio;
            _unidadRepositorio = unidadRepositorio;
        }

        public async Task<List<ImportConsumoYpfEnRuta>> ImportarConsumosDesdeExcelAsync(string filePath, int idPeriodo)
        {
            return await _excelService.LeerExcelAsync(filePath, async (worksheet, row) =>
            {
                var documentoChofer = worksheet.Cells[row, 12].Text;
                var patente = worksheet.Cells[row, 14].Text.Replace(" ", string.Empty); // Limpiar espacios

                var idChofer = await _choferRepositorio.ObtenerIdPorDocumentoAsync(documentoChofer);
                var idUnidad = await ObtenerIdUnidadDesdePatenteAsync(patente);

                var nombreConsumo = worksheet.Cells[row, 19].Text;
                var idConsumo = MapearNombreConsumo(nombreConsumo);

                var consumo = new ImportConsumoYpfEnRuta
                {
                    FechaHora = DateTime.Parse(worksheet.Cells[row, 1].Text),
                    Localidad = worksheet.Cells[row, 7].Text,
                    Tarjeta = worksheet.Cells[row, 9].Text,
                    IdChofer = idChofer ?? 73, // Asignar 0 si no se encuentra el chofer
                    IdUnidad = idUnidad ?? 77, // Asignar 0 si no se encuentra la unidad
                    Remito = worksheet.Cells[row, 18].Text,
                    IdConsumo = idConsumo,
                    Litros = decimal.Parse(worksheet.Cells[row, 20].Text),
                    ImporteTotalYer = decimal.Parse(worksheet.Cells[row, 24].Text),
                    ImporteSinImpuestos = decimal.Parse(worksheet.Cells[row, 24].Text) - SumarImpuestos(worksheet, row),
                    Factura = worksheet.Cells[row, 26].Text,
                    IdPeriodo = idPeriodo
                };

                return consumo;
            });
        }

        public async Task ExportarConsumosAExcelAsync(IEnumerable<ImportConsumoYpfEnRuta> consumos, string filePath)
        {
            await _excelService.ExportarAExcelAsync(consumos, filePath, "ConsumosYPF");
        }

        private async Task<int?> ObtenerIdUnidadDesdePatenteAsync(string patente)
        {
            var idTractor = await _unidadRepositorio.ObtenerIdTractorPorPatenteAsync(patente);
            return idTractor.HasValue
                ? await _unidadRepositorio.ObtenerIdUnidadPorTractorAsync(idTractor.Value)
                : null;
        }

        private decimal SumarImpuestos(ExcelWorksheet worksheet, int row)
        {
            return Enumerable.Range(27, 4) // Columnas AB, AC, AD, AE
                             .Select(col => decimal.Parse(worksheet.Cells[row, col].Text))
                             .Sum();
        }

        private int MapearNombreConsumo(string nombreConsumo)
        {
            return nombreConsumo switch
            {
                "ULTRADIESEL" => 25,
                "INFINIA DIESEL" => 26,
                "D.DIESEL 500" => 27,
                _ => throw new Exception($"Consumo '{nombreConsumo}' no reconocido.")
            };
        }
    }
}