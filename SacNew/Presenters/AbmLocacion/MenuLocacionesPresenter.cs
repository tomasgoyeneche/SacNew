using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.Configuraciones.AbmLocaciones;

namespace SacNew.Presenters
{
    public class MenuLocacionesPresenter : BasePresenter<IMenuLocacionesView>
    {
        private readonly ILocacionRepositorio _locacionRepositorio;

        public MenuLocacionesPresenter(
            ILocacionRepositorio locacionRepositorio,
            ISesionService sesionService,
            IServiceProvider serviceProvider
        ) : base(sesionService, serviceProvider)
        {
            _locacionRepositorio = locacionRepositorio;
        }

        public async Task InicializarAsync()
        {
            await CargarLocacionesAsync();
        }

        public async Task CargarLocacionesAsync()
        {
            await EjecutarConCargaAsync(
                () => _locacionRepositorio.ObtenerTodasAsync(),
                _view.CargarLocaciones
            );
        }

        public async Task BuscarLocacionesAsync(string criterio)
        {
            await EjecutarConCargaAsync(
                () => _locacionRepositorio.BuscarPorCriterioAsync(criterio),
                _view.CargarLocaciones
            );
        }

        public async Task EliminarLocacionAsync(int idLocacion)
        {
            var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar esta locación?");
            if (confirmacion == DialogResult.Yes)
            {
                await EjecutarConCargaAsync(async () =>
                {
                    await _locacionRepositorio.EliminarAsync(idLocacion);
                    _view.MostrarMensaje("Locación eliminada correctamente.");
                }, CargarLocacionesAsync);
            }
        }

        public async void EditarLocacion(int idLocacion)
        {
            await AbrirFormularioAsync<AgregarEditarLocacion>(async form =>
            {
                await form._presenter.InicializarAsync(idLocacion);
            });
            await CargarLocacionesAsync(); // Refrescar al cerrar el formulario
        }

        public async void AgregarLocacion()
        {
            await AbrirFormularioAsync<AgregarEditarLocacion>(async form =>
            {
                await form._presenter.InicializarAsync(null);
            });
            await CargarLocacionesAsync(); // Refrescar al cerrar el formulario
        }
    }
}