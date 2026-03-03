using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Processor;
using SacNew.Views.GestionFlota.Postas.YpfIngresaConsumos.ImportarConsumos;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class ImportarConsumosYpfPresenter : BasePresenter<IImportarConsumosYpfView>
    {
        private readonly IPeriodoRepositorio _periodoRepositorio;
        private readonly IImportacionYpfProcessor _importacionProcessor;
        private readonly IImportConsumoYpfRepositorio _consumoYpfRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;

        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IConceptoRepositorio _conceptoRepositorio;

        public ImportarConsumosYpfPresenter(
            IImportConsumoYpfRepositorio consumoYpfRepositorio,
            IImportacionYpfProcessor importacionProcessor,
            IPeriodoRepositorio periodoRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IChoferRepositorio choferRepositorio,
            IConceptoRepositorio consumoRepositorio,
            ITractorRepositorio tractorRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _importacionProcessor = importacionProcessor;
            _periodoRepositorio = periodoRepositorio;
            _consumoYpfRepositorio = consumoYpfRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _tractorRepositorio = tractorRepositorio;
            _choferRepositorio = choferRepositorio;
            _conceptoRepositorio = consumoRepositorio;
        }

        public async Task ImportarConsumosAsync(string filePath)
        {
            int? idPeriodo = await ObtenerIdPeriodoAsync();
            if (idPeriodo == null)
            {
                _view.MostrarMensaje("Debe seleccionar un período válido.");
                return;
            }

            //var consumosExistentes = await _consumoYpfRepositorio.ExistenConsumosParaPeriodoAsync(idPeriodo.Value);
            //if (consumosExistentes)
            //{
            //    _view.MostrarMensaje("Ya existen consumos para este período.");
            //    return;
            //}

            await EjecutarConCargaAsync(async () =>
            {
                List<ImportConsumoYpfEnRuta> consumos = await _importacionProcessor.ImportarConsumosDesdeExcelAsync(filePath, idPeriodo.Value);
                var dtos = await ConvertirADtoAsync(consumos);
                _view.MostrarConsumos(dtos);
                _view.MostrarMensaje("Importación completada con éxito.");
            });
        }

        private async Task<List<ImportConsumoYpfEnRutaDto>> ConvertirADtoAsync(IEnumerable<ImportConsumoYpfEnRuta> consumos)
        {
            var listaDto = new List<ImportConsumoYpfEnRutaDto>();

            foreach (var c in consumos)
            {
                if (c.IdChofer == 73)
                    continue;

                var unidad = await _unidadRepositorio.ObtenerPorUnidadIdAsync(c.IdUnidad);
                var tractor = await _tractorRepositorio.ObtenerTractorPorIdAsync(unidad.IdTractor);
                var chofer = await _choferRepositorio.ObtenerPorIdAsync(c.IdChofer);
                var consumo = await _conceptoRepositorio.ObtenerPorIdAsync(c.IdConsumo);
                var periodo = await _periodoRepositorio.ObtenerPorIdAsync(c.IdPeriodo);

                listaDto.Add(new ImportConsumoYpfEnRutaDto
                {
                    IdImportConsumoYPFEnRuta = c.IdImportConsumoYPFEnRuta,
                    FechaHora = c.FechaHora,
                    Localidad = c.Localidad,
                    Tarjeta = c.Tarjeta,
                    IdChofer = c.IdChofer,
                    NombreChofer = chofer?.Nombre + " " + chofer?.Apellido ?? "",
                    IdUnidad = c.IdUnidad,
                    PatenteTractor = tractor?.Patente ?? "",
                    Remito = c.Remito,
                    IdConsumo = c.IdConsumo,
                    Producto = consumo?.Descripcion ?? "",
                    Litros = c.Litros,
                    ImporteTotalYer = c.ImporteTotalYer,
                    ImporteSinImpuestos = c.ImporteSinImpuestos,
                    Factura = c.Factura,
                    IdPeriodo = c.IdPeriodo,
                    NombrePeriodo = periodo?.NombrePeriodo ?? "",
                    Chequeado = c.Chequeado
                });
            }

            return listaDto;
        }

        private List<ImportConsumoYpfEnRuta> ConvertirADominio(List<ImportConsumoYpfEnRutaDto> dtos)
        {
            return dtos.Select(x => new ImportConsumoYpfEnRuta
            {
                IdImportConsumoYPFEnRuta = x.IdImportConsumoYPFEnRuta,
                FechaHora = x.FechaHora,
                Localidad = x.Localidad,
                Tarjeta = x.Tarjeta,
                IdChofer = x.IdChofer,
                IdUnidad = x.IdUnidad,
                Remito = x.Remito,
                IdConsumo = x.IdConsumo,
                Litros = x.Litros,
                ImporteTotalYer = x.ImporteTotalYer,
                ImporteSinImpuestos = x.ImporteSinImpuestos,
                Factura = x.Factura,
                IdPeriodo = x.IdPeriodo,
                Chequeado = x.Chequeado
            }).ToList();
        }

        private async Task<int?> ObtenerIdPeriodoAsync()
        {
            var fecha = _view.PeriodoSeleccionado;
            var quincena = _view.QuincenaSeleccionada;
            return await _periodoRepositorio.ObtenerIdPeriodoPorMesAnioQuincenaAsync(fecha.Month, fecha.Year, quincena);
        }

        public async Task GuardarConsumosAsync()
        {
            var dtos = _view.ObtenerConsumos();
            if (!dtos.Any())
            {
                _view.MostrarMensaje("No hay consumos para guardar.");
                return;
            }

            var consumos = ConvertirADominio(dtos);

            await EjecutarConCargaAsync(async () =>
            {
                foreach (var c in consumos)
                {
                    if (c.IdImportConsumoYPFEnRuta == 0)
                    {
                        await _consumoYpfRepositorio.AgregarConsumoAsync(c);
                    }
                    else
                    {
                        await _consumoYpfRepositorio.ActualizarConsumoAsync(c);
                    }
                }

                _view.MostrarMensaje("Consumos guardados correctamente.");
            });
        }

        public async Task ActualizarChequeadoAsync(ImportConsumoYpfEnRutaDto dto)
        {
            var entidad = new ImportConsumoYpfEnRuta
            {
                IdImportConsumoYPFEnRuta = dto.IdImportConsumoYPFEnRuta,
                Chequeado = dto.Chequeado
            };

            await _consumoYpfRepositorio.ActualizarChequeadoAsync(entidad);
        }

        public async Task BuscarConsumosPorPeriodo()
        {
            int idPeriodo = await ObtenerIdPeriodoAsync() ?? 0;
            var consumos = await _consumoYpfRepositorio.ObtenerPorPeriodoAsync(idPeriodo);
            var dtos = await ConvertirADtoAsync(consumos);
            _view.MostrarConsumos(dtos);
        }

        public async Task ExportarConsumosAExcelAsync(string filePath)
        {
            var dtos = _view.ObtenerConsumos();
            var consumos = ConvertirADominio(dtos);

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