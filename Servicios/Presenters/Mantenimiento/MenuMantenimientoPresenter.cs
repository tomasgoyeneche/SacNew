using Core.Base;
using Core.Services;
using Servicios.Views;
using Servicios.Views.Mantenimiento;
using Servicios.Views.Mantenimientos;

namespace Servicios.Presenters.Mantenimiento
{
    public class MenuMantenimientoPresenter : BasePresenter<IMenuMantenimientoView>
    {
        public MenuMantenimientoPresenter(
           ISesionService sesionService,
           INavigationService navigationService)
           : base(sesionService, navigationService)
        {
        }

        public async void AbrirArticulos()
        {
            await AbrirFormularioAsync<MenuArticuloForm>(async form =>
            {
                await form._presenter.CargarArticulosAsync();
            });
        }

        public async void AbrirProveedores()
        {
            await AbrirFormularioAsync<MenuArticuloProveedorForm>(async form =>
            {
                await form._presenter.CargarProveedoresAsync();
            });
        }

        public async void AbrirMovimientosStock()
        {
            await AbrirFormularioAsync<MovimientoStockForm>(async form =>
            {
                await form._presenter.InicializarAsync();
            });
        }

        public async void AbrirMenuMantenimientos()
        {
            await AbrirFormularioAsync<ListadoMantenimientosForm>(async form =>
            {
                await form._presenter.InicializarAsync();
            });
        }

        public async void AbrirOrdenesActuales()
        {
            await AbrirFormularioAsync<ListadoOrdenTrabajoForm>(async form =>
            {
                await form._presenter.InicializarAsync("Todos");
            });
        }

        public async void AbrirOrdenesTerminadas()
        {
            await AbrirFormularioAsync<ListadoOrdenTrabajoForm>(async form =>
            {
                await form._presenter.InicializarAsync("Terminadas");
            });
        }

        public async void AbrirProximosMantenimientos()
        {
            await AbrirFormularioAsync<MuestraDatosGenericoForm>(async form =>
            {
                await form._presenter.InicializarAsync("OrdenTrabajoProximo");
            });
        }

        public void AbrirInformeMantenimiento()
        { /* abrir form correspondiente */ }

        public void AbrirInformeStock()
        { /* ... */ }

        public void AbrirInformeTrabajos()
        { /* ... */ }


        public async void AbrirMovimientosArticulo()
        {
            await AbrirFormularioAsync<MuestraDatosGenericoForm>(async form =>
            {
                await form._presenter.InicializarAsync("ArticuloMovimientoHistorico", _sesionService.IdPosta);
            });

        }

        public async void AbrirArticulosExistencia()
        {
            await AbrirFormularioAsync<MuestraDatosGenericoForm>(async form =>
            {
                await form._presenter.InicializarAsync("ArticuloStockDeposito", _sesionService.IdPosta);
            });
        }

        public async void AbrirArticulosStockCritico()
        {
            await AbrirFormularioAsync<MuestraDatosGenericoForm>(async form =>
            {
                await form._presenter.InicializarAsync("ArticuloStockCritico", _sesionService.IdPosta);
            });
        }
    }
}