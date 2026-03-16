using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views.Postas.Informes.ConsultarConsumos;
using Shared.Models;

namespace GestionFlota.Presenters.Informes
{
    public class ResultadosConsumoPresenter : BasePresenter<IResultadosConsumoView>
    {
        private readonly IExcelService _excelService;
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IConsumoGasoilRepositorio _consumoGasoilRepositorio;
        private readonly IConsumoOtrosRepositorio _consumoOtrosRepositorio;



        public ResultadosConsumoPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IExcelService excelService,
            IConceptoRepositorio conceptoRepositorio,
            IConsumoGasoilRepositorio consumoGasoilRepositorio,
            IConsumoOtrosRepositorio consumoOtrosRepositorio)
            : base(sesionService, navigationService)
        {
            _excelService = excelService;
            _conceptoRepositorio = conceptoRepositorio;
            _consumoGasoilRepositorio = consumoGasoilRepositorio;
            _consumoOtrosRepositorio = consumoOtrosRepositorio;
        }

        public async Task ExportarAExcelAsync()
        {
            try
            {
                var resultados = _view.ObtenerResultados();
                if (resultados == null || !resultados.Any())
                {
                    _view.MostrarMensaje("No hay resultados para exportar.");
                    return;
                }

                var visibles = resultados.Select(r => new
                {
                    r.NumeroPoc,
                    r.Chofer_Nombre,
                    r.Chofer_Documento,
                    r.Codigo_Posta,
                    r.Empresa_Nombre,
                    r.Empresa_Cuit,
                    r.Tractor_Patente,
                    r.Semi_Patente,
                    r.Concepto_Codigo,
                    r.Odometro,
                    r.Comentario,
                    r.FechaCreacion,
                    r.FechaCierre,
                    r.Usuario,
                    r.NumeroVale,
                    r.LitrosAutorizados,
                    r.LitrosCargados,
                    r.Observaciones,
                    r.Dolar,
                    r.PrecioTotal,
                    r.FechaCarga,
                    r.Estado
                }).ToList();

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.Title = "Guardar archivo de resultados";
                    sfd.FileName = "Informe_Consumos.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        await _excelService.ExportarAExcelAsync(visibles, sfd.FileName, "Consumos");
                        _view.MostrarMensaje("Exportación exitosa.");
                    }
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al exportar: {ex.Message}");
            }
        }

        public void CalcularYMostrarTotales(List<InformeConsumoPocDto> resultados)
        {
            var totales = resultados
                .GroupBy(x => x.Concepto_Codigo)
                .Select(g => new TotalConsumoDto
                {
                    Concepto = g.Key,
                    PrecioTotal = g.Sum(x => x.PrecioTotal),
                    Cantidad = g.Sum(x => x.LitrosCargados),
                    Tickets = g.Count()
                })
                .OrderBy(x => x.Concepto)
                .ToList();

            _view.MostrarTotales(totales);
        }

        public async Task ValorizaYCuentaConsumos()
        {
            List<InformeConsumoPocDto> consumos = _view.ObtenerResultados();
            var (cantidad, clavesActualizadas) =
         await ValorizarConsumosAsync(consumos);

            _view.MostrarMensaje($"{cantidad} registros fueron valorizados.");

            _view.MarcarRegistrosValorizados(clavesActualizadas);
        }



        public async Task<(int cantidad, List<string> clavesActualizadas)>
        ValorizarConsumosAsync(List<InformeConsumoPocDto> resultados)
        {
            int contador = 0;
            var clavesActualizadas = new List<string>();

            foreach (var item in resultados)
            {
                Concepto concepto = await _conceptoRepositorio.ObtenerPorIdAsync(item.IdConsumo);
                if (concepto == null)
                    continue;

                if (concepto.PrecioActual == 0)
                    continue;

                if (item.FechaCarga.Date < concepto.Vigencia.Date)
                    continue;

                decimal nuevoTotal = item.LitrosCargados * concepto.PrecioActual;

                string clave = item.Tipo_Consumo == "GASOIL"
                   ? $"G_{item.IdRegistro}"
                   : $"O_{item.IdRegistro}";

                if (item.Tipo_Consumo == "GASOIL")
                {
                    var consumo = await _consumoGasoilRepositorio.ObtenerPorIdAsync(item.IdRegistro);
                    if (consumo == null)
                        continue;

                    if (consumo.PrecioTotal == nuevoTotal)
                        continue;

                    var mensaje =
                    $"¿Desea valorizar el consumo?\n\n" +
                    $"Concepto: {item.Concepto_Codigo}\n" +
                    $"Remito/Vale: {item.NumeroVale}\n" +
                    $"Precio actual: {consumo.PrecioTotal:N2}\n" +
                    $"Precio unidad: {concepto.PrecioActual:N2}\n" +
                    $"Nuevo precio: {nuevoTotal:N2}";

                    if (!_view.ConfirmarValorizacion(mensaje))
                        continue;

                    consumo.PrecioTotal = nuevoTotal;
                    await _consumoGasoilRepositorio.ActualizarConsumoAsync(consumo);
                    item.PrecioTotal = nuevoTotal;

                    contador++;
                    clavesActualizadas.Add(clave);
                }
                else if (item.Tipo_Consumo == "OTROS")
                {
                    var consumoOtros = await _consumoOtrosRepositorio.ObtenerPorIdAsync(item.IdRegistro);
                    if (consumoOtros == null)
                        continue;

                    if (consumoOtros.ImporteTotal == nuevoTotal)
                        continue;

                    var mensaje =
                        $"¿Desea valorizar el consumo?\n\n" +
                        $"Concepto: {item.Concepto_Codigo}\n" +
                        $"Remito/Vale: {item.NumeroVale}\n" +
                        $"Precio actual: {consumoOtros.ImporteTotal:N2}\n" +
                        $"Precio unidad: {concepto.PrecioActual:N2}\n" +
                        $"Nuevo precio: {nuevoTotal:N2}";

                    if (!_view.ConfirmarValorizacion(mensaje))
                        continue;

                    consumoOtros.ImporteTotal = nuevoTotal;
                    await _consumoOtrosRepositorio.ActualizarConsumoAsync(consumoOtros);
                    item.PrecioTotal = nuevoTotal;

                    contador++;
                    clavesActualizadas.Add(clave);
                }
            }

            return (contador, clavesActualizadas);
        }
    }
}