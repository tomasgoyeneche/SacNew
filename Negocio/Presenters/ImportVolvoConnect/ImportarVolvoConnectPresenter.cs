using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Processor;
using SacNew.Views.GestionFlota.Postas.DatosVolvo;

namespace GestionFlota.Presenters
{
    public class ImportarVolvoConnectPresenter : BasePresenter<IImportarVolvoConnectView>
    {
        private readonly IPeriodoRepositorio _periodoRepositorio;
        private readonly IVolvoConnectProcessor _volvoConnectProcessor;
        private readonly IImportVolvoConnectRepositorio _volvoConnectRepositorio;

        public ImportarVolvoConnectPresenter(
            IPeriodoRepositorio periodoRepositorio,
            IVolvoConnectProcessor volvoConnectProcessor,
            IImportVolvoConnectRepositorio volvoConnectRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _periodoRepositorio = periodoRepositorio;
            _volvoConnectProcessor = volvoConnectProcessor;
            _volvoConnectRepositorio = volvoConnectRepositorio;
        }

        public async Task CargarPeriodosAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var periodos = await _periodoRepositorio.ObtenerPeriodosActivosAsync();
                _view.CargarPeriodos(periodos);
            });
        }

        public async Task ImportarVolvoAsync(string filePath)
        {
            if (_view.PeriodoSeleccionado == null)
            {
                _view.MostrarMensaje("Debe seleccionar un período.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                var idPeriodo = _view.PeriodoSeleccionado.IdPeriodo;

                if (await _volvoConnectRepositorio.ExistenDatosParaPeriodoAsync(idPeriodo))
                {
                    _view.MostrarMensaje("Ya existen consumos para este período.");
                    return;
                }

                var consumos = await _volvoConnectProcessor.ImportarDesdeExcelAsync(filePath, idPeriodo);
                _view.MostrarDatos(consumos);
                _view.MostrarMensaje("Importación completada.");
            });
        }

        public async Task GuardarConsumosAsync()
        {
            var consumos = _view.ObtenerDatos();

            if (!consumos.Any())
            {
                _view.MostrarMensaje("No hay datos para guardar.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                foreach (var consumo in consumos)
                {
                    await _volvoConnectRepositorio.AgregarImportacionAsync(consumo);
                }
                _view.MostrarMensaje("Consumos guardados correctamente.");
            });
        }

        public async Task ExportarConsumosAExcelAsync(string filePath)
        {
            var consumos = _view.ObtenerDatos();

            if (!consumos.Any())
            {
                _view.MostrarMensaje("No hay consumos para exportar.");
                return;
            }

            await _volvoConnectProcessor.ExportarAExcelAsync(consumos, filePath);
            _view.MostrarMensaje("Exportación completada.");
        }
    }
}