using Configuraciones.Views;
using Core.Base;
using Core.Reports;
using Core.Repositories;
using Core.Services;
using GestionDocumental.Reports;
using GestionOperativa.Processor;
using GestionOperativa.Views;
using GestionOperativa.Views.AgregarGuardia;
using Servicios;
using Shared.Models;
using System.IO;

namespace GestionOperativa.Presenters
{
    public class GuardiaPresenter : BasePresenter<IGuardiaView>
    {
        private readonly IGuardiaRepositorio _guardiaRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IAlertaRepositorio _alertaRepositorio;
        private readonly ITeRepositorio _teRepositorio;
        private readonly IGuardiaIngresoOtrosRepositorio _ingresoOtrosRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IProgramaRepositorio _programaRepositorio;
        private readonly IExcelService _excelService;

        private readonly IPOCRepositorio _pocRepositorio;
        private readonly IReporteConsumosNomTeOtrosProcessor _consumoNomTeProcessor;
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly IPeriodoRepositorio _periodoRepositorio;
        private readonly IReporteNominasProcessor _reporteNominasProcessor;

        private readonly IVaporizadoRepositorio _vaporizadoRepositorio;

        private int _idPosta;

        public GuardiaPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IGuardiaRepositorio guardiaRepositorio,
            ITeRepositorio teRepositorio,
            IGuardiaIngresoOtrosRepositorio ingresoOtrosRepositorio,
            IAlertaRepositorio alertaRepositorio,
            INominaRepositorio nominaRepositorio,
            IProgramaRepositorio programaRepositorio,
            IExcelService excelService,
            IReporteConsumosNomTeOtrosProcessor consumoNomTeProcessor,
            IPOCRepositorio pocRepositorio,
            IPostaRepositorio postaRepositorio,
            IPeriodoRepositorio periodoRepositorio,
            IReporteNominasProcessor reporteNominasProcessor,
            IVaporizadoRepositorio vaporizadoRepositorio,
            IUnidadRepositorio unidadRepositorio)
            : base(sesionService, navigationService)
        {
            _guardiaRepositorio = guardiaRepositorio;
            _teRepositorio = teRepositorio;
            _ingresoOtrosRepositorio = ingresoOtrosRepositorio;
            _alertaRepositorio = alertaRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _pocRepositorio = pocRepositorio;
            _postaRepositorio = postaRepositorio;
            _programaRepositorio = programaRepositorio;
            _excelService = excelService;
            _periodoRepositorio = periodoRepositorio;
            _consumoNomTeProcessor = consumoNomTeProcessor;
            _reporteNominasProcessor = reporteNominasProcessor;
            _vaporizadoRepositorio = vaporizadoRepositorio;
        }

