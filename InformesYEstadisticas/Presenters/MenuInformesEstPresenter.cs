using Configuraciones.Views;
using Core.Base;
using Core.Reports;
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
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IVaporizadoRepositorio _vaporizadoRepositorio;
        private readonly IChoferEstadoRepositorio _choferEstadoRepositorio;
        private readonly IExcelService _excelService;
        private readonly IPlanillaRepositorio _planillaRepositorio;
        private readonly IReporteConsumosNomTeOtrosProcessor _ReporteConsumosNomTeOtrosProcessor;

        public MenuInformesEstPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IVaporizadoRepositorio vaporizadoRepositorio,
            IDocumentacionProcessor documentacionService,
            IUnidadRepositorio unidadRepositorio,
            INominaRepositorio nominaRepositorio,
            IChoferRepositorio choferRepositorio,
            IChoferEstadoRepositorio choferEstadoRepositorio,
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
            _unidadRepositorio = unidadRepositorio;
            _choferRepositorio = choferRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _vaporizadoRepositorio = vaporizadoRepositorio;
            _choferEstadoRepositorio = choferEstadoRepositorio;
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

        public async Task GenerarReporteNomina()
        {
            await EjecutarConCargaAsync(async () =>
            {
                // Obtener los datos desde el repositorio
                List<UnidadDto> unidades = await _unidadRepositorio.ObtenerUnidadesDtoAsync();
                var totalEquipos = unidades.Count;
                var totalEmpresas = unidades
                    .Select(u => u.Empresa_Unidad)
                    .Distinct()
                    .Count();
                // Crear una instancia del nuevo reporte DevExpress
                ResumenNominaMetanol reporte = new ResumenNominaMetanol();
                reporte.DataSource = unidades;
                reporte.Parameters["pTotalEmpresas"].Value = totalEmpresas;
                reporte.Parameters["pTotalEquipos"].Value = totalEquipos;
                reporte.DataMember = "";

                await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
            });
        }

        public async Task GenerarReporteVaporizados(DateTime desde, DateTime hasta, bool mostrarDetalle)
        {
            List<VaporizadoDto> lista = await _vaporizadoRepositorio.ObtenerTodosLosVaporizadosDto();
            if (lista == null || !lista.Any())
            {
                _view.MostrarMensaje("No hay datos para el rango seleccionado.");
                return;
            }

            List<VaporizadoDto> listaFiltrada = lista
            .Where(v => v.Inicio != null &&
                        v.Inicio.Value.Date >= desde.Date &&
                        v.Inicio.Value.Date <= hasta.Date)
            .ToList();

            if (!listaFiltrada.Any())
            {
                _view.MostrarMensaje("No hay datos para el rango de fechas seleccionado.");
                return;
            }

            ReporteGerencialVap reporte = new ReporteGerencialVap();
            reporte.DataSource = listaFiltrada;
            reporte.DataMember = "";

            string entreHasta =
              $"Período del reporte: {desde:dd/MM/yyyy} al {hasta.AddDays(-1):dd/MM/yyyy}";

            //reporte.Parameters["pTotalDias"].Value = totalDias;
            //reporte.Parameters["pTotalChoferes"].Value = totalChoferes;
            //reporte.Parameters["pDiasPeriodo"].Value = dias;
            reporte.Parameters["pMostrarDetalle"].Value = mostrarDetalle;
            reporte.Parameters["pEntreHasta"].Value = entreHasta;

            await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
            {
                form.MostrarReporteDevExpress(reporte);
                return Task.CompletedTask;
            });
        }

        public async Task GenerarReporteNovedadesChoferes(DateTime desde, DateTime hasta, bool mostrarDetalle, bool disponible)
        {
            List<NovedadesChoferesDto> lista = await _choferEstadoRepositorio.ObtenerTodasLasNovedadesDto();
            if (lista == null || !lista.Any())
            {
                _view.MostrarMensaje("No hay datos para el rango seleccionado.");
                return;
            }

            var listaFiltrada = await FiltrarYRecalcularNovedadesAsync(lista, desde, hasta, disponible);

            if (!listaFiltrada.Any())
            {
                _view.MostrarMensaje("No hay novedades en el rango seleccionado.");
                return;
            }
            int totalDias = listaFiltrada.Sum(x => x.Dias);

            List<Chofer> choferes = await _choferRepositorio.ObtenerTodosLosChoferesIncluyendoInactivos();

            int dias = (hasta.Date - desde.Date).Days;

            int totalChoferes = choferes
           .Where(c =>
               c.FechaAlta.Date < hasta.Date &&
               (c.FechaBaja == null || c.FechaBaja.Value.Date >= desde.Date))
           .Select(c => c.IdChofer)
           .Distinct()
           .Count();

            int totalDiasPeriodo = await CalcularTotalDiasPeriodoAsync(
              choferes,
              desde,
              hasta,
              disponible
            );

            string entreHasta =
             $"Período del reporte: {desde:dd/MM/yyyy} al {hasta.AddDays(-1):dd/MM/yyyy}";

            ReporteNovedadesChoferes reporte = new ReporteNovedadesChoferes();
            reporte.DataSource = listaFiltrada;
            reporte.DataMember = "";

            reporte.Parameters["pTotalDias"].Value = totalDias;
            reporte.Parameters["pTotalChoferes"].Value = totalChoferes;
            reporte.Parameters["pTotalDiasPeriodo"].Value = totalDiasPeriodo;
            reporte.Parameters["pMostrarDetalle"].Value = mostrarDetalle;
            reporte.Parameters["pEntreHasta"].Value = entreHasta;

            reporte.Parameters["pDiasPeriodo"].Value = dias;

            await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
            {
                form.MostrarReporteDevExpress(reporte);
                return Task.CompletedTask;
            });
        }

        private async Task<int> CalcularTotalDiasPeriodoAsync(
        List<Chofer> choferes,
        DateTime desde,
        DateTime hasta,
        bool disponible)
        {
            desde = desde.Date;
            hasta = hasta.Date;

            int totalDiasPeriodo = 0;

            foreach (var chofer in choferes)
            {
                // Primero: intersección por alta / baja
                if (chofer.FechaAlta.Date < hasta &&
                    (chofer.FechaBaja == null || chofer.FechaBaja.Value.Date >= desde))
                {
                    DateTime inicioConteo = chofer.FechaAlta.Date < desde
                        ? desde
                        : chofer.FechaAlta.Date;

                    DateTime finConteo = chofer.FechaBaja.HasValue
                        ? (chofer.FechaBaja.Value.AddDays(1).Date > hasta
                            ? hasta
                            : chofer.FechaBaja.Value.AddDays(1).Date)
                        : hasta;

                    for (DateTime fecha = inicioConteo; fecha < finConteo; fecha = fecha.AddDays(1))
                    {
                        if (!disponible)
                        {
                            // Comportamiento actual
                            totalDiasPeriodo++;
                        }
                        else
                        {
                            // Solo días con nómina activa
                            var nomina = await _nominaRepositorio
                                .ObtenerNominaActivaPorChoferAsync(chofer.IdChofer, fecha);

                            if (nomina != null)
                                totalDiasPeriodo++;
                        }
                    }
                }
            }

            return totalDiasPeriodo;
        }

        private async Task<List<NovedadesChoferesDto>> FiltrarYRecalcularNovedadesAsync(
        List<NovedadesChoferesDto> lista,
        DateTime desde,
        DateTime hasta,
        bool disponible)
        {
            desde = desde.Date;
            hasta = hasta.Date;

            var resultado = new List<NovedadesChoferesDto>();

            foreach (var n in lista)
            {
                // 1️⃣ Ver intersección básica con el período
                if (n.FechaInicio < hasta && n.FechaFin >= desde)
                {
                    DateTime inicioConteo = n.FechaInicio < desde
                        ? desde
                        : n.FechaInicio.Date;

                    DateTime finConteo = n.FechaFin.AddDays(1) > hasta
                        ? hasta
                        : n.FechaFin.AddDays(1);

                    int diasCalculados = 0;

                    // 2️⃣ Contar días
                    for (DateTime fecha = inicioConteo; fecha < finConteo; fecha = fecha.AddDays(1))
                    {
                        if (!disponible)
                        {
                            // comportamiento actual
                            diasCalculados++;
                        }
                        else
                        {
                            // solo contar si tiene nómina activa ese día
                            var nomina = await _nominaRepositorio
                                .ObtenerNominaActivaPorChoferAsync(n.idChofer, fecha);

                            if (nomina != null)
                                diasCalculados++;
                        }
                    }

                    // 3️⃣ Si no quedó ningún día válido, no entra al informe
                    if (diasCalculados > 0)
                    {
                        resultado.Add(new NovedadesChoferesDto
                        {
                            idEstadoChofer = n.idEstadoChofer,
                            idChofer = n.idChofer,
                            NombreCompleto = n.NombreCompleto,
                            idEstado = n.idEstado,
                            Descripcion = n.Descripcion,
                            Abreviado = n.Abreviado,
                            FechaInicio = n.FechaInicio,
                            FechaFin = n.FechaFin,
                            Dias = diasCalculados,
                            Disponible = n.Disponible,
                            Observaciones = n.Observaciones
                        });
                    }
                }
            }

            return resultado;
        }

        //    private List<NovedadesChoferesDto> FiltrarYRecalcularNovedades(
        //List<NovedadesChoferesDto> lista,
        //DateTime desde,
        //DateTime hasta)
        //    {
        //        desde = desde.Date;
        //        hasta = hasta.Date;

        //        var resultado = new List<NovedadesChoferesDto>();

        //        foreach (var n in lista)
        //        {
        //            // 1️⃣ Ver si intersecta
        //            if (n.FechaInicio < hasta && n.FechaFin >= desde)
        //            {
        //                // 2️⃣ Calcular solo los días dentro del rango
        //                DateTime inicioConteo = n.FechaInicio < desde
        //                    ? desde
        //                    : n.FechaInicio.Date;

        //                DateTime finConteo = n.FechaFin.AddDays(1) > hasta
        //                    ? hasta
        //                    : n.FechaFin.AddDays(1);

        //                int dias = (finConteo - inicioConteo).Days;

        //                if (dias > 0)
        //                {
        //                    // 3️⃣ Copiar DTO sin tocar fechas
        //                    resultado.Add(new NovedadesChoferesDto
        //                    {
        //                        idEstadoChofer = n.idEstadoChofer,
        //                        idChofer = n.idChofer,
        //                        NombreCompleto = n.NombreCompleto,
        //                        idEstado = n.idEstado,
        //                        Descripcion = n.Descripcion,
        //                        Abreviado = n.Abreviado,
        //                        FechaInicio = n.FechaInicio,
        //                        FechaFin = n.FechaFin,
        //                        Dias = dias,
        //                        Disponible = n.Disponible,
        //                        Observaciones = n.Observaciones
        //                    });
        //                }
        //            }
        //        }

        //        return resultado;
        //    }

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