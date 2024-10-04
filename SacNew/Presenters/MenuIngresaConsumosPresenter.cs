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

        public void Inicializar()
        {
            MostrarNombreUsuario();
            CargarPOC();  // Cargar las POCs al inicializar
        }

        public void MostrarNombreUsuario()
        {
            string nombreUsuario = _sesionService.NombreCompleto;
            _view.MostrarNombreUsuario(nombreUsuario);
        }

        public void CargarPOC()
        {
            var listaPOC = _repositorioPOC.ObtenerTodos();
            _view.MostrarPOC(listaPOC);
        }

        public void BuscarPOC(string criterio)
        {
            var listaFiltrada = _repositorioPOC.BuscarPOC(criterio);
            _view.MostrarPOC(listaFiltrada);
        }

        public POC ObtenerPOCPorId(int idPoc)
        {
            return _repositorioPOC.ObtenerPorId(idPoc);
        }

        public void EditarPOC(POC poc)
        {
            // Obtener los datos de la POC desde el repositorio
            var agregarEditarPoc = _serviceProvider.GetService<AgregarEditarPoc>();
            agregarEditarPoc._presenter.CargarDatosParaEditar(poc);// Cargar los datos en el formulario
            agregarEditarPoc.ShowDialog();
            CargarPOC(); // Recargar la lista después de la edición
        }

        public void AgregarPOC()
        {
            // Obtener los datos de la POC desde el repositorio

            // Recargar la lista después de la edición

            using (var agregarEditarPOC = _serviceProvider.GetService<AgregarEditarPoc>())
            {
                agregarEditarPOC.ShowDialog();
                CargarPOC();
            }
        }

        public void EliminarPOC(int id)
        {
            var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar esta POC?");
            if (confirmacion == DialogResult.Yes)
            {
                _repositorioPOC.EliminarPOC(id);
                _view.MostrarMensaje("POC eliminada correctamente.");
                CargarPOC();  // Recargar la lista después de eliminar
            }
        }
    }
}