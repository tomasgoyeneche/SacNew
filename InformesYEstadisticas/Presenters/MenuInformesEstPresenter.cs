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
        private readonly IPlanillaRepositorio _planillaRepositorio;

        public MenuInformesEstPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IDocumentacionProcessor documentacionService,
            IPlanillaRepositorio planillaRepositorio,
            IConfRepositorio confRepositorio
        ) : base(sesionService, navigationService)
        {
            _documentacionService = documentacionService;
            _confRepositorio = confRepositorio;
            _planillaRepositorio = planillaRepositorio;
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
                Planilla planilla = await _planillaRepositorio.ObtenerPorIdAsync(2);
                // Obtener los datos desde el repositorio
                List<PlanillaPreguntaDto> preguntas = await _planillaRepositorio.ObtenerPreguntasPorPlanilla(2);
                // Crear una instancia del nuevo reporte DevExpress

                List<PlanillaPreguntaDto> preguntasReporte = preguntas.Select(p => new PlanillaPreguntaDto
                {
                    Orden = p.Orden,
                    Texto = p.Texto,
                    Conforme = p.Conforme,
                    Observaciones = FormatearObservaciones(p.Observaciones),
                    NoConforme = p.NoConforme,
                    EsEncabezado = EsEntero(p.Orden)
                }).OrderBy(p => p.Orden, new AlfanumericStringComparer())
                .ToList();

                ReporteVerifMensual reporte = new ReporteVerifMensual();
                reporte.DataSource = new List<Planilla?> { planilla };
                reporte.DataMember = "";

                reporte.PreguntasPlanilla.DataSource = preguntasReporte;
                reporte.PreguntasPlanilla.DataMember = ""; // si es lista directa

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
                Planilla? planilla = await _planillaRepositorio.ObtenerPorIdAsync(3);
                // Obtener los datos desde el repositorio
                List<PlanillaPreguntaDto> preguntas = await _planillaRepositorio.ObtenerPreguntasPorPlanilla(3);
                // Crear una instancia del nuevo reporte DevExpress

                List<PlanillaPreguntaDto> preguntasReporte = preguntas.Select(p => new PlanillaPreguntaDto
                {
                    Orden = p.Orden,
                    Texto = p.Texto,
                    DescripcionUnidadTipo = p.DescripcionUnidadTipo,
                    Conforme = p.Conforme,
                    Observaciones = FormatearObservaciones(p.Observaciones),
                    NoConforme = p.NoConforme
                }).OrderBy(p => p.Orden, new AlfanumericStringComparer())
                .ToList();

                ReporteControlOperativoConsumos reporte = new ReporteControlOperativoConsumos();
                reporte.DataSource = new List<Planilla?> { planilla };
                reporte.DataMember = "";

                reporte.DetailReport.DataSource = preguntasReporte;
                reporte.DetailReport.DataMember = ""; // si es lista directa

                await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
            });
        }

        private string FormatearObservaciones(string observaciones)
        {
            if (string.IsNullOrEmpty(observaciones))
                return "";

            // Si es N/A, devolver vacío
            if (observaciones.Trim().Equals("N/A", StringComparison.OrdinalIgnoreCase))
                return "";

            if (observaciones.Trim().Equals("SI-NO-N/A", StringComparison.OrdinalIgnoreCase))
            {
                return "SI | NO";
            }

            if (observaciones.Trim().Equals("MATERIAL", StringComparison.OrdinalIgnoreCase))
            {
                return "N/A";
            }

            // Eliminar guiones finales si hay uno solo
            if (observaciones.EndsWith("-") && observaciones.Count(c => c == '-') == 1)
            {
                return observaciones.TrimEnd('-');
            }
            else
            {
                // Reemplazar los guiones por barras
                return observaciones.TrimEnd('-').Replace("-", " | ");
            }
        }

        private bool EsEntero(string orden)
        {
            if (string.IsNullOrEmpty(orden))
                return false;

            // Si contiene un punto o una coma, no es entero
            if (orden.Contains(".") || orden.Contains(","))
                return false;

            return int.TryParse(orden, out _);
        }
    }
}