using Core.Base;
using Core.Repositories;
using Core.Services;
using Microsoft.Win32;
using Servicios.Views.Mantenimiento;
using Servicios.Views.Mantenimientos;
using Shared.Models;

namespace Servicios.Presenters
{
    public class ListadoMantenimientosPresenter : BasePresenter<IListadoMantenimientosView>
    {
        private readonly IMantenimientoRepositorio _movimientoRepositorio;
        private readonly IMantenimientoTareaRepositorio _mantenimientoTareaRepositorio;
        private readonly IMantenimientoTareaArticuloRepositorio _mantenimientoTareaArticuloRepositorio;



        public ListadoMantenimientosPresenter(
            IMantenimientoRepositorio movimientoRepositorio,
            IMantenimientoTareaRepositorio mantenimientoTareaRepositorio,
            IMantenimientoTareaArticuloRepositorio mantenimientoTareaArticuloRepositorio,
            ISesionService sesionService,

            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _movimientoRepositorio = movimientoRepositorio;
            _mantenimientoTareaRepositorio = mantenimientoTareaRepositorio;
            _mantenimientoTareaArticuloRepositorio = mantenimientoTareaArticuloRepositorio;
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

        public async Task EliminarMantenimiento(int idMantenimiento)
        {
            await _movimientoRepositorio.EliminarAsync(idMantenimiento);
            foreach (MantenimientoTarea tareasAsociadas in await _mantenimientoTareaRepositorio.ObtenerPorMantenimientoAsync(idMantenimiento))
            {
                await _mantenimientoTareaRepositorio.EliminarAsync(tareasAsociadas.IdMantenimientoTarea);

                foreach (MantenimientoTareaArticulo manArticulo in await _mantenimientoTareaArticuloRepositorio.ObtenerPorTareaAsync(tareasAsociadas.IdTarea))
                {
                    await _mantenimientoTareaArticuloRepositorio.EliminarAsync(manArticulo.IdMantenimientoTareaArticulo);
                    // Aquí podrías agregar lógica para reestablecer el stock si es necesario
                }
            }

            _view.MostrarMensaje("Mantenimiento predefinido eliminado correctamente.");
            await InicializarAsync();
        }

    }
}