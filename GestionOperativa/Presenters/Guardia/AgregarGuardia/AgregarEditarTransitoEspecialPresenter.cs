using Configuraciones.Views;
using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionDocumental.Reports;
using GestionOperativa.Processor;
using Shared.Models;

namespace GestionOperativa.Presenters
{
    public class AgregarEditarTransitoEspecialPresenter : BasePresenter<IAgregarEditarTransitoEspecialView>
    {
        private readonly IGuardiaRepositorio _guardiaRepositorio;
        private readonly ITeRepositorio _teRepositorio;

        private readonly IReporteConsumosNomTeOtrosProcessor _reporteConsumoTeOtros;
        public DateTime _Fecha;
        private int _idPosta;

        public AgregarEditarTransitoEspecialPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IReporteConsumosNomTeOtrosProcessor reporteConsumosNomTeOtrosProcessor,
            ITeRepositorio teRepositorio,
            IGuardiaRepositorio guardiaRepositorio
        ) : base(sesionService, navigationService)
        {
            _guardiaRepositorio = guardiaRepositorio;
            _reporteConsumoTeOtros = reporteConsumosNomTeOtrosProcessor;
            _teRepositorio = teRepositorio;
        }

        public async Task InicializarAsync(DateTime fecha, string patente, int idPosta)
        {
            _idPosta = idPosta;
            _view.Fecha = fecha;
            _Fecha = fecha;
            _view.Tractor = patente;

            var empresas = await _teRepositorio.ObtenerEmpresasTransitoEspecialAsync();
            _view.CargarEmpresasTransitoEspecial(empresas);
        }

        public async Task GuardarAsync()
        {
            var te = new TransitoEspecial
            {
                RazonSocial = _view.RazonSocial,
                Cuit = _view.Cuit,
                Apellido = _view.Apellido,
                Nombre = _view.Nombre,
                Documento = _view.Documento,
                Licencia = _view.Licencia,
                Art = _view.Art,
                Seguro = _view.Seguro,
                Tractor = _view.Tractor,
                Semi = _view.Semi,
                Zona = _view.Zona,
                Activo = true
            };

            if (!await ValidarAsync(te))
                return;

            int nroControl = await _guardiaRepositorio.ObtenerProximoNumeroControlAsync(_idPosta);
            int idPoc = await _guardiaRepositorio.RegistrarIngresoTransitoEspecialAsync(te, _idPosta, _Fecha, _sesionService.IdUsuario, nroControl);
            _view.MostrarMensaje("Ingreso de Tránsito Especial registrado correctamente.");
            ReporteIngresoTe? reporte = await _reporteConsumoTeOtros.ObtenerReporteTeOtros(idPoc, _Fecha, te, nroControl);
            await GenerarPocTe(reporte);
            _view.Close();
        }

        public async Task GenerarPocTe(ReporteIngresoTe reporte)
        {
            await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                }, true);
        }
    }
}