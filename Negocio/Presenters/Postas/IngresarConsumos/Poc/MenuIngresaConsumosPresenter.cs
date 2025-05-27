using Core.Base;
using Core.Repositories;
using Core.Services;
using SacNew.Interfaces;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo;

namespace GestionFlota.Presenters
{
    public class MenuIngresaConsumosPresenter : BasePresenter<IMenuIngresaConsumosView>
    {
        private readonly IPOCRepositorio _repositorioPOC;

        public MenuIngresaConsumosPresenter(
            IPOCRepositorio repositorioPOC,
            ISesionService sesionService,
            INavigationService navigationService)
        : base(sesionService, navigationService)  // Aquí pasamos las dependencias a la clase base
        {
            _repositorioPOC = repositorioPOC;
        }

        public async Task InicializarAsync()
        {
            await ManejarErroresAsync(async () =>
            {
                CargarPOCAsync();
            });
        }

        public async Task CargarPOCAsync()
        {
            await EjecutarConCargaAsync(
                () => _repositorioPOC.ObtenerTodosPorPostaAsync(_sesionService.IdPosta, "abierta"),
                _view.MostrarPOC
            );
        }

        public async Task BuscarPOCAsync(string criterio)
        {
            await EjecutarConCargaAsync(
                () => _repositorioPOC.BuscarPOCAsync(criterio, _sesionService.IdPosta),
                _view.MostrarPOC
            );
        }

        public async Task EditarPOCAsync(int idPoc)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var poc = await _repositorioPOC.ObtenerPorIdAsync(idPoc)
                    ?? throw new Exception("POC no encontrada.");

                await AbrirFormularioAsync<AgregarEditarPoc>(async form =>
                {
                    form._presenter.CargarDatosParaEditar(poc);
                    await Task.CompletedTask;
                });
            }, CargarPOCAsync);
        }

        public async Task AgregarPOCAsync()
        {
            await EjecutarConCargaAsync(() =>
            {
                return AbrirFormularioAsync<AgregarEditarPoc>(async form => await Task.CompletedTask);
            }, CargarPOCAsync);
        }

        public async Task EliminarPOCAsync(int id)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar esta POC?");
                if (confirmacion == DialogResult.Yes)
                {
                    await _repositorioPOC.EliminarPOCAsync(id);
                    _view.MostrarMensaje("POC eliminada correctamente.");
                }
            }, CargarPOCAsync);
        }

        public async Task AbrirMenuIngresaGasoilOtrosAsync(int idPoc)
        {
            await AbrirFormularioAsync<MenuIngresarGasoilOtros>(async form =>
            {
                await form._presenter.CargarDatosAsync(idPoc);
            });
        }
    }
}