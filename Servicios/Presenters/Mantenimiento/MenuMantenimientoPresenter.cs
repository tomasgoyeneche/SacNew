using Core.Base;
using Core.Services;
using GestionFlota.Views.Alertas;
using Servicios.Views;
using Servicios.Views.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



        public void AbrirInformeMantenimiento() { /* abrir form correspondiente */ }
        public void AbrirInformeStock() { /* ... */ }
        public void AbrirInformeTrabajos() { /* ... */ }

        public void AbrirHistorialArticulo() { /* ... */ }
        public void AbrirMovimientosArticulo() { /* ... */ }

        public void AbrirArticulosExistencia() { /* ... */ }
        public void AbrirArticulosStockCritico() { /* ... */ }



    }
}
