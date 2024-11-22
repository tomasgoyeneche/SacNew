using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.Informes;

namespace SacNew.Presenters
{
    public class ConsumoPorUnidadPresenter : BasePresenter<IConsumoUnidadView>
    {
        private readonly IPeriodoRepositorio _periodoRepositorio;
        private readonly IConsumoUnidadRepositorio _consumoUnidadRepositorio;
        private readonly IExcelService _excelService;

        public ConsumoPorUnidadPresenter(
            IPeriodoRepositorio periodoRepositorio,
            IConsumoUnidadRepositorio consumoUnidadRepositorio,
            IExcelService excelService,
            ISesionService sesionService,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _periodoRepositorio = periodoRepositorio;
            _consumoUnidadRepositorio = consumoUnidadRepositorio;
            _excelService = excelService;
        }

        public async Task CargarPeriodosAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var periodos = await _periodoRepositorio.ObtenerPeriodosActivosAsync();
                _view.CargarPeriodos(periodos);
            });
        }

        public async Task BuscarConsumosAsync()
        {
            var idPeriodo = _view.IdPeriodoSeleccionado;
            if (idPeriodo == 0)
            {
                _view.MostrarMensaje("Debe seleccionar un periodo.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                var consumos = await _consumoUnidadRepositorio.ObtenerConsumosPorPeriodoAsync(idPeriodo);
                _view.MostrarConsumos(consumos);
                _view.MostrarMensaje("Búsqueda completada.");
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
                    consumo.idPeriodo = _view.IdPeriodoSeleccionado;
                    await _consumoUnidadRepositorio.GuardarConsumoAsync(consumo);
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

            await _excelService.ExportarAExcelAsync(consumos, filePath);
            _view.MostrarMensaje("Exportación completada con éxito.");
        }
    }
}