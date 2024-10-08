using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc;

namespace SacNew.Presenters
{
    public class MenuIngresaConsumosPresenter
    {
        private IMenuIngresaConsumosView _view;
        private readonly IRepositorioPOC _repositorioPOC;
        private readonly ISesionService _sesionService;
        private readonly IServiceProvider _serviceProvider;

        public MenuIngresaConsumosPresenter(IRepositorioPOC repositorioPOC, ISesionService sesionService, IServiceProvider serviceProvider)
        {
            _repositorioPOC = repositorioPOC;
            _sesionService = sesionService;
            _serviceProvider = serviceProvider;
        }

        public void SetView(IMenuIngresaConsumosView view)
        {
            _view = view;
        }

        public async Task InicializarAsync()
        {
            await ManejarErroresAsync(async () =>
            {
                MostrarNombreUsuario();
                await CargarPOCAsync().ConfigureAwait(false);  // Cargar las POCs al inicializar
            });
        }

        public void MostrarNombreUsuario()
        {
            string nombreUsuario = _sesionService.NombreCompleto;
            _view.MostrarNombreUsuario(nombreUsuario);
        }

        public async Task CargarPOCAsync()
        {
            await ManejarErroresAsync(async () =>
            {
                var listaPOC = await _repositorioPOC.ObtenerTodosAsync().ConfigureAwait(false);
                _view.MostrarPOC(listaPOC);
            });
        }

        public async Task BuscarPOCAsync(string criterio)
        {
            await ManejarErroresAsync(async () =>
            {
                var listaFiltrada = await _repositorioPOC.BuscarPOCAsync(criterio).ConfigureAwait(false);
                _view.MostrarPOC(listaFiltrada);
            });
        }

        public async Task EditarPOCAsync(int idPoc)
        {
            await ManejarErroresAsync(async () =>
            {
                var poc = await _repositorioPOC.ObtenerPorIdAsync(idPoc).ConfigureAwait(false);
                if (poc == null)
                {
                    _view.MostrarMensaje("POC no encontrada.");
                    return;
                }

                var agregarEditarPoc = _serviceProvider.GetService<AgregarEditarPoc>();
                agregarEditarPoc._presenter.CargarDatosParaEditar(poc);  // Cargar los datos en el formulario
                agregarEditarPoc.ShowDialog();
                await CargarPOCAsync().ConfigureAwait(false);  // Recargar la lista después de la edición
            });
        }
        public async Task AgregarPOCAsync()
        {
            await ManejarErroresAsync(async () =>
            {
                var agregarEditarPOC = _serviceProvider.GetService<AgregarEditarPoc>();
                agregarEditarPOC.ShowDialog();
                await CargarPOCAsync().ConfigureAwait(false);  // Recargar después de agregar
            });
        }

        public async Task EliminarPOCAsync(int id)
        {
            await ManejarErroresAsync(async () =>
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar esta POC?");
                if (confirmacion == DialogResult.Yes)
                {
                    await _repositorioPOC.EliminarPOCAsync(id).ConfigureAwait(false);
                    _view.MostrarMensaje("POC eliminada correctamente.");
                    await CargarPOCAsync().ConfigureAwait(false);  // Recargar la lista después de eliminar
                }
            });
        }

        private async Task ManejarErroresAsync(Func<Task> accion)
        {
            try
            {
                await accion();
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Ocurrió un error: {ex.Message}");
            }
        }
    }
}