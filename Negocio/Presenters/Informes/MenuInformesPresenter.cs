using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views.Postas.Informes;
using GestionFlota.Views.Postas.Informes.ConsultarConsumos;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task BuscarConsumosAsync(DateTime fecha)
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<InformeConsumoPocDto> resultados = await _consumoOtrosRepositorio.ObtenerPorFechaCargaAsync(fecha, _sesionService.IdPosta);

                await AbrirFormularioAsync<MostrarResultadosConsumosForm>(async form =>
                {
                    form.MostrarResultados(resultados);
                });
            });
        }
    }
}
