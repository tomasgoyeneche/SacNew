using Configuraciones.Views;
using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionDocumental.Reports;
using GestionOperativa.Processor;
using GestionOperativa.Reports;
using InformesYEstadisticas.Views;
using Shared.Models;
using Shared.Models.DTOs;

namespace InformesYEstadisticas.Presenters
{
    public class MenuInformesEstPresenter : BasePresenter<IMenuInformesEstView>
    {
        private readonly IDocumentacionProcessor _documentacionService;
        private readonly IConfRepositorio _confRepositorio;
        private readonly IProgramaRepositorio _programaRepositorio;
        private readonly IExcelService _excelService;

        private readonly IPlanillaRepositorio _planillaRepositorio;
        private readonly IReporteConsumosNomTeOtrosProcessor _ReporteConsumosNomTeOtrosProcessor;

        public MenuInformesEstPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IDocumentacionProcessor documentacionService,
            IPlanillaRepositorio planillaRepositorio,
            IProgramaRepositorio programaRepositorio,
            IExcelService excelService,
            IReporteConsumosNomTeOtrosProcessor ReporteConsumosNomTeOtrosProcessor,
            IConfRepositorio confRepositorio
        ) : base(sesionService, navigationService)
        {
            _documentacionService = documentacionService;
            _confRepositorio = confRepositorio;
            _planillaRepositorio = planillaRepositorio;
            _ReporteConsumosNomTeOtrosProcessor = ReporteConsumosNomTeOtrosProcessor;
            _programaRepositorio = programaRepositorio;
            _excelService = excelService;
        }

        public async Task GenerarFichaVacia()
        {
            await EjecutarConCargaAsync(async () =>
            {
                Planilla planilla = await _planillaRepositorio.ObtenerPorIdAsync(1);
                // Obtener los datos desde el repositorio
                List<PlanillaPreguntaDto> preguntas = await _planillaRepositorio.ObtenerPreguntasPorPlanilla(1);

                List<PlanillaPreguntaDto> listaOrdenada = preguntas
                .OrderBy(p => p.Orden, new AlfanumericStringComparer())
                .ToList();
                // Crear una instancia del nuevo reporte DevExpress
                ReporteFichaTecnicaUnidadVacia reporte = new ReporteFichaTecnicaUnidadVacia();
                reporte.DataSource = new List<Planilla> { planilla };
                reporte.DataMember = "";

                reporte.DetailReportPreg.DataSource = listaOrdenada;
                reporte.DetailReportPreg.DataMember = ""; // si es lista directa

                await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
            });
        }

        public async Task GenerarVerifMensual()
        {
            await EjecutarConCargaAsync(async () =>
            {
                ReporteVerifMensual? reporte = await _ReporteConsumosNomTeOtrosProcessor.ObtenerReporteVerifMensual(0, 1, DateTime.Now);

                await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
            });
        }

        public async Task GenerarReporteConsumos()
        {
            await EjecutarConCargaAsync(async () =>
            {
                ReporteControlOperativoConsumos? reporte = await _ReporteConsumosNomTeOtrosProcessor.ObtenerReporteConsumosNomina(0, 1, DateTime.Now);

                await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
            });
        }

        public async Task ExportarTransoftAsync(DateTime desde, DateTime hasta)
        {
            var lista = await _programaRepositorio.ObtenerTransoftAsync(desde, hasta);
            if (lista == null || !lista.Any())
            {
                _view.MostrarMensaje("No hay datos para el rango seleccionado.");
                return;
            }

            string carpeta = @"C:\Compartida\Exportaciones";
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string filePath = Path.Combine(carpeta, $"Transoft-{desde:yyyyMMdd}_a_{hasta:yyyyMMdd}.xlsx");
            await _excelService.ExportarAExcelAsync(lista, filePath, "Transoft");

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });

            _view.MostrarMensaje("Archivo Transoft exportado y abierto.");
        }

        public async Task ExportarTransoftMetanolAsync(DateTime desde, DateTime hasta)
        {
            var lista = await _programaRepositorio.ObtenerTransoftMetanolAsync(desde, hasta);
            if (lista == null || !lista.Any())
            {
                _view.MostrarMensaje("No hay datos para el rango seleccionado.");
                return;
            }

            string carpeta = @"C:\Compartida\Exportaciones";
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string filePath = Path.Combine(carpeta, $"TransoftMetanol-{desde:yyyyMMdd}_a_{hasta:yyyyMMdd}.xlsx");
            await _excelService.ExportarAExcelAsync(lista, filePath, "TransoftMetanol");

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });

            _view.MostrarMensaje("Archivo TransoftMetanol exportado y abierto.");
        }

        public async Task ExportarAcumuladoAsync(DateTime desde, DateTime hasta)
        {
            var lista = await _programaRepositorio.ObtenerAcumuladoAsync(desde, hasta);
            if (lista == null || !lista.Any())
            {
                _view.MostrarMensaje("No hay datos para el rango seleccionado.");
                return;
            }

            string carpeta = @"C:\Compartida\Exportaciones";
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string filePath = Path.Combine(carpeta, $"Acumulado-{desde:yyyyMMdd}_a_{hasta:yyyyMMdd}.xlsx");
            await _excelService.ExportarAExcelAsync(lista, filePath, "Acumulado");

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });

            _view.MostrarMensaje("Archivo Acumulado exportado y abierto.");
        }

        public async Task ExportarAnuladoAsync(DateTime desde, DateTime hasta)
        {
            var lista = await _programaRepositorio.ObtenerAnuladosAsync(desde, hasta);
            if (lista == null || !lista.Any())
            {
                _view.MostrarMensaje("No hay datos para el rango seleccionado.");
                return;
            }

            string carpeta = @"C:\Compartida\Exportaciones";
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string filePath = Path.Combine(carpeta, $"Anulados-{desde:yyyyMMdd}_a_{hasta:yyyyMMdd}.xlsx");
            await _excelService.ExportarAExcelAsync(lista, filePath, "Anulado");

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });

            _view.MostrarMensaje("Archivo Acumulado exportado y abierto.");
        }

        public async Task ExportarAsignadosCargadosAsync(DateTime desde, DateTime hasta)
        {
            var lista = await _programaRepositorio.ObtenerAsignadosCargadosAsync(desde, hasta);
            if (lista == null || !lista.Any())
            {
                _view.MostrarMensaje("No hay datos para el rango seleccionado.");
                return;
            }

            string carpeta = @"C:\Compartida\Exportaciones";
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string filePath = Path.Combine(carpeta, $"AsignadosCargados-{desde:yyyyMMdd}_a_{hasta:yyyyMMdd}.xlsx");
            await _excelService.ExportarAExcelAsync(lista, filePath, "AsignadosCargados");

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });

            _view.MostrarMensaje("Archivo Acumulado exportado y abierto.");
        }
    }
}