        public async Task InicializarAsync(int idPosta, int? idGuardiaSeleccionada = null)
        {
            _idPosta = idPosta;

            List<GuardiaDto> guardias = await _guardiaRepositorio.ObtenerGuardiasPorPostaAsync(idPosta);
            if (guardias != null)
            {
                _view.MostrarGuardia(guardias);

                // Generar resumen
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

                var resu = await _guardiaRepositorio.ObtenerResumenEnParadorAsync(_idPosta);
                _view.MostrarResumenParador(resu.unidades, resu.tractores, resu.semis, resu.choferes);

                if (idGuardiaSeleccionada.HasValue)
                {
                    _view.SeleccionarGuardiaPorId(idGuardiaSeleccionada.Value);
                }
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

        public async Task RegistrarOtrosAsync()
        {
            DateTime fecha = DateTime.Now;
            string patente = _view.PatenteIngresada.Trim().ToUpper();

            if (await _guardiaRepositorio.EstaEnParadorAsync(patente) && patente != "")
            {
                _view.MostrarMensaje("La unidad ya se encuentra en un parador.");
                return;
            }

            await AbrirFormularioAsync<AgregarIngresoOtrosForm>(async form =>
                {
                    await form._presenter.InicializarAsync(fecha, patente, _idPosta);
                });
            await InicializarAsync(_idPosta);
        }

        public async Task RegistrarIngresoAsync(bool esManual)
        {
            DateTime fecha = esManual ? _view.FechaManual.GetValueOrDefault() : DateTime.Now;

            string patente = _view.PatenteIngresada.Trim().ToUpper();
            if (string.IsNullOrEmpty(patente))
            {
                _view.MostrarMensaje("Debe ingresar una patente.");
                return;
            }

            if (await _guardiaRepositorio.EstaEnParadorAsync(patente))
            {
                _view.MostrarMensaje("La unidad ya se encuentra en un parador.");
                return;
            }
            // Buscar IdTractor
            int? idTractor = await _unidadRepositorio.ObtenerIdTractorPorPatenteAsync(patente);

            if (idTractor == null)
            {
                await AbrirFormularioAsync<AgregarEditarTransitoEspecialForm>(async form =>
                {
                    await form._presenter.InicializarAsync(fecha, patente, _idPosta);
                });
                _view.PatenteIngresada = string.Empty; // Limpiar campo de patente
                await InicializarAsync(_idPosta);
                return;
            }

            // Buscar IdUnidad
            int? idUnidad = await _unidadRepositorio.ObtenerIdUnidadPorTractorAsync(idTractor.Value);
            if (idUnidad == null)
            {
                _view.MostrarMensaje("Unidad no encontrada.");
                return;
            }

            Nomina? nomina = await _nominaRepositorio.ObtenerNominaActivaPorUnidadAsync(idUnidad.Value, fecha);
            if (nomina == null)
            {
                _view.MostrarMensaje("No se encontró una nómina activa para la unidad.");
                return;
            }

            List<AlertaDto> alertas = await _alertaRepositorio.ObtenerAlertasPorIdNominaAsync(nomina.IdNomina);

            var ingreso = new GuardiaIngreso
            {
                IdPosta = _idPosta,
                TipoIngreso = 1,
                IdNomina = nomina.IdNomina,
                IdTe = null,
                IdGuardiaIngresoOtros = null,
                IdGuardiaEstado = 1,
                FechaIngreso = fecha,
                Activo = true
            };

            int idPoc = await _guardiaRepositorio.RegistrarIngresoAsync(ingreso, _sesionService.IdUsuario, esManual ? "Ingreso - Manual" : "Ingreso");

            Posta posta = await _postaRepositorio.ObtenerPorIdAsync(_idPosta);
            int? idPeriodo = await _periodoRepositorio.ObtenerIdPeriodoPorMesAnioAsync(fecha.Month, fecha.Year);  // Esta mal ya que te devuelve siempre primera quincena corregir

            //POC poc = new POC()
            //{
            //    NumeroPoc = posta.Codigo + "-" + idPoc.ToString(),
            //    IdPosta = posta.IdPosta,
            //    IdUnidad = nomina.IdUnidad,
            //    IdChofer = nomina.IdChofer,
            //    Odometro = 0, // ver si tomar del satelital
            //    FechaCreacion = fecha,
            //    FechaCierre = null,
            //    IdPeriodo = idPeriodo ?? 0, // Cambiar por el periodo correcto
            //    IdUsuario = _sesionService.IdUsuario,
            //    Estado = "Abierta"
            //};

            await _nominaRepositorio.RegistrarNominaAsync(nomina.IdNomina, "Ingreso Guardia", $"{posta.Descripcion}-{DateTime.Now}", _sesionService.IdUsuario);
            _view.PatenteIngresada = string.Empty; // Limpiar campo de patente
            //await _pocRepositorio.AgregarPOCAsync(poc);
            ReporteControlOperativoConsumos? reporte = await _consumoNomTeProcessor.ObtenerReporteConsumosNomina(nomina.IdNomina, idPoc, fecha);
            foreach (AlertaDto ale in alertas)
            {
                _view.MostrarMensaje($"Alerta: {ale.Descripcion} eliminar si ya fue resuelta.");
            }
            _view.MostrarMensaje(esManual ? "Ingreso manual registrado correctamente." : "Ingreso registrado correctamente.");
            await GenerarReporte(reporte);
            await InicializarAsync(_idPosta);
        }

        public async Task GenerarReporte(ReporteControlOperativoConsumos reporte)
        {
            await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
            {
                form.MostrarReporteDevExpress(reporte);
                return Task.CompletedTask;
            });
        }

        public async Task RegistrarSalidaAsync(GuardiaDto guardia, bool esManual)
        {
            DateTime fecha = esManual ? _view.FechaSalidaManual.GetValueOrDefault() : DateTime.Now;

            if (guardia.IdEstadoEvento == 10)
            {
                _view.MostrarMensaje("Este ingreso ya fue dado de baja.");
                return;
            }

            GuardiaIngreso guardiaIngreso = await _guardiaRepositorio.ObtenerGuardiaPorId(guardia.IdGuardiaIngreso);
            List<GuardiaHistorialDto> historial = await _guardiaRepositorio.ObtenerHistorialPorIngresoAsync(guardia.IdGuardiaIngreso);

            // 2. Verificar si hay evento de vaporizado (id 7)
            bool tuvoVaporizado = historial.Any(h => h.IdGuardiaEstado == 7);

            if (tuvoVaporizado)
            {
                // 3. Buscar vaporizado relacionado (según tipo de ingreso)
                Vaporizado? vaporizado = null;
                if (guardia.TipoIngreso == 1) // Nomina
                    vaporizado = await _vaporizadoRepositorio.ObtenerPorNominaAsync(guardia.IdEntidad);
                else if (guardia.TipoIngreso == 2) // TE
                    vaporizado = await _vaporizadoRepositorio.ObtenerPorTeAsync(guardia.IdEntidad);

                // 4. Validar campos obligatorios (podés adaptar este helper a tu modelo)
                if (vaporizado == null || !VaporizadoCompleto(vaporizado))
                {
                    // Abrir el form para completar los datos del vaporizado
                    await AbrirFormularioAsync<AgregarEditarVaporizadoForm>(async form =>
                    {
                        await form._presenter.CargarDatosAsync(vaporizado, guardiaIngreso);
                    });

                    Vaporizado? vaporizadoActualizado = (guardia.TipoIngreso == 1)
                    ? await _vaporizadoRepositorio.ObtenerPorNominaAsync(guardia.IdEntidad)
                    : await _vaporizadoRepositorio.ObtenerPorTeAsync(guardia.IdEntidad);

                    if (vaporizadoActualizado == null || !VaporizadoCompleto(vaporizadoActualizado))
                    {
                        _view.MostrarMensaje("No se completaron todos los datos obligatorios del vaporizado. No se puede registrar la salida.");
                        return;
                    }
                }
            }

            if (guardia.TipoIngreso == 1)
            {
                // 1. Obtener la Posta
                Posta? posta = await _postaRepositorio.ObtenerPorIdAsync(guardia.IdPosta);
                //if (posta != null)
                //{
                //    // 2. Armar número de POC y buscar el POC
                //    string numeroPoc = $"{posta.Codigo}-{guardia.IdGuardiaIngreso}";
                //    POC? poc = await _pocRepositorio.ObtenerPorNumeroAsync(numeroPoc);

                //    // 3. Si existe, cerrar el POC
                //    if (poc != null)
                //    {
                //        await _pocRepositorio.ActualizarFechaCierreYEstadoAsync(poc.IdPoc, fecha, "cerrada");
                //    }
                //}
                await _nominaRepositorio.RegistrarNominaAsync(guardia.IdEntidad, "Salida Guardia", $"{posta.Descripcion}-{DateTime.Now}", _sesionService.IdUsuario);
            }

            await _guardiaRepositorio.RegistrarSalidaAsync(
                guardia.IdGuardiaIngreso,
                _sesionService.IdUsuario,
                fecha,
                esManual ? "Salida - Manual" : "Salida"
            );

            _view.MostrarMensaje(esManual ? "Salida manual registrada correctamente." : "Salida registrada correctamente.");
            await InicializarAsync(_idPosta);
        }

        private bool VaporizadoCompleto(Vaporizado vap)
        {
            // Adaptá la validación según tus reglas reales
            return
                !string.IsNullOrWhiteSpace(vap.NroCertificado)
                && !string.IsNullOrWhiteSpace(vap.RemitoDanes)
                && vap.CantidadCisternas.HasValue && vap.CantidadCisternas > 0
                && vap.IdVaporizadoMotivo.HasValue && vap.IdVaporizadoMotivo > 0
                && vap.FechaInicio.HasValue
                && vap.FechaFin.HasValue
                && vap.IdVaporizadoZona.HasValue && vap.IdVaporizadoZona > 0;
            // Sumá los campos que sean obligatorios para vos
        }

        public async Task AbrirCambioEstadoAsync(GuardiaDto guardia, int? idGuardiaSeleccionada = null)
        {
            await AbrirFormularioAsync<CambiarEstadoForm>(async form =>
            {
                await form._presenter.InicializarAsync(guardia, false);
            });
            await InicializarAsync(_idPosta, idGuardiaSeleccionada);
        }

        public async Task MostrarNominaActualAsync()
        {
            ReporteNominaActual? reporte = await _reporteNominasProcessor.ObtenerReporteNominaActual();

            await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
            {
                form.MostrarReporteDevExpress(reporte);
                return Task.CompletedTask;
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

        public async Task ExportarTeAsync(DateTime desde, DateTime hasta)
        {
            var lista = await _teRepositorio.ObtenerControlTransitoEspecial(desde, hasta);
            if (lista == null || !lista.Any())
            {
                _view.MostrarMensaje("No hay datos para el rango seleccionado.");
                return;
            }

            string carpeta = @"C:\Compartida\Exportaciones";
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string filePath = Path.Combine(carpeta, $"ControlTransitoEspecial-{desde:yyyyMMdd}_a_{hasta:yyyyMMdd}.xlsx");
            await _excelService.ExportarAExcelAsync(lista, filePath, "ControlTransitoEspecial");

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });

            _view.MostrarMensaje("Archivo TransitoEspecial exportado y abierto.");
        }
    }
}