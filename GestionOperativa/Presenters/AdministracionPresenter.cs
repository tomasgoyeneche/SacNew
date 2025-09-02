using Configuraciones.Views;
using Core.Base;
using Core.Reports;
using Core.Repositories;
using Core.Services;
using GestionDocumental.Reports;
using GestionOperativa.Processor;
using GestionOperativa.Reports;
using GestionOperativa.Views;
using Servicios;
using Shared.Models;
using System.IO;

namespace GestionOperativa.Presenters
{
    public class AdministracionPresenter : BasePresenter<IAdministracionView>
    {
        private readonly IGuardiaRepositorio _guardiaRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IAlertaRepositorio _alertaRepositorio;
        private readonly ITeRepositorio _teRepositorio;
        private readonly IGuardiaIngresoOtrosRepositorio _ingresoOtrosRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IProgramaRepositorio _programaRepositorio;
        private readonly IExcelService _excelService;
        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;
        private readonly IConfRepositorio _confRepositorio;
        private readonly IReporteConsumosNomTeOtrosProcessor _reporteConsumosNomTeOtrosProcessor;
        private readonly IPOCRepositorio _pocRepositorio;
        private readonly IReporteNominasProcessor _reporteNominasProcessor;

        private int _idPosta;

        public AdministracionPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IGuardiaRepositorio guardiaRepositorio,
            ITeRepositorio teRepositorio,
            IGuardiaIngresoOtrosRepositorio ingresoOtrosRepositorio,
            IAlertaRepositorio alertaRepositorio,
            INominaRepositorio nominaRepositorio,
            ITractorRepositorio tractorRepositorio,
            IProgramaRepositorio programaRepositorio,
            IExcelService excelService,
            IChoferRepositorio choferRepositorio,
            IReporteConsumosNomTeOtrosProcessor reporteConsumosNomTeOtrosProcessor,
            IReporteNominasProcessor reporteNominasProcessor,
            ISemiRepositorio semiRepositorio,
            IConfRepositorio confRepositorio,
            IPOCRepositorio pocRepositorio,
            IUnidadRepositorio unidadRepositorio)
            : base(sesionService, navigationService)
        {
            _guardiaRepositorio = guardiaRepositorio;
            _teRepositorio = teRepositorio;
            _ingresoOtrosRepositorio = ingresoOtrosRepositorio;
            _alertaRepositorio = alertaRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _tractorRepositorio = tractorRepositorio;
            _choferRepositorio = choferRepositorio;
            _confRepositorio = confRepositorio;
            _semiRepositorio = semiRepositorio;
            _programaRepositorio = programaRepositorio;
            _excelService = excelService;
            _pocRepositorio = pocRepositorio;
            _reporteNominasProcessor = reporteNominasProcessor;
            _reporteConsumosNomTeOtrosProcessor = reporteConsumosNomTeOtrosProcessor;
        }

        public async Task InicializarAsync(int idPosta)
        {
            _idPosta = idPosta;

            List<GuardiaDto> guardias = await _guardiaRepositorio.ObtenerGuardiasPorPostaAsync(idPosta);
            if (guardias != null)
            {
                _view.MostrarGuardia(guardias);

                var resumenPorEstado = guardias
                    .GroupBy(g => g.IdEstadoEvento)
                    .ToList();

                var resumen = new List<(string Descripcion, int Cantidad)>();
                foreach (var grupo in resumenPorEstado)
                {
                    var estado = await _guardiaRepositorio.ObtenerEstadoPorIdAsync(grupo.Key);
                    resumen.Add((estado?.Descripcion ?? "Desconocido", grupo.Count()));
                }

                _view.MostrarResumen(resumen);
            }
        }

        public async Task MostrarHistorialAsync(int idGuardiaIngreso)
        {
            var historial = await _guardiaRepositorio.ObtenerHistorialPorIngresoAsync(idGuardiaIngreso);
            _view.MostrarHistorial(historial);
        }

