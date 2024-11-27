using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.ManualConsumos.IngresarConsumo;

namespace SacNew.Presenters
{
    public class IngresaGasoilPresenter : BasePresenter<IIngresaGasoilView>
    {
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IConsumoGasoilRepositorio _consumoGasoilRepositorio;
        private int _idPoc;
        EmpresaCredito _amount;    

        public IngresaGasoilPresenter(
            IConceptoRepositorio conceptoRepositorio,
            IConsumoGasoilRepositorio consumoGasoilRepositorio,
            ISesionService sesionService,
         INavigationService navigationService
            ): base(sesionService, navigationService)
        {
            _conceptoRepositorio = conceptoRepositorio;
            _consumoGasoilRepositorio = consumoGasoilRepositorio;
        }


        public async Task CargarDatosAsync(int idPoc, EmpresaCredito empresaCredito)
        {
            _idPoc = idPoc;
            _amount = empresaCredito;
        }

    }
}