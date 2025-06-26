using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class RuteoPresenter : BasePresenter<IRuteoView>
    {
        private readonly IProgramaRepositorio _programaRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IAlertaRepositorio _alertaRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IPOCRepositorio _pocRepositorio;
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly IPeriodoRepositorio _periodoRepositorio;

        private readonly IVaporizadoRepositorio _vaporizadoRepositorio;

        public RuteoPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IProgramaRepositorio programaRepositorio,
            IAlertaRepositorio alertaRepositorio,
            INominaRepositorio nominaRepositorio,
            IPostaRepositorio postaRepositorio,
            IPeriodoRepositorio periodoRepositorio,
            IVaporizadoRepositorio vaporizadoRepositorio,
            IUnidadRepositorio unidadRepositorio)
            : base(sesionService, navigationService)
        {
            _programaRepositorio = programaRepositorio;
            _alertaRepositorio = alertaRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _postaRepositorio = postaRepositorio;
            _periodoRepositorio = periodoRepositorio;
            _vaporizadoRepositorio = vaporizadoRepositorio;
        }

        public async Task InicializarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<Shared.Models.Ruteo> ruteos = await _programaRepositorio.ObtenerRuteoAsync();
                List<RuteoResumen> resumen = await _programaRepositorio.ObtenerResumenAsync();

                var cargados = ruteos
                   .Where(r => r.Estado == "En Viaje" || r.Estado == "Descargando")
                   .ToList();

                var vacios = ruteos
                   .Where(r => r.Estado != "En Viaje" && r.Estado != "Descargando")
                   .ToList();

                _view.MostrarRuteoCargados(cargados);
                _view.MostrarRuteoVacios(vacios);
                _view.MostrarResumen(resumen);
            });
        }

        public async Task CargarVencimientosYAlertasAsync(Shared.Models.Ruteo ruteo)
        {
            List<VencimientosDto> vencimientos = new();
            List<AlertaDto> alertas = new();
            DateTime fechaLimite = DateTime.Now.AddDays(20);

            Nomina? nomina = await _nominaRepositorio.ObtenerPorIdAsync(ruteo.IdNomina);
            List<VencimientosDto> vencNomina = await _nominaRepositorio.ObtenerVencimientosPorNominaAsync(nomina);
            vencimientos.AddRange(vencNomina.Where(v => v.FechaVencimiento <= fechaLimite));

            List<AlertaDto> alertasNomina = await _alertaRepositorio.ObtenerAlertasPorIdNominaAsync(ruteo.IdNomina);
            alertas.AddRange(alertasNomina);

            var historial = await _nominaRepositorio.ObtenerHistorialPorNomina(ruteo.IdNomina);

            _view.MostrarHistorial(historial);
            _view.MostrarVencimientos(vencimientos.OrderBy(v => v.FechaVencimiento).ToList());
            _view.MostrarAlertas(alertas);
        }

        public async Task AbrirEdicionDePrograma(Shared.Models.Ruteo ruteo)
        {
            await AbrirFormularioAsync<EditarProgramaForm>(async form =>
            {
                await form._presenter.InicializarAsync(ruteo);
            });
            await InicializarAsync();
        }
    }
}