        public async Task CargarVencimientosYAlertasAsync(GuardiaDto guardia)
        {
            List<VencimientosDto> vencimientos = new();
            List<AlertaDto> alertas = new();
            DateTime fechaLimite = DateTime.Now.AddDays(20);

            switch (guardia.TipoIngreso)
            {
                case 1: // Nómina
                    Nomina? nomina = await _nominaRepositorio.ObtenerPorIdAsync(guardia.IdEntidad);
                    var vencNomina = await _nominaRepositorio.ObtenerVencimientosPorNominaAsync(nomina);
                    vencimientos.AddRange(vencNomina.Where(v => v.FechaVencimiento <= fechaLimite));

                    var alertasNomina = await _alertaRepositorio.ObtenerAlertasPorIdNominaAsync(guardia.IdEntidad);
                    alertas.AddRange(alertasNomina);
                    break;

                case 2: // Tránsito Especial
                    TransitoEspecial? te = await _teRepositorio.ObtenerPorIdAsync(guardia.IdEntidad);
                    if (te != null)
                    {
                        if (te.Licencia.HasValue && te.Licencia < fechaLimite)
                            vencimientos.Add(new VencimientosDto { Descripcion = "Licencia", FechaVencimiento = te.Licencia.Value });

                        if (te.Seguro.HasValue && te.Seguro < fechaLimite)
                            vencimientos.Add(new VencimientosDto { Descripcion = "Seguro", FechaVencimiento = te.Seguro.Value });

                        if (te.Art.HasValue && te.Art < fechaLimite)
                            vencimientos.Add(new VencimientosDto { Descripcion = "ART", FechaVencimiento = te.Art.Value });
                    }
                    break;

                case 3: // Otros
                    GuardiaIngresoOtros? otros = await _ingresoOtrosRepositorio.ObtenerPorIdAsync(guardia.IdEntidad);
                    if (otros != null)
                    {
                        if (otros.Licencia.HasValue && otros.Licencia < fechaLimite)
                            vencimientos.Add(new VencimientosDto { Descripcion = "Licencia", FechaVencimiento = otros.Licencia.Value });

                        if (otros.Art.HasValue && otros.Art < fechaLimite)
                            vencimientos.Add(new VencimientosDto { Descripcion = "ART", FechaVencimiento = otros.Art.Value });
                    }
                    break;
            }

            _view.MostrarVencimientos(vencimientos.OrderBy(v => v.FechaVencimiento).ToList());
            _view.MostrarAlertas(alertas);
        }

        public async Task MostrarDatosYFotoAsync(GuardiaDto guardia)
        {
            switch (guardia.TipoIngreso)
            {
                case 1: // Nómina
                    Nomina? nomina = await _nominaRepositorio.ObtenerPorIdAsync(guardia.IdEntidad);
                    UnidadDto? unidadDto = await _unidadRepositorio.ObtenerPorIdDtoAsync(nomina.IdUnidad);
                    Unidad? unidad = await _unidadRepositorio.ObtenerPorUnidadIdAsync(nomina.IdUnidad);
                    ChoferDto? chofer = await _choferRepositorio.ObtenerPorIdDtoAsync(nomina.IdChofer);
                    TractorDto? tractor = await _tractorRepositorio.ObtenerPorIdDtoAsync(unidad.IdTractor);
                    SemiDto? semi = await _semiRepositorio.ObtenerPorIdDtoAsync(unidad.IdSemi);

                    string? rutaFoto = null;

                    if (chofer != null)
                    {
                        rutaFoto = await ObtenerRutaPorIdAsync(1, "", chofer.Documento + ".jpg");
                    }

                    string patenteUnidad = $"{unidadDto.Tractor_Patente}_{unidadDto.Semirremolque_Patente}";
                    string? rutaFotoUnidad = await ObtenerRutaPorIdAsync(6, "Nomina", $"{patenteUnidad}.jpg");

                    _view.MostrarDatosNomina(chofer, tractor, semi, unidadDto, rutaFoto, rutaFotoUnidad);

                    break;

                case 2: // Nómina
                    TransitoEspecial? transitoEspecial = await _teRepositorio.ObtenerPorIdAsync(guardia.IdEntidad);
                    _view.MostrarDatosTe(transitoEspecial);
                    break;

                case 3: // Nómina
                    GuardiaIngresoOtros? ingresoOtros = await _ingresoOtrosRepositorio.ObtenerPorIdAsync(guardia.IdEntidad);
                    _view.MostrarDatosOtros(ingresoOtros);
                    break;

                default:
                    break;
            }
        }

        private async Task<string?> ObtenerRutaPorIdAsync(int idConf, string subDirectorio, string archivo)
        {
            var conf = await _confRepositorio.ObtenerRutaPorIdAsync(idConf);
            return conf == null || string.IsNullOrEmpty(conf.Ruta)
                ? null
                : Path.Combine(conf.Ruta, subDirectorio, archivo);
        }

