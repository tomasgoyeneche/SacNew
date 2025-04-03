using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Processor;
using SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.ImportarConsumos;

namespace GestionFlota.Presenters
{
    public class ImportarConsumosYpfPresenter : BasePresenter<IImportarConsumosYpfView>
    {
        private readonly IPeriodoRepositorio _periodoRepositorio;
        private readonly IImportacionYpfProcessor _importacionProcessor;
        private readonly IImportConsumoYpfRepositorio _consumoYpfRepositorio;

        public ImportarConsumosYpfPresenter(
            IImportConsumoYpfRepositorio consumoYpfRepositorio,

            IImportacionYpfProcessor importacionProcessor,
            IPeriodoRepositorio periodoRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _importacionProcessor = importacionProcessor;
            _periodoRepositorio = periodoRepositorio;
            _consumoYpfRepositorio = consumoYpfRepositorio;
        }

        public async Task CargarPeriodosAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var periodos = await _periodoRepositorio.ObtenerPeriodosActivosAsync();
                _view.CargarPeriodos(periodos);
            });
        }

        public async Task ImportarConsumosAsync(string filePath)
        {
            if (_view.PeriodoSeleccionado == null)
            {
                _view.MostrarMensaje("Debe seleccionar un período.");
                return;
            }

            var idPeriodo = _view.PeriodoSeleccionado.IdPeriodo;

            var consumosExistentes = await _consumoYpfRepositorio.ExistenConsumosParaPeriodoAsync(idPeriodo);
            if (consumosExistentes)
            {
                _view.MostrarMensaje("Ya existen consumos para este período. No se puede importar nuevamente.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                var consumos = await _importacionProcessor.ImportarConsumosDesdeExcelAsync(filePath, idPeriodo);

                _view.MostrarConsumos(consumos);
                _view.MostrarMensaje("Importación completada con éxito.");
            });
        }

        public async Task GuardarConsumosAsync()
        {
            var consumos = _view.ObtenerConsumos();

            if (!consumos.Any())
            {
                _view.MostrarMensaje("No hay consumos para guardar.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                foreach (var consumo in consumos)
                {
                    await _consumoYpfRepositorio.AgregarConsumoAsync(consumo);
                }

                _view.MostrarMensaje("Consumos guardados correctamente.");
            });
        }

        public async Task ExportarConsumosAExcelAsync(string filePath)
        {
            var consumos = _view.ObtenerConsumos();

            if (!consumos.Any())
            {
                _view.MostrarMensaje("No hay consumos para exportar.");
                return;
            }

            await _importacionProcessor.ExportarConsumosAExcelAsync(consumos, filePath);
            _view.MostrarMensaje("Exportación a Excel completada.");
        }
    }
}