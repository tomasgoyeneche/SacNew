using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views;
using GestionFlota.Views.Alertas;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class AlertasPresenter : BasePresenter<IAlertasView>
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IAlertaRepositorio _alertaRepositorio;

        public AlertasPresenter(
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IAlertaRepositorio alertaRepositorio,
            ISesionService sesionService,
            INavigationService navigationService)
        : base(sesionService, navigationService)  // Aquí pasamos las dependencias a la clase base
        {
            _unidadRepositorio = unidadRepositorio;
            _choferRepositorio = choferRepositorio;
            _alertaRepositorio = alertaRepositorio;
        }

        public async Task CargarAlertasAsync()
        {
            List<AlertaDto> novedades = await _alertaRepositorio.ObtenerTodasLasAlertasDtoAsync();
            _view.MostrarAlertas(novedades);
        }

        public async Task EliminarAlertaAsync(int idAlerta)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar esta Alerta?");
                if (confirmacion != DialogResult.Yes) return;

                await _alertaRepositorio.EliminarAlertaAsync(idAlerta);

                _view.MostrarMensaje("Alerta eliminada correctamente.");
            }, async () => await CargarAlertasAsync());
        }

        public async Task EditarAlertaAsync(int idAlerta)
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarAlertaForm>(async form =>
                {
                    await form._presenter.InicializarAsync(idAlerta);
                });
            }, async () => await CargarAlertasAsync());
        }

        public async Task AgregarAlertaAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarAlertaForm>(async form =>
                {
                    await form._presenter.InicializarAsync(0);
                });
            }, async () => await CargarAlertasAsync());
        }
    }
}