using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Views.Configuraciones.AbmLocaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Presenters
{
    public class MenuLocacionesPresenter
    {
        private IMenuLocacionesView _view;
        private readonly ILocacionRepositorio _locacionRepositorio;
        private readonly IServiceProvider _serviceProvider;

        public MenuLocacionesPresenter(ILocacionRepositorio locacionRepositorio,IServiceProvider serviceProvider)
        {
            _locacionRepositorio = locacionRepositorio;
            _serviceProvider = serviceProvider;
        }

        public void SetView(IMenuLocacionesView view)
        {
            _view = view;
        }

        public async Task InicializarAsync()
        {
            await CargarLocacionesAsync(); 
            // No necesitas ConfigureAwait(false) aquí
        }

        public async Task CargarLocacionesAsync()
        {
            await ManejarErroresAsync(async () =>
            {
                var locaciones = await _locacionRepositorio.ObtenerTodasAsync();
                _view.CargarLocaciones(locaciones);  // Actualizamos la UI, no usar ConfigureAwait(false)
            }, "Error al cargar locaciones.");
        }

        public async Task BuscarLocacionesAsync(string criterio)
        {
            await ManejarErroresAsync(async () =>
            {
                var locaciones = await _locacionRepositorio.BuscarPorCriterioAsync(criterio);
                _view.CargarLocaciones(locaciones);  // No usar ConfigureAwait(false)
            }, "Error al buscar locaciones.");
        }

        public async Task EliminarLocacionAsync(int idLocacion)
        {
            var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar esta locación?");
            if (confirmacion == DialogResult.Yes)
            {
                await ManejarErroresAsync(async () =>
                {
                    await _locacionRepositorio.EliminarAsync(idLocacion);  // No usar ConfigureAwait(false)
                    _view.MostrarMensaje("Locación eliminada correctamente.");
                    await CargarLocacionesAsync();  // No usar ConfigureAwait(false)
                }, "Error al eliminar la locación.");
            }
        }

        private async Task ManejarErroresAsync(Func<Task> accion, string mensajeError)
        {
            try
            {
                await accion();
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"{mensajeError} Detalles: {ex.Message}");
            }
        }



        public async void EditarLocacion(int idLocacion)
        {
            var agregarEditarLocacionForm = _serviceProvider.GetService<AgregarEditarLocacion>();

            // Inicializamos el presenter con la locación seleccionada
            await agregarEditarLocacionForm._presenter.InicializarAsync(idLocacion);

            // Mostrar el formulario después de la inicialización
            agregarEditarLocacionForm.ShowDialog();

            // Refrescar las locaciones después de cerrar el formulario
            await CargarLocacionesAsync();
        }

        public async  void AgregarLocacion()
        {
            var agregarEditarLocacionForm = _serviceProvider.GetService<AgregarEditarLocacion>();

            // Inicializamos sin pasar un id, ya que es una nueva locación
            await agregarEditarLocacionForm._presenter.InicializarAsync(null);

            // Mostrar el formulario
            agregarEditarLocacionForm.ShowDialog();

            // Refrescar las locaciones después de cerrar el formulario
            await CargarLocacionesAsync();
        }

    }
}
