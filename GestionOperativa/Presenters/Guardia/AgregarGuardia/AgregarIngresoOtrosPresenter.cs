using Configuraciones.Views;
using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionDocumental.Reports;
using GestionOperativa.Processor;
using GestionOperativa.Views.AgregarGuardia;
using Shared.Models;

namespace GestionOperativa.Presenters.AgregarGuardia
{
    public class AgregarIngresoOtrosPresenter : BasePresenter<IAgregarIngresoOtrosView>
    {
        private readonly IGuardiaIngresoOtrosRepositorio _guardiaRepositorio;
        private readonly IReporteConsumosNomTeOtrosProcessor _reporteConsumoTeOtros;

        public DateTime _Fecha;

        public AgregarIngresoOtrosPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IGuardiaIngresoOtrosRepositorio guardiaRepositorio,
            IReporteConsumosNomTeOtrosProcessor reporteConsumoTeOtros
        ) : base(sesionService, navigationService)
        {
            _guardiaRepositorio = guardiaRepositorio;
            _reporteConsumoTeOtros = reporteConsumoTeOtros;
        }

        public Task InicializarAsync(DateTime fecha, string patente)
        {
            _view.Fecha = fecha;
            _Fecha = fecha;
            _view.Patente = patente;
            return Task.CompletedTask;
        }

        public async Task GuardarAsync()
        {
            var ingresoOtros = new GuardiaIngresoOtros
            {
                Apellido = _view.Apellido,
                Nombre = _view.Nombre,
                Documento = _view.Documento,
                Licencia = _view.Licencia,
                Art = _view.Art,
                Patente = _view.Patente,
                Empresa = _view.Empresa,
                Observaciones = _view.Observaciones,
                Tipo = _view.TipoIngreso,
                Activo = true
            };

            int idPoc = await _guardiaRepositorio.RegistrarIngresoOtrosAsync(ingresoOtros, _sesionService.IdPosta, _Fecha, _sesionService.IdUsuario);
            _view.MostrarMensaje("Ingreso de Otros registrado correctamente.");

            TransitoEspecial te = new TransitoEspecial
            {
                RazonSocial = ingresoOtros.Empresa,
                Cuit = ingresoOtros.Documento,
                Apellido = ingresoOtros.Apellido,
                Nombre = ingresoOtros.Nombre,
                Documento = ingresoOtros.Documento,
                Licencia = ingresoOtros.Licencia,
                Art = ingresoOtros.Art,
                Tractor = _view.Patente,
                Seguro = null,
                Activo = true
            };
            ReporteIngresoTe? reporte = await _reporteConsumoTeOtros.ObtenerReporteTeOtros(idPoc, _Fecha, te);
            await GenerarPocIngresoOtros(reporte);
            _view.Close();
        }

        public async Task GenerarPocIngresoOtros(ReporteIngresoTe reporte)
        {
            await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
            {
                form.MostrarReporteDevExpress(reporte);
                return Task.CompletedTask;
            });
        }
    }
}