        public async Task AbrirCambioEstadoAsync(GuardiaDto guardia)
        {
            await AbrirFormularioAsync<CambiarEstadoForm>(async form =>
            {
                await form._presenter.InicializarAsync(guardia, true);
            });
            await InicializarAsync(_idPosta);
        }

        public async Task GenerarReporteFlotaAsync()
        {
            // Obtener los datos desde el repositorio
            ReporteNominaMetanolActiva? reporte = await _reporteNominasProcessor.ObtenerReporteNominaMetanol();

            await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
        }

        public async Task GenerarNominaActual()
        {
            ReporteNominaActual? reporte = await _reporteNominasProcessor.ObtenerReporteNominaActual();

            await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
            {
                form.MostrarReporteDevExpress(reporte);
                return Task.CompletedTask;
            });
        }

        public async Task GenerarReporteTelefonosNomina()
        {
            ReporteNominaTelefonos? reporte = new ReporteNominaTelefonos();

            List<ChoferDto?> choferes = await _choferRepositorio.ObtenerTodosLosChoferesDto();

            reporte.DataSource = choferes;

            await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
            {
                form.MostrarReporteDevExpress(reporte);
                return Task.CompletedTask;
            });
        }

        public async Task MostrarEquiposEnParador()
        {
            ReporteEquiposEnParador? reporte = new ReporteEquiposEnParador();

            List<GuardiaDto?> guardia = await _guardiaRepositorio.ObtenerGuardiasPorPostaAsync(_idPosta);

            reporte.DataSource = guardia;

            await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
            {
                form.MostrarReporteDevExpress(reporte);
                return Task.CompletedTask;
            });
        }

        public async Task ReimprimirPoc(GuardiaDto guardia)
        {
            switch (guardia.TipoIngreso)
            {
                case 1:
                    ReporteControlOperativoConsumos? reporte = await _reporteConsumosNomTeOtrosProcessor.ObtenerReporteConsumosNomina(guardia.IdEntidad, guardia.IdGuardiaIngreso, guardia.Ingreso);
                    await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                    {
                        form.MostrarReporteDevExpress(reporte);
                        return Task.CompletedTask;
                    });
                    break;

                case 2:
                    TransitoEspecial te = await _teRepositorio.ObtenerPorIdAsync(guardia.IdEntidad);
                    ReporteIngresoTe? reporteTe = await _reporteConsumosNomTeOtrosProcessor.ObtenerReporteTeOtros(guardia.IdGuardiaIngreso, guardia.Ingreso, te);
                    await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                    {
                        form.MostrarReporteDevExpress(reporteTe);
                        return Task.CompletedTask;
                    });
                    break;

                case 3:
                    GuardiaIngresoOtros? otros = await _ingresoOtrosRepositorio.ObtenerPorIdAsync(guardia.IdEntidad);
                    TransitoEspecial teOtros = new TransitoEspecial
                    {
                        RazonSocial = otros.Empresa,
                        Cuit = otros.Documento,
                        Apellido = otros.Apellido,
                        Nombre = otros.Nombre,
                        Documento = otros.Documento,
                        Licencia = otros.Licencia,
                        Art = otros.Art,
                        Tractor = otros.Patente,
                        Seguro = null,
                        Activo = true
                    };
                    ReporteIngresoTe? reporteOtros = await _reporteConsumosNomTeOtrosProcessor.ObtenerReporteTeOtros(guardia.IdGuardiaIngreso, guardia.Ingreso, teOtros);
                    await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                    {
                        form.MostrarReporteDevExpress(reporteOtros);
                        return Task.CompletedTask;
                    });
                    break;

                default:
                    break;
            }
        }

        public async Task VerifMensual(GuardiaDto guardia)
        {
            ReporteVerifMensual? reporte = await _reporteConsumosNomTeOtrosProcessor.ObtenerReporteVerifMensual(guardia.IdEntidad, guardia.IdGuardiaIngreso, guardia.Ingreso);
            await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
            {
                form.MostrarReporteDevExpress(reporte);
                return Task.CompletedTask;
            });
        }

        public async Task AbrirVaporizados(string tipoPermiso)
        {
            await AbrirFormularioConPermisosAsync<MenuVaporizados>(tipoPermiso, async form =>
            {
                await form._presenter.CargarVaporizadosAsync(_sesionService.IdPosta);
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
    }
}