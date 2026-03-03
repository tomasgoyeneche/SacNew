using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views.Postas.Informes;
using GestionFlota.Views.Postas.Informes.ConsultarConsumos;
using Shared.Models;

namespace GestionFlota.Presenters.Informes
{
    public class MenuInformesPresenter : BasePresenter<IMenuInformesView>
    {
        private readonly IConsumoOtrosRepositorio _consumoOtrosRepositorio;

        public MenuInformesPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IConsumoOtrosRepositorio consumoOtrosRepositorio
        ) : base(sesionService, navigationService)
        {
            _consumoOtrosRepositorio = consumoOtrosRepositorio;
        }

        public async Task ExportaTransoftConsumos(DateTime desde, DateTime hasta)
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<InformeConsumoPocDto> resultados = await _consumoOtrosRepositorio.BuscarConsumosPorFechaAsync(desde, hasta);

                await AbrirFormularioAsync<MostrarExportaConsumosForm>(async form =>
                {
                    form.MostrarResultados(resultados);
                });
            });
        }

        public async Task BuscarConsumosAsync(DateTime fecha)
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<InformeConsumoPocDto> resultados = await _consumoOtrosRepositorio.ObtenerPorFechaCargaAsync(fecha, _sesionService.IdPosta);

                await AbrirFormularioAsync<MostrarExportaConsumosForm>(async form =>
                {
                    form.MostrarResultados(resultados);
                });
            });
        }
    }
}