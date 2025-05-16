using App.Views;
using Core.Base;
using Core.Services;
using GestionDocumental.Views;
using GestionFlota.Views.Alertas;

namespace App.Presenters
{
    public class MenuPresenter : BasePresenter<IMenuView>
    {
        public MenuPresenter(
           ISesionService sesionService,
           INavigationService navigationService)
           : base(sesionService, navigationService)
        {
        }

        public void Inicializar()
        {
            _view.MostrarNombreUsuario(_sesionService.NombreCompleto);
            _view.MostrarDiaDeHoy(DateTime.Now.ToString("dd/MM/yyyy"));
        }

        public async void AbrirNovedades(string tipoNovedad, string tipoPermiso)
        {
            await AbrirFormularioConPermisosAsync<NovedadesForm>(tipoPermiso, async form =>
            {
                await form._presenter.CargarNovedadesAsync(false, tipoNovedad);
            });
        }

        public async void AbrirAlertas(string tipoPermiso)
        {
            await AbrirFormularioConPermisosAsync<AlertasForm>(tipoPermiso, async form =>
            {
                await form._presenter.CargarAlertasAsync();
            });
        }
    }
}