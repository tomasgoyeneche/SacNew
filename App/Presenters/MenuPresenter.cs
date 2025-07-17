using App.Views;
using Core.Base;
using Core.Services;
using DevExpress.XtraSplashScreen;
using GestionDocumental.Views;
using GestionFlota.Views;
using GestionFlota.Views.Alertas;
using GestionOperativa;
using GestionOperativa.Views;
using Servicios;

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

        public async void AbrirGuardia(string tipoPermiso, int idPosta)
        {
            await AbrirFormularioConPermisosAsync<GuardiaForm>(tipoPermiso, async form =>
            {
                await form._presenter.InicializarAsync(idPosta);
            });
        }

        public async void AbrirAdministracion(string tipoPermiso, int idPosta)
        {
            await AbrirFormularioConPermisosAsync<AdministracionForm>(tipoPermiso, async form =>
            {
                await form._presenter.InicializarAsync(idPosta);
            });
        }

        public async void AbrirVaporizados(string tipoPermiso)
        {
            await AbrirFormularioConPermisosAsync<MenuVaporizados>(tipoPermiso, async form =>
            {
                await form._presenter.CargarVaporizadosAsync(_sesionService.IdPosta);
            });
        }

        public async void AbrirRuteo(string tipoPermiso)
        {
            SplashScreenManager.ShowDefaultWaitForm("Cargando", "Por favor espere...");

            try
            {
                await AbrirFormularioConPermisosAsync<RuteoForm>(tipoPermiso, async form =>
                {
                    await form._presenter.InicializarAsync();
                }, false);
            }
            finally
            {
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        public async void AbrirDisponibilidad(string tipoPermiso)
        {
            await AbrirFormularioConPermisosAsync<DisponibilidadForm>(tipoPermiso, async form =>
            {
                await form._presenter.InicializarAsync();
            }, false);
        }

        public async void AbrirCupeo(string tipoPermiso)
        {
            await AbrirFormularioConPermisosAsync<CupeoForm>(tipoPermiso, async form =>
            {
                await form._presenter.InicializarAsync();
            }, false);
        }

        public async void AbrirViajesYRemitos(string tipoPermiso)
        {
            await AbrirFormularioConPermisosAsync<ViajesConsolidadosForm>(tipoPermiso, async form =>
            {
                await form._presenter.InicializarAsync();
            });
        }
    }
}