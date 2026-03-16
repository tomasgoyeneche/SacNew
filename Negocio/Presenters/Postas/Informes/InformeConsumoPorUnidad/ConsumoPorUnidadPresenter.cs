using Core.Base;
using Core.Repositories;
using Core.Services;
using SacNew.Views.GestionFlota.Postas.Informes;

namespace GestionFlota.Presenters
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

        private async Task<int?> ObtenerIdPeriodoDesde()
        {
            var fecha = _view.PeriodoDesde;
            var quincena = _view.QuincenaHasta;
            return await _periodoRepositorio.ObtenerIdPeriodoPorMesAnioQuincenaAsync(fecha.Month, fecha.Year, quincena);
        }

        private async Task<int?> ObtenerIdPeriodoHasta()
        {
            var fecha = _view.PeriodoHasta;
            var quincena = _view.QuincenaHasta;
            return await _periodoRepositorio.ObtenerIdPeriodoPorMesAnioQuincenaAsync(fecha.Month, fecha.Year, quincena);
        }

        public async Task BuscarConsumosAsync()
        {
            int idPeriodoDesde = await ObtenerIdPeriodoDesde() ?? 0;
            int idPeriodoHasta = await ObtenerIdPeriodoHasta() ?? 0;

            if (idPeriodoHasta == 0 || idPeriodoDesde == 0)
            {
                _view.MostrarMensaje("Debe seleccionar un periodo.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                var consumos = await _consumoUnidadRepositorio.ObtenerConsumosPorPeriodoAsync(idPeriodoDesde, idPeriodoHasta);

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

            int idPeriodoDesde = await ObtenerIdPeriodoDesde() ?? 0;
            int idPeriodoHasta = await ObtenerIdPeriodoHasta() ?? 0;

            if(idPeriodoDesde == idPeriodoHasta)
            {
                await EjecutarConCargaAsync(async () =>
                {
                    foreach (var consumo in consumos)
                    {
                        consumo.idPeriodo = idPeriodoDesde;
                        await _consumoUnidadRepositorio.GuardarConsumoAsync(consumo);
                    }
                    _view.MostrarMensaje("Consumos guardados correctamente.");
                });
            }
            else
            {
                _view.MostrarMensaje("Tiene mas de un periodo seleccionado, solo puede guardar de 1 periodo");
                return;
            }

           
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