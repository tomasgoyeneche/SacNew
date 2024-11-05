using SacNew.Processor;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.ImportConsumos;

namespace SacNew.Presenters
{
    public class ImportDatosMvPresenter : BasePresenter<ImportaDatosMvView>
    {
        private readonly IPeriodoRepositorio _periodoRepositorio;
        private readonly IImportacionMercadoVictoriaProcessor _importacionProcessor;
        private readonly IImportMercadoVictoriaRepositorio _consumoMvRepositorio;

        public ImportDatosMvPresenter(
            IImportMercadoVictoriaRepositorio consumoMvRepositorio,

            IImportacionMercadoVictoriaProcessor importacionProcessor,
            IPeriodoRepositorio periodoRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _importacionProcessor = importacionProcessor;
            _periodoRepositorio = periodoRepositorio;
            _consumoMvRepositorio = consumoMvRepositorio;
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

            var consumosExistentes = await _consumoMvRepositorio.ExistenDatosParaPeriodoAsync(idPeriodo);
            if (consumosExistentes)
            {
                _view.MostrarMensaje("Ya existen consumos para este período. No se puede importar nuevamente.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                var consumos = await _importacionProcessor.ImportarDesdeExcelAsync(filePath, idPeriodo);

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
                    await _consumoMvRepositorio.AgregarConsumoAsync(consumo);
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