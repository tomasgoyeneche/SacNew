using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Repositories;
using SacNew.Views.Configuraciones.AbmLocaciones;

namespace SacNew.Presenters
{
    public class MenuLocacionesPresenter
    {
        private IMenuLocacionesView _view;
        private readonly ILocacionRepositorio _locacionRepositorio;
        private readonly IServiceProvider _serviceProvider;

        public MenuLocacionesPresenter(ILocacionRepositorio locacionRepositorio, IServiceProvider serviceProvider)
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
                _view.CargarLocaciones(locaciones);
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
            await AbrirFormularioLocacionAsync(idLocacion);
        }

        private async Task AbrirFormularioLocacionAsync(int? idLocacion)
        {
            var agregarEditarLocacionForm = _serviceProvider.GetService<AgregarEditarLocacion>();
            await agregarEditarLocacionForm._presenter.InicializarAsync(idLocacion);
            agregarEditarLocacionForm.ShowDialog();
            await CargarLocacionesAsync(); // Refrescar la lista al cerrar el formulario
        }

        public async void AgregarLocacion()
        {
            await AbrirFormularioLocacionAsync(null);
        }
    }
}