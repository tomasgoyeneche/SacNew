using Core.Base;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental;
using GestionOperativa.Views.AdministracionDocumental.Altas;

namespace GestionOperativa.Presenters
{
    public class MenuAdministracionDocumentalPresenter : BasePresenter<IMenuAdministracionDocumentalView>
    {
        public MenuAdministracionDocumentalPresenter(
           ISesionService sesionService,
           INavigationService navigationService)
           : base(sesionService, navigationService)
        {
        }

        public async Task CargarMenuEntidad(string numeroEntidad)
        {
            await AbrirFormularioAsync<MenuAltaForm>(async form =>
            {
                await form.CargarEntidades(numeroEntidad);
            });
        }
    }
}