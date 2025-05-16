using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views.Postas.Modificaciones.ReabrirPoc;

namespace GestionFlota.Presenters
{
    public class ReabrirPocPresenter : BasePresenter<IReabrirPocView>
    {
        private readonly IPOCRepositorio _repositorioPOC;

        public ReabrirPocPresenter(
            IPOCRepositorio repositorioPOC,
            ISesionService sesionService,
            INavigationService navigationService)
        : base(sesionService, navigationService)  // Aquí pasamos las dependencias a la clase base
        {
            _repositorioPOC = repositorioPOC;
        }

        public async Task InicializarAsync()
        {
            await CargarPOCAsync();
        }

        public async Task CargarPOCAsync()
        {
            await EjecutarConCargaAsync(
                () => _repositorioPOC.ObtenerTodosPorPostaAsync(_sesionService.IdPosta, "cerrada"),
                _view.MostrarPOC
            );
        }

        public async Task ReabrirPOCAsync(int id)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea reabrir esta POC?");
                if (confirmacion == DialogResult.Yes)
                {
                    await _repositorioPOC.ActualizarFechaCierreYEstadoAsync(id, null, "abierta");
                    _view.MostrarMensaje("POC reabierta correctamente.");
                }
            }, CargarPOCAsync);
        }
    }
}