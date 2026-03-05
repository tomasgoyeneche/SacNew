using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Processor;
using SacNew.Views.GestionFlota.Postas.DatosVolvo;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class ImportarVolvoConnectPresenter : BasePresenter<IImportarVolvoConnectView>
    {
        private readonly IPeriodoRepositorio _periodoRepositorio;
        private readonly IVolvoConnectProcessor _volvoConnectProcessor;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;

        private readonly IImportVolvoConnectRepositorio _volvoConnectRepositorio;

        public ImportarVolvoConnectPresenter(
            IPeriodoRepositorio periodoRepositorio,
            IVolvoConnectProcessor volvoConnectProcessor,
            IUnidadRepositorio unidadRepositorio,
            ITractorRepositorio tractorRepositorio,
            IImportVolvoConnectRepositorio volvoConnectRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _periodoRepositorio = periodoRepositorio;
            _volvoConnectProcessor = volvoConnectProcessor;
            _volvoConnectRepositorio = volvoConnectRepositorio;
            _tractorRepositorio = tractorRepositorio;
            _unidadRepositorio = unidadRepositorio; 
        }

        private async Task<int?> ObtenerIdPeriodoAsync()
        {
            var fecha = _view.PeriodoSeleccionado;
            var quincena = _view.QuincenaSeleccionada;
            return await _periodoRepositorio.ObtenerIdPeriodoPorMesAnioQuincenaAsync(fecha.Month, fecha.Year, quincena);
        }

        public async Task ImportarVolvoAsync(string filePath)
        {
            int? idPeriodo = await ObtenerIdPeriodoAsync();
            if (idPeriodo == null)
            {
                _view.MostrarMensaje("Debe seleccionar un período válido.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                if (await _volvoConnectRepositorio.ExistenDatosParaPeriodoAsync(idPeriodo.Value))
                {
                    _view.MostrarMensaje("Ya existen datos para este período.");
                    return;
                }

                List<ImportVolvoConnect> consumos = await _volvoConnectProcessor.ImportarDesdeExcelAsync(filePath, idPeriodo.Value);
                List<ImportVolvoConnectDto> dtos = await ConvertirADtoAsync(consumos);
                _view.MostrarDatos(dtos);
                _view.MostrarMensaje("Importación completada.");
            });
        }

        private async Task<List<ImportVolvoConnectDto>> ConvertirADtoAsync(IEnumerable<ImportVolvoConnect> consumos)
        {
            var listaDto = new List<ImportVolvoConnectDto>();

            foreach (var c in consumos)
            {
                Unidad? unidad = await _unidadRepositorio.ObtenerPorUnidadIdAsync(c.IdUnidad);
                Tractor? tractor = await _tractorRepositorio.ObtenerTractorPorIdAsync(unidad.IdTractor);
                Periodo? periodo = await _periodoRepositorio.ObtenerPorIdAsync(c.IdPeriodo);

                listaDto.Add(new ImportVolvoConnectDto
                {
                    IdImportVolvoConnect = c.IdImportVolvoConnect,
                    IdUnidad = c.IdUnidad,
                    PatenteTractor = tractor.Patente,
                    Kilometros = c.Kilometros,
                    PromedioGasoilEnMarcha = c.PromedioGasoilEnMarcha,
                    GasoilEnMarcha = c.GasoilEnMarcha,  
                    PromedioGasoilEnConduccion = c.PromedioGasoilEnConduccion,
                    GasoilEnConduccion = c.GasoilEnConduccion,
                    IdPeriodo = c.IdPeriodo,
                    NombrePeriodo = periodo.NombrePeriodo
                });
            }

            return listaDto;
        }

        private List<ImportVolvoConnect> ConvertirADominio(List<ImportVolvoConnectDto> dtos)
        {
            return dtos.Select(x => new ImportVolvoConnect
            {
                IdImportVolvoConnect = x.IdImportVolvoConnect,
                 IdUnidad = x.IdUnidad,
                 Kilometros = x.Kilometros,
                 PromedioGasoilEnMarcha = x.PromedioGasoilEnMarcha,
                 GasoilEnMarcha = x.GasoilEnMarcha,
                 PromedioGasoilEnConduccion = x.PromedioGasoilEnConduccion,
                 GasoilEnConduccion = x.GasoilEnConduccion,
                IdPeriodo = x.IdPeriodo
            }).ToList();
        }

        public async Task BuscarConsumosPorPeriodo()
        {
            int idPeriodo = await ObtenerIdPeriodoAsync() ?? 0;
            List<ImportVolvoConnect> consumos = await _volvoConnectRepositorio.ObtenerPorPeriodoAsync(idPeriodo);
            var dtos = await ConvertirADtoAsync(consumos);
            _view.MostrarDatos(dtos);
        }


        public async Task GuardarConsumosAsync()
        {
            var dtos = _view.ObtenerDatos();
            if (!dtos.Any())
            {
                _view.MostrarMensaje("No hay consumos para guardar.");
                return;
            }

            var consumos = ConvertirADominio(dtos);

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
            var dtos = _view.ObtenerDatos();
            var consumos = ConvertirADominio(dtos);

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