using Core.Base;
using Core.Services;
using GestionFlota.Views.Postas.Informes.ConsultarConsumos;
using Shared.Models;

namespace GestionFlota.Presenters.Informes
{
    public class ResultadosConsumoPresenter : BasePresenter<IResultadosConsumoView>
    {
        private readonly IExcelService _excelService;

        public ResultadosConsumoPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IExcelService excelService)
            : base(sesionService, navigationService)
        {
            _excelService = excelService;
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
    }
}