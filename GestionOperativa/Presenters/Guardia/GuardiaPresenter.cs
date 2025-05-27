using Configuraciones.Views;
using Core.Base;
using Core.Reports;
using Core.Repositories;
using Core.Services;
using GestionDocumental.Reports;
using GestionOperativa.Processor;
using GestionOperativa.Views;
using GestionOperativa.Views.AgregarGuardia;
using Shared.Models;

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
        private readonly IPOCRepositorio _pocRepositorio;
        private readonly IReporteConsumosNomTeOtrosProcessor _consumoNomTeProcessor;
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly IPeriodoRepositorio _periodoRepositorio;
        private readonly IReporteNominasProcessor _reporteNominasProcessor;



        private int _idPosta;

        public GuardiaPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IGuardiaRepositorio guardiaRepositorio,
            ITeRepositorio teRepositorio,
            IGuardiaIngresoOtrosRepositorio ingresoOtrosRepositorio,
            IAlertaRepositorio alertaRepositorio,
            INominaRepositorio nominaRepositorio,
            IReporteConsumosNomTeOtrosProcessor consumoNomTeProcessor,
            IPOCRepositorio pocRepositorio,
            IPostaRepositorio postaRepositorio,
            IPeriodoRepositorio periodoRepositorio,
            IReporteNominasProcessor reporteNominasProcessor,
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
            _periodoRepositorio = periodoRepositorio;
            _consumoNomTeProcessor = consumoNomTeProcessor;
            _reporteNominasProcessor = reporteNominasProcessor;
        }

        public async Task InicializarAsync(int idPosta)
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

            if (await _guardiaRepositorio.EstaEnParadorAsync(patente))
            {
                _view.MostrarMensaje("La unidad ya se encuentra en un parador.");
                return;
            }

            await AbrirFormularioAsync<AgregarIngresoOtrosForm>(async form =>
                {
                    await form._presenter.InicializarAsync(fecha, patente);
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
                    await form._presenter.InicializarAsync(fecha, patente);
                });
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

            POC poc = new POC()
            {
                NumeroPoc = posta.Codigo + "-" + idPoc.ToString(),  
                IdPosta = posta.IdPosta,
                IdUnidad = nomina.IdUnidad,
                IdChofer = nomina.IdChofer, 
                Odometro = 0, // ver si tomar del satelital
                FechaCreacion = fecha,
                FechaCierre = null,
                IdPeriodo = idPeriodo ?? 0, // Cambiar por el periodo correcto  
                IdUsuario = _sesionService.IdUsuario,   
                Estado = "Abierta"
            };

            await _pocRepositorio.AgregarPOCAsync(poc);
            ReporteControlOperativoConsumos? reporte = await _consumoNomTeProcessor.ObtenerReporteConsumosNomina(nomina.IdNomina, idPoc, fecha);
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

            await _guardiaRepositorio.RegistrarSalidaAsync(
                guardia.IdGuardiaIngreso,
                _sesionService.IdUsuario,
                fecha,
                esManual ? "Salida - Manual" : "Salida"
            );

            _view.MostrarMensaje(esManual ? "Salida manual registrada correctamente." : "Salida registrada correctamente.");
            await InicializarAsync(_idPosta);
        }

        public async Task AbrirCambioEstadoAsync(GuardiaDto guardia)
        {
            await AbrirFormularioAsync<CambiarEstadoForm>(async form =>
            {
                await form._presenter.InicializarAsync(guardia, false);
            });
            await InicializarAsync(_idPosta);
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
    }
}