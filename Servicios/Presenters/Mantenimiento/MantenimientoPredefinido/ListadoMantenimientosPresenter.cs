using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views;
using Servicios.Views.Mantenimiento;
using Servicios.Views.Mantenimientos;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Presenters
{
    public class ListadoMantenimientosPresenter : BasePresenter<IListadoMantenimientosView>
    {
        private readonly IMantenimientoRepositorio _movimientoRepositorio;

        public ListadoMantenimientosPresenter(
            IMantenimientoRepositorio movimientoRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _movimientoRepositorio = movimientoRepositorio;
        }

        public async Task<int> CrearMantenimientoAsync(int idTipoMantenimiento)
        {
            var mantenimiento = new Shared.Models.Mantenimiento
            {
                Nombre = "",
                IdTipoMantenimiento = idTipoMantenimiento,
                AplicaA = "Unidad",
                Activo = true,
                Descripcion = ""
            };

            return await _movimientoRepositorio.AgregarAsync(mantenimiento);
        }

        public async Task AbrirEdicionMantenimientoAsync(int idMantenimiento)
        {
            await AbrirFormularioAsync<MenuCrearMantenimientoForm>(async form =>
            {
                await form._presenter.InicializarAsync("MantenimientoPredefinido", idMantenimiento);
            });

            // refrescar lista después de cerrar
            await InicializarAsync();
        }


        public async Task InicializarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var movimientos = await _movimientoRepositorio.ObtenerTodosAsync();
                _view.MostrarMantenimientos(movimientos);
            });
        }
    }
}
