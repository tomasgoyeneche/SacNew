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
            try
            {
                MostrarNombreUsuario();
                await CargarPOCAsync();  // Cargar las POCs al inicializar
            }
            catch (Exception ex)
            {
                // Manejo centralizado de errores
                _view.MostrarMensaje($"Error al inicializar: {ex.Message}");
            }
        }

        public void MostrarNombreUsuario()
        {
            string nombreUsuario = _sesionService.NombreCompleto;
            _view.MostrarNombreUsuario(nombreUsuario);
        }

        public async Task CargarPOCAsync()
        {
            try
            {
                var listaPOC = await Task.Run(() => _repositorioPOC.ObtenerTodos());
                _view.MostrarPOC(listaPOC);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar POCs: {ex.Message}");
            }
        }

        public void BuscarPOC(string criterio)
        {
            try
            {
                var listaFiltrada = _repositorioPOC.BuscarPOC(criterio);
                _view.MostrarPOC(listaFiltrada);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al buscar POCs: {ex.Message}");
            }
        }

        public async Task EditarPOCAsync(int idPoc)
        {
            try
            {
                var poc = ObtenerPOCPorId(idPoc);
                var agregarEditarPoc = _serviceProvider.GetService<AgregarEditarPoc>();
                agregarEditarPoc._presenter.CargarDatosParaEditar(poc);  // Cargar los datos en el formulario
                agregarEditarPoc.ShowDialog();
                await CargarPOCAsync(); // Recargar la lista después de la edición
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al editar la POC: {ex.Message}");
            }
        }

        public async Task AgregarPOCAsync()
        {
            try
            {
                var agregarEditarPOC = _serviceProvider.GetService<AgregarEditarPoc>();
                agregarEditarPOC.ShowDialog();
                await CargarPOCAsync(); // Recargar después de agregar
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al agregar la POC: {ex.Message}");
            }
        }

        public async Task EliminarPOCAsync(int id)
        {
            try
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar esta POC?");
                if (confirmacion == DialogResult.Yes)
                {
                    _repositorioPOC.EliminarPOC(id);
                    _view.MostrarMensaje("POC eliminada correctamente.");
                    await CargarPOCAsync(); // Recargar la lista después de eliminar
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al eliminar la POC: {ex.Message}");
            }
        }

        private POC ObtenerPOCPorId(int idPoc)
        {
            try
            {
                return _repositorioPOC.ObtenerPorId(idPoc);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al obtener la POC: {ex.Message}");
                return null;
            }
        }
    